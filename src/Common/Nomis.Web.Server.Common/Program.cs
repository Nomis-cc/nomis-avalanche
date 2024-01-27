// ------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nomis.Aave;
using Nomis.Api.Aave.Extensions;
using Nomis.Api.Avalanche.Extensions;
using Nomis.Api.BalanceChecker.Extensions;
using Nomis.Api.Chainanalysis.Extensions;
using Nomis.Api.Common.Extensions;
using Nomis.Api.Common.Middlewares;
using Nomis.Api.Common.Settings;
using Nomis.Api.Common.Swagger.Filters;
using Nomis.Api.Covalent.Extensions;
using Nomis.Api.CyberConnect.Extensions;
using Nomis.Api.DeFi.Extensions;
using Nomis.Api.DefiLlama.Extensions;
using Nomis.Api.DexAggregator.Extensions;
using Nomis.Api.Greysafe.Extensions;
using Nomis.Api.Hapi.Extensions;
using Nomis.Api.IPFS.Extensions;
using Nomis.Api.LensProtocol.Extensions;
using Nomis.Api.MailServices.Extensions;
using Nomis.Api.PolygonId.Extensions;
using Nomis.Api.Referrals.Extensions;
using Nomis.Api.Snapshot.Extensions;
using Nomis.Api.SoulboundToken.Extensions;
using Nomis.Api.Tally.Extensions;
using Nomis.BalanceChecker;
using Nomis.Blockchain.Abstractions.Settings;
using Nomis.CacheProviderService.Extensions;
using Nomis.Chainanalysis;
using Nomis.Coingecko.Extensions;
using Nomis.Covalent;
using Nomis.CurrentUserService.Extensions;
using Nomis.CyberConnect;
using Nomis.DataAccess.PostgreSql.Extensions;
using Nomis.DataAccess.PostgreSql.Referral.Extensions;
using Nomis.DataAccess.PostgreSql.Scoring.Extensions;
using Nomis.DeFi;
using Nomis.DefiLlama;
using Nomis.DexProviderService;
using Nomis.Domain;
using Nomis.ElasticMailServices;
using Nomis.Greysafe;
using Nomis.HapiExplorer;
using Nomis.IPFS;
using Nomis.LensProtocol;
using Nomis.PolygonId;
using Nomis.ProxyService.Extensions;
using Nomis.ReferralService;
using Nomis.ReferralService.Extensions;
using Nomis.ScoringService.Extensions;
using Nomis.Snapshot;
using Nomis.Snowtrace;
using Nomis.SoulboundTokenService;
using Nomis.Tally;
using Nomis.Utils.Extensions;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Filters;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Configuration
    .AddJsonFiles()
    .AddEnvironmentVariables()
    .AddUserSecrets<PolygonIdApi>()
    .AddUserSecrets<IPFS>()
    .AddUserSecrets<EvmScoreSoulboundToken>()
    .AddUserSecrets<BalanceChecker>()
    .AddUserSecrets<CovalentApi>()
    .AddUserSecrets<Tally>();

builder.Services
    .AddHttpContextAccessor()
    .AddCache(builder.Configuration)
    .AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
    })
    .AddSettings<BlacklistSettings>(builder.Configuration)
    .AddCurrentUserService()
    .AddApplicationPersistence(builder.Configuration)
    .AddScoringPersistence(builder.Configuration)
    .AddReferralPersistence(builder.Configuration)
    .AddUSDConverter(builder.Configuration)
    .AddScoringService()
    .AddReferralService(builder.Configuration)
    .AddProxyService(builder.Configuration);

var scoringOptions = builder.ConfigureScoringOptions()
    .WithReferralsService<Referral>()
    .WithIPFSService<IPFS>()
    .WithCovalentAPI<CovalentApi>()
    .WithDefiLlamaAPI<DefiLlamaApi>()
    .WithDeFiAPI<DeFiApi>()
    .WithBalanceCheckerService<BalanceChecker>()
    .WithPolygonIdAPI<PolygonIdApi>()
    .WithSnapshotProtocol<SnapshotHub>()
    .WithTallyProtocol<Tally>()
    .WithCyberConnectProtocol<CyberConnect>()
    .WithLensProtocol<LensProtocol>()
    .WithAaveLendingProtocol<Aave>()
    .WithHAPIProtocol<HapiExplorer>()
    .WithGreysafeService<Greysafe>()
    .WithChainanalysisService<Chainanalysis>()
    .WithEvmSoulboundTokenService<EvmScoreSoulboundToken>()
    .WithAvalancheBlockchain<Snowtrace>()
    .WithDexAggregator<DexProviderRegistrar>()
    .WithMailService<ElasticMail>()
    .Build();

builder.Services
    .AddApiVersioning(config =>
    {
        config.DefaultApiVersion = new(1, 0);
        config.AssumeDefaultVersionWhenUnspecified = true;
        config.ReportApiVersions = true;
    });

builder.Services.AddSettings<CorsSettings>(builder.Configuration);
var corsSettings = builder.Configuration.GetSettings<CorsSettings>();
if (corsSettings.UseCORS)
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin();
            policy.WithMethods("GET", "OPTIONS");
            policy.AllowAnyHeader();
        });

        foreach (var policyOrigins in corsSettings.PolicyOrigins)
        {
            options.AddPolicy(policyOrigins.Key, policy =>
            {
                policy.WithOrigins(policyOrigins.Value.ToArray());
                policy.WithMethods("GET", "OPTIONS");
                policy.AllowAnyHeader();
            });
        }
    });
}

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressMapClientErrors = true;
    })
    .ConfigureApplicationPartManager(manager =>
    {
        manager.AddNomisControllers(scoringOptions);
    });

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSingleton<ExceptionHandlingMiddleware>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSettings<ApiCommonSettings>(builder.Configuration);
var apiCommonSettings = builder.Configuration.GetSettings<ApiCommonSettings>();

if (apiCommonSettings.UseSwagger)
{
    builder.Services.AddSwaggerGen(options =>
    {
        #region Add xml-comments

        var xmlPathes = new List<string>();
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (!assembly.IsDynamic)
            {
                string xmlFile = $"{assembly.GetName().Name}.xml";
                string xmlPath = Path.Combine(baseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                    options.IncludeXmlCommentsWithRemarks(xmlPath);
                    xmlPathes.Add(xmlPath);
                }
            }
        }

        options.UseAllOfToExtendReferenceSchemas();

        options.IncludeXmlCommentsFromInheritDocs(true);

        options.AddEnumsWithValuesFixFilters(o =>
        {
            o.ApplySchemaFilter = true;
            o.XEnumNamesAlias = "x-enum-varnames";
            o.XEnumDescriptionsAlias = "x-enum-descriptions";
            o.ApplyParameterFilter = true;
            o.ApplyDocumentFilter = true;
            o.IncludeDescriptions = true;
            o.IncludeXEnumRemarks = true;
            o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;
            o.NewLine = "\n";

            foreach (string xmlPath in xmlPathes)
            {
                o.IncludeXmlCommentsFrom(xmlPath);
            }
        });

        #endregion Add xml-comments

        options.SwaggerDoc("v1", new()
        {
            Version = "v1",
            Title = "Nomis Score API",
            Description = "An API to get Nomis Score for crypto wallets.",
        });

        #region Add Versioning

        options.OperationFilter<RemoveVersionFromParameterFilter>();
        options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
        options.DocInclusionPredicate((version, desc) =>
        {
            if (!desc.TryGetMethodInfo(out var methodInfo))
            {
                return false;
            }

            var versions = methodInfo
                .DeclaringType?
                .GetCustomAttributes(true)
                .OfType<ApiVersionAttribute>()
                .SelectMany(attr => attr.Versions);

            var maps = methodInfo
                .GetCustomAttributes(true)
                .OfType<MapToApiVersionAttribute>()
                .SelectMany(attr => attr.Versions)
                .ToList();

            return versions?.Any(v => string.Equals($"v{v}", version, StringComparison.OrdinalIgnoreCase)) == true
                   && (maps.Count == 0 || maps.Exists(v => string.Equals($"v{v}", version, StringComparison.OrdinalIgnoreCase)));
        });

        #endregion Add Versioning

        options.EnableAnnotations();
        options.ExampleFilters();

        options.DocumentFilter<AppendActionCountToTagSummaryDocumentFilter>("(the count of actions: {0})");

        options.AddNomisFilters(scoringOptions);

        options.OrderActionsBy(apiDesc => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.RelativePath}");
        options.DocumentFilter<TagOrderByNameDocumentFilter>();
    });
    builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

    if (apiCommonSettings.UseSwaggerCaching)
    {
        builder.Services.Replace(ServiceDescriptor.Transient<ISwaggerProvider, CachingSwaggerProvider>());
    }
}

builder.Services
    .AddHealthChecks()
    .AddCustomHealthChecks(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    if (apiCommonSettings.SerilogSinkToConsole)
    {
        configuration.WriteTo.Console(outputTemplate: apiCommonSettings.SerilogConsoleOutputTemplate);
    }
});
var app = builder.Build();

using var serviceScope = app.Services.CreateScope();
foreach (var initializer in serviceScope.ServiceProvider.GetServices<IDatabaseSeeder>())
{
    initializer.Initialize();
}

app.UseResponseCompression();

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = SerilogExtensions.HttpRequestEnricher;
});

app.MapHealthChecks("/health");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseStaticFiles();

app.UseRouting();

if (apiCommonSettings.UseSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "Nomis Score API V1");
        options.DocumentTitle = "Nomis Score API V1";
        options.DefaultModelsExpandDepth(0);

        options.InjectStylesheet("../css/swagger.css");
        options.DisplayRequestDuration();
        options.DocExpansion(DocExpansion.None);

        options.ConfigObject.DisplayOperationId = true;
        options.ConfigObject.ShowCommonExtensions = true;
        options.ConfigObject.ShowExtensions = true;
    });
}

app.UseHttpsRedirection();

if (corsSettings.UseCORS)
{
    app.UseCors();
}

app.UseAuthorization();

app.MapControllers();

try
{
    await app.RunAsync().ConfigureAwait(false);
}
catch (Exception e) when (e.GetType().Name is not "StopTheHostException" && e.GetType().Name is not "HostAbortedException")
{
    Log.Fatal(e, "A fatal error has occurred!");
}
finally
{
    Log.Information("Server Shutting down...");
    await Log.CloseAndFlushAsync().ConfigureAwait(false);
}