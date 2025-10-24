using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsProjectKAndMainDepWorkerToProjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainDepWorker",
                table: "Project",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProjectK1",
                table: "Project",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProjectK2",
                table: "Project",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainDepWorker",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectK1",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectK2",
                table: "Project");
        }
    }
}
