using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class CreateEstimateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estimate",
                columns: table => new
                {
                    EstimateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstimateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimateCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimateReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReplacementOrAdditionType = table.Column<int>(type: "int", nullable: true),
                    ReplacementOrAdditionEsimateId = table.Column<int>(type: "int", nullable: true),
                    EstimatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SMR = table.Column<double>(type: "float", nullable: true),
                    Equipment = table.Column<double>(type: "float", nullable: true),
                    Other = table.Column<double>(type: "float", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estimate", x => x.EstimateId);
                    table.ForeignKey(
                        name: "FK_Estimate_AspNetUsers_EstimatorId",
                        column: x => x.EstimatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Estimate_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estimate_EstimatorId",
                table: "Estimate",
                column: "EstimatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Estimate_StatusId",
                table: "Estimate",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estimate");
        }
    }
}
