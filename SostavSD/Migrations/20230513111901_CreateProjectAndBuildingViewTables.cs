using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class CreateProjectAndBuildingViewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildingView",
                columns: table => new
                {
                    BuildingViewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingViewName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingView", x => x.BuildingViewId);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    BuildingViewId = table.Column<int>(type: "int", nullable: false),
                    ProjectReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectReleaseDateByContract = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PriceLevel = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrintType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CiCVersion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_BuildingView_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "BuildingView",
                        principalColumn: "BuildingViewId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_Contract_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Contract",
                        principalColumn: "ContractID");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "BuildingView");
        }
    }
}
