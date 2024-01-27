using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nomis.DataAccess.PostgreSql.Referral.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Referral");

            migrationBuilder.CreateTable(
                name: "AuditTrails",
                schema: "Referral",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    EntityName = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    AffectedColumns = table.Column<string>(type: "text", nullable: true),
                    PrimaryKey = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferralWallets",
                schema: "Referral",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletAddress = table.Column<string>(type: "text", nullable: false),
                    ReferralCode = table.Column<string>(type: "text", nullable: false),
                    RewardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralWallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferralDatas",
                schema: "Referral",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReferredWalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReferringWalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReferralLevel = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferralDatas_ReferralWallets_ReferredWalletId",
                        column: x => x.ReferredWalletId,
                        principalSchema: "Referral",
                        principalTable: "ReferralWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReferralDatas_ReferralWallets_ReferringWalletId",
                        column: x => x.ReferringWalletId,
                        principalSchema: "Referral",
                        principalTable: "ReferralWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RewardDatas",
                schema: "Referral",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RewardedWalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    LastPaidTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RewardDatas_ReferralWallets_RewardedWalletId",
                        column: x => x.RewardedWalletId,
                        principalSchema: "Referral",
                        principalTable: "ReferralWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReferralDatas_ReferredWalletId",
                schema: "Referral",
                table: "ReferralDatas",
                column: "ReferredWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralDatas_ReferringWalletId",
                schema: "Referral",
                table: "ReferralDatas",
                column: "ReferringWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_RewardDatas_RewardedWalletId",
                schema: "Referral",
                table: "RewardDatas",
                column: "RewardedWalletId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrails",
                schema: "Referral");

            migrationBuilder.DropTable(
                name: "ReferralDatas",
                schema: "Referral");

            migrationBuilder.DropTable(
                name: "RewardDatas",
                schema: "Referral");

            migrationBuilder.DropTable(
                name: "ReferralWallets",
                schema: "Referral");
        }
    }
}
