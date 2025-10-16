using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class CreateDrawingEstimateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrawingEstimate",
                columns: table => new
                {
                    DrawingId = table.Column<int>(type: "int", nullable: false),
                    EstimateId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawingEstimate", x => new { x.DrawingId, x.EstimateId });
                    table.ForeignKey(
                        name: "FK_DrawingEstimate_Drawing_DrawingId",
                        column: x => x.DrawingId,
                        principalTable: "Drawing",
                        principalColumn: "DrawingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrawingEstimate_Estimate_EstimateId",
                        column: x => x.EstimateId,
                        principalTable: "Estimate",
                        principalColumn: "EstimateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrawingEstimate_EstimateId",
                table: "DrawingEstimate",
                column: "EstimateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrawingEstimate");
        }
    }
}
