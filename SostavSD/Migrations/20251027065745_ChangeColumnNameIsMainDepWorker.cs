using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNameIsMainDepWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMaineDepWorker",
                table: "AspNetUsers",
                newName: "IsMainDepWorker");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMainDepWorker",
                table: "AspNetUsers",
                newName: "IsMaineDepWorker");
        }
    }
}
