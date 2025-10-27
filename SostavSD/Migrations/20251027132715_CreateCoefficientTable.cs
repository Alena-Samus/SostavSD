using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class CreateCoefficientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coefficient",
                columns: table => new
                {
                    BuildingViewId = table.Column<int>(type: "int", nullable: false),
                    BuildingZoneId = table.Column<int>(type: "int", nullable: false),
                    CoefficientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OHROPR1 = table.Column<double>(type: "float", nullable: true),
                    PlannedProfit = table.Column<double>(type: "float", nullable: true),
                    OHROPR2 = table.Column<double>(type: "float", nullable: true),
                    Relevance = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coefficient", x => new { x.BuildingViewId, x.BuildingZoneId });
                    table.ForeignKey(
                        name: "FK_Coefficient_BuildingView_BuildingViewId",
                        column: x => x.BuildingViewId,
                        principalTable: "BuildingView",
                        principalColumn: "BuildingViewId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coefficient_BuildingZone_BuildingZoneId",
                        column: x => x.BuildingZoneId,
                        principalTable: "BuildingZone",
                        principalColumn: "BuildingZoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coefficient_BuildingZoneId",
                table: "Coefficient",
                column: "BuildingZoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coefficient");
        }
    }
}
