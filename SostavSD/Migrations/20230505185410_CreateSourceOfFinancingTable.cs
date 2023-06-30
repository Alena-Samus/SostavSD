using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class CreateSourceOfFinancingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CalculatorId",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceOfFinancingId",
                table: "Contract",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SourceOfFinancing",
                columns: table => new
                {
                    SourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceOfFinancing", x => x.SourceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_SourceOfFinancingId",
                table: "Contract",
                column: "SourceOfFinancingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_SourceOfFinancing_SourceOfFinancingId",
                table: "Contract",
                column: "SourceOfFinancingId",
                principalTable: "SourceOfFinancing",
                principalColumn: "SourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_SourceOfFinancing_SourceOfFinancingId",
                table: "Contract");

            migrationBuilder.DropTable(
                name: "SourceOfFinancing");

            migrationBuilder.DropIndex(
                name: "IX_Contract_SourceOfFinancingId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "CalculatorId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "SourceOfFinancingId",
                table: "Contract");
        }
    }
}
