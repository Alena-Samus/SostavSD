using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class TableProjectDrop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingViewId = table.Column<int>(type: "int", nullable: false),
                    CiCVersion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ConstructionPhase = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    PriceLevel = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrintType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Priority = table.Column<int>(type: "int", maxLength: 3, nullable: true),
                    ProjectReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectReleaseDateByContract = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkStartDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                });
        }
    }
}
