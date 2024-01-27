using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nomis.DataAccess.PostgreSql.Scoring.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreDataToScoringData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Blockchain",
                schema: "Scoring",
                table: "ScoringDatas",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CalculationModel",
                schema: "Scoring",
                table: "ScoringDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RawRequest",
                schema: "Scoring",
                table: "ScoringDatas",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculationModel",
                schema: "Scoring",
                table: "ScoringDatas");

            migrationBuilder.DropColumn(
                name: "RawRequest",
                schema: "Scoring",
                table: "ScoringDatas");

            migrationBuilder.AlterColumn<int>(
                name: "Blockchain",
                schema: "Scoring",
                table: "ScoringDatas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");
        }
    }
}
