// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditableDbContextExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nomis.CurrentUserService.Interfaces;
using Nomis.DataAccess.Interfaces.Contexts;
using Nomis.DataAccess.Interfaces.Enums;
using Nomis.DataAccess.Interfaces.EventLogging;
using Nomis.DataAccess.Interfaces.Models;
using Nomis.DataAccess.PostgreSql.Models;
using Nomis.Domain.Contracts;
using Nomis.Domain.Settings;
using Nomis.Utils.Contracts.Deleting;
using Nomis.Utils.Extensions;

namespace Nomis.DataAccess.PostgreSql.Extensions
{
    /// <summary>
    /// <see cref="IAuditableDbContext"/> extension methods.
    /// </summary>
    public static class AuditableDbContextExtensions
    {
        #region SaveChangeWithAuditAndPublishEventsAsync

        /// <summary>
        /// Save changes with audit and publish events asynchronously.
        /// </summary>
        /// <typeparam name="TAuditableDbContext">The auditable DB context type.</typeparam>
        /// <param name="context">Auditable DB context.</param>
        /// <param name="eventLogger"><see cref="IEventLogger"/>.</param>
        /// <param name="publisher"><see cref="IPublisher"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="entitySettings"><see cref="EntitySettings"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns the number of affected records in the database.</returns>
        public static async Task<int> SaveChangesWithAuditAndPublishEventsAsync<TAuditableDbContext>(
            this TAuditableDbContext context,
            IEventLogger eventLogger,
            IPublisher publisher,
            ICurrentUserService currentUserService,
            IOptionsSnapshot<EntitySettings> entitySettings,
            CancellationToken cancellationToken = new())
                where TAuditableDbContext : DbContext, IAuditableDbContext
        {
            var currentUserId = currentUserService.GetUserId();
            foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        if (currentUserId != Guid.Empty)
                        {
                            entry.Entity.CreatedBy = currentUserId;
                        }

                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        if (currentUserId != Guid.Empty)
                        {
                            entry.Entity.LastModifiedBy = currentUserId;
                        }

                        break;
                    case EntityState.Deleted:
                        if (entry.Entity is ISoftDelete softDeletedEntity && entitySettings.Value.SoftDeleteEnabled)
                        {
                            softDeletedEntity.SetBySoftDeleted(currentUserId);
                            entry.State = EntityState.Modified;
                        }

                        break;
                }
            }

            var auditEntries = OnBeforeSaveChanges(context, currentUserId, entitySettings.Value);
            if (currentUserId == Guid.Empty)
            {
                int result = await PublishEventsAsync(context, eventLogger, publisher, auditEntries, cancellationToken).ConfigureAwait(false);
                return await context.BaseSaveChangesAsync(cancellationToken).ConfigureAwait(false) + result;
            }
            else
            {
                int result = await PublishEventsAsync(context, eventLogger, publisher, auditEntries, cancellationToken).ConfigureAwait(false);
                result += await OnAfterSaveChangesAsync(context, auditEntries.Where(_ => _.HasTemporaryProperties).ToList()).ConfigureAwait(false);
                return await context.SaveChangesAsync(true, cancellationToken).ConfigureAwait(false) + result;
            }
        }

        #endregion SaveChangeWithAuditAndPublishEventsAsync

        #region SaveChangesWithAuditAndPublishEvents

        /// <summary>
        /// Save changes with audit and publish events synchronously.
        /// </summary>
        /// <typeparam name="TAuditableDbContext">The auditable DB context type.</typeparam>
        /// <param name="context">Auditable DB context.</param>
        /// <param name="eventLogger"><see cref="IEventLogger"/>.</param>
        /// <param name="publisher"><see cref="IPublisher"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="entitySettings"><see cref="EntitySettings"/>.</param>
        /// <returns>Returns the number of affected records in the database.</returns>
        public static int SaveChangesWithAuditAndPublishEvents<TAuditableDbContext>(
            this TAuditableDbContext context,
            IEventLogger eventLogger,
            IPublisher publisher,
            ICurrentUserService currentUserService,
            IOptionsSnapshot<EntitySettings> entitySettings)
                where TAuditableDbContext : DbContext, IAuditableDbContext
        {
            return SaveChangesWithAuditAndPublishEventsAsync(context, eventLogger, publisher, currentUserService, entitySettings).GetAwaiter().GetResult();
        }

        #endregion SaveChangesWithAuditAndPublishEvents

        #region OnBeforeSaveChanges

        /// <summary>
        /// Get audit collection before save changes.
        /// </summary>
        /// <typeparam name="TAuditableDbContext">The auditable DB context type.</typeparam>
        /// <param name="context">Auditable DB context.</param>
        /// <param name="userId">User id.</param>
        /// <param name="entitySettings"><see cref="EntitySettings"/>.</param>
        private static List<IAuditEntry> OnBeforeSaveChanges<TAuditableDbContext>(
            TAuditableDbContext context,
            Guid userId,
            EntitySettings entitySettings)
                where TAuditableDbContext : DbContext, IAuditableDbContext
        {
            var auditEntries = new List<IAuditEntry>();
            if (!entitySettings.EnableAudit)
            {
                return auditEntries;
            }

            foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State is EntityState.Detached or EntityState.Unchanged)
                {
                    continue;
                }

                var auditEntry = new AuditEntry(entry)
                {
                    UserId = userId
                };
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Detached:
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified && !(property.OriginalValue == null && property.CurrentValue == null) && property.OriginalValue?.Equals(property.CurrentValue) != true)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                context.AuditTrails.Add(auditEntry.ToAudit());
            }

            return auditEntries.ToList();
        }

        #endregion OnBeforeSaveChanges

        #region OnAfterSaveChangesAsync

        /// <summary>
        /// Save audit collection data after DB save changes.
        /// </summary>
        /// <typeparam name="TAuditableDbContext">The auditable DB context type.</typeparam>
        /// <param name="context">Auditable DB context.</param>
        /// <param name="auditEntries">Audit entries.</param>
        /// <returns>Returns the number of affected records in the database.</returns>
        private static Task<int> OnAfterSaveChangesAsync<TAuditableDbContext>(
            TAuditableDbContext context,
            IReadOnlyCollection<IAuditEntry>? auditEntries)
                where TAuditableDbContext : DbContext, IAuditableDbContext
        {
            if (auditEntries == null || auditEntries.Count == 0)
            {
                return Task.FromResult(0);
            }

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                context.AuditTrails.Add(auditEntry.ToAudit());
            }

            return context.BaseSaveChangesAsync();
        }

        #endregion OnAfterSaveChangesAsync

        #region PublishEventsAsync

        /// <summary>
        /// Publish domain events.
        /// </summary>
        /// <typeparam name="TAuditableDbContext">The auditable DB context type.</typeparam>
        /// <param name="context">Auditable DB context.</param>
        /// <param name="eventLogger"><see cref="IEventLogger"/>.</param>
        /// <param name="publisher"><see cref="IPublisher"/>.</param>
        /// <param name="auditEntries">Audit entries.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Returns the number of affected records in the database.</returns>
        private static async Task<int> PublishEventsAsync<TAuditableDbContext>(
            TAuditableDbContext context,
            IEventLogger eventLogger,
            IPublisher publisher,
            List<IAuditEntry> auditEntries,
            CancellationToken cancellationToken = new())
            where TAuditableDbContext : DbContext, IAuditableDbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<IGeneratesDomainEvents>()
                .Where(x => x.Entity.DomainEvents.Count > 0)
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    var relatedAuditEntries = auditEntries.Where(x => domainEvent.RelatedEntities.Any(t => t == x.Entry.Entity.GetType())).ToList();
                    if (relatedAuditEntries.Count > 0)
                    {
                        var oldValues = relatedAuditEntries.Select(x => new
                        {
                            Entity = x.Entry.Entity.GetType().GetGenericTypeName(),
                            OldValues = x.OldValues
                        }).ToList();
                        var newValues = relatedAuditEntries.Select(x => new
                        {
                            Entity = x.Entry.Entity.GetType().GetGenericTypeName(),
                            NewValues = x.NewValues
                        }).ToList();
                        var changes = (oldValues.Count == 0 ? null : JsonSerializer.Serialize(oldValues), newValues.Count == 0 ? null : JsonSerializer.Serialize(newValues));
                        await eventLogger.SaveAsync(domainEvent, changes).ConfigureAwait(false);
                        await publisher.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
                    }
                    else
                    {
                        await eventLogger.SaveAsync(domainEvent, (null, null)).ConfigureAwait(false);
                        await publisher.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
                    }
                });
            await Task.WhenAll(tasks).ConfigureAwait(false);
            return await context.BaseSaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        #endregion PublishEventsAsync
    }
}