using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class DropTableConstructionPhase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_ConstructionPhase_ConstructionPhasePhaseId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "ConstructionPhase");

            migrationBuilder.DropIndex(
                name: "IX_Project_ConstructionPhasePhaseId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ConstructionPhasePhaseId",
                table: "Project");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConstructionPhasePhaseId",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConstructionPhase",
                columns: table => new
                {
                    PhaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhaseName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionPhase", x => x.PhaseId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_ConstructionPhasePhaseId",
                table: "Project",
                column: "ConstructionPhasePhaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ConstructionPhase_ConstructionPhasePhaseId",
                table: "Project",
                column: "ConstructionPhasePhaseId",
                principalTable: "ConstructionPhase",
                principalColumn: "PhaseId");
        }
    }
}
