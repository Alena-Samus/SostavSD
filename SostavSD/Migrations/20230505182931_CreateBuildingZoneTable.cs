using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class CreateBuildingZoneTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractDateEndOfWork",
                table: "Contract");

            migrationBuilder.AddColumn<int>(
                name: "BuildingZoneId",
                table: "Contract",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BuildingZone",
                columns: table => new
                {
                    BuildingZoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingZoneName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingZone", x => x.BuildingZoneId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_BuildingZoneId",
                table: "Contract",
                column: "BuildingZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_BuildingZone_BuildingZoneId",
                table: "Contract",
                column: "BuildingZoneId",
                principalTable: "BuildingZone",
                principalColumn: "BuildingZoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_BuildingZone_BuildingZoneId",
                table: "Contract");

            migrationBuilder.DropTable(
                name: "BuildingZone");

            migrationBuilder.DropIndex(
                name: "IX_Contract_BuildingZoneId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "BuildingZoneId",
                table: "Contract");

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractDateEndOfWork",
                table: "Contract",
                type: "datetime2",
                nullable: true);
        }
    }
}
