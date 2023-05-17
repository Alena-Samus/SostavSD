using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnProjectNameToTheProjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Contract");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
