using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class CreateProjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    ConstructionPhase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    BuildingViewId = table.Column<int>(type: "int", nullable: false),
                    ProjectReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectReleaseDateByContract = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PriceLevel = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrintType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CiCVersion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_BuildingView_BuildingViewId",
                        column: x => x.BuildingViewId,
                        principalTable: "BuildingView",
                        principalColumn: "BuildingViewId");
                    table.ForeignKey(
                        name: "FK_Project_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "ContractID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_BuildingViewId",
                table: "Project",
                column: "BuildingViewId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ContractId",
                table: "Project",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}
