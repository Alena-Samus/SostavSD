using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class CreateStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsProject = table.Column<bool>(type: "bit", nullable: false),
                    IsDrawing = table.Column<bool>(type: "bit", nullable: false),
                    IsEstimate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_StatusId",
                table: "Project",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Status_StatusId",
                table: "Project",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Status_StatusId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Project_StatusId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Project");
        }
    }
}
