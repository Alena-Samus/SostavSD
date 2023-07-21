using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class CreateDrawingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drawing",
                columns: table => new
                {
                    DrawingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrawingName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    DrawingPriority = table.Column<int>(type: "int", nullable: true),
                    DrawingReleaseDateBySchedule = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DrawingReleaseDateDepertment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DrawingDateOfAdmissionToDepartmetn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drawing", x => x.DrawingId);
                    table.ForeignKey(
                        name: "FK_Drawing_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drawing_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drawing_ProjectId",
                table: "Drawing",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Drawing_StatusId",
                table: "Drawing",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drawing");
        }
    }
}
