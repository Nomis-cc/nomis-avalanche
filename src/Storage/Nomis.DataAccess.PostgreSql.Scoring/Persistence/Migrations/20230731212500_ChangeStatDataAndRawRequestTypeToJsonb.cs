using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nomis.DataAccess.PostgreSql.Scoring.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStatDataAndRawRequestTypeToJsonb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Temp_StatData",
                nullable: true,
                table: "ScoringDatas",
                schema: "Scoring",
                type: "jsonb");

            migrationBuilder.Sql("UPDATE \"Scoring\".\"ScoringDatas\" SET \"Temp_StatData\" = \"StatData\"::jsonb;");

            migrationBuilder.DropColumn(
                name: "StatData",
                table: "ScoringDatas",
                schema: "Scoring");

            migrationBuilder.RenameColumn(
                name: "Temp_StatData",
                table: "ScoringDatas",
                newName: "StatData",
                schema: "Scoring");

            migrationBuilder.AlterColumn<string>(
                name: "StatData",
                schema: "Scoring",
                table: "ScoringDatas",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Temp_RawRequest",
                nullable: true,
                table: "ScoringDatas",
                schema: "Scoring",
                type: "jsonb");

            migrationBuilder.Sql("UPDATE \"Scoring\".\"ScoringDatas\" SET \"Temp_RawRequest\" = \"RawRequest\"::jsonb;");

            migrationBuilder.DropColumn(
                name: "RawRequest",
                table: "ScoringDatas",
                schema: "Scoring");

            migrationBuilder.RenameColumn(
                name: "Temp_RawRequest",
                table: "ScoringDatas",
                newName: "RawRequest",
                schema: "Scoring");

            migrationBuilder.AlterColumn<string>(
                name: "RawRequest",
                schema: "Scoring",
                table: "ScoringDatas",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StatData",
                schema: "Scoring",
                table: "ScoringDatas",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "RawRequest",
                schema: "Scoring",
                table: "ScoringDatas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);
        }
    }
}
