using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnConstructionPhaseToTheProjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_BuildingView_ProjectId",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Project",
                type: "int",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 3);

            migrationBuilder.AddColumn<string>(
                name: "ConstructionPhase",
                table: "Project",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

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
                name: "FK_Project_BuildingView_ProjectId",
                table: "Project",
                column: "ProjectId",
                principalTable: "BuildingView",
                principalColumn: "BuildingViewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ConstructionPhase_ConstructionPhasePhaseId",
                table: "Project",
                column: "ConstructionPhasePhaseId",
                principalTable: "ConstructionPhase",
                principalColumn: "PhaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_BuildingView_ProjectId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_ConstructionPhase_ConstructionPhasePhaseId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "ConstructionPhase");

            migrationBuilder.DropIndex(
                name: "IX_Project_ConstructionPhasePhaseId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ConstructionPhase",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ConstructionPhasePhaseId",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Project",
                type: "int",
                maxLength: 3,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_BuildingView_ProjectId",
                table: "Project",
                column: "ProjectId",
                principalTable: "BuildingView",
                principalColumn: "BuildingViewId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
