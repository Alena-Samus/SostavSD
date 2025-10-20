using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyDrawingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Task",
                table: "Drawing",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drawing_GroupId",
                table: "Drawing",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drawing_Deppart_GroupId",
                table: "Drawing",
                column: "GroupId",
                principalTable: "Deppart",
                principalColumn: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drawing_Deppart_GroupId",
                table: "Drawing");

            migrationBuilder.DropIndex(
                name: "IX_Drawing_GroupId",
                table: "Drawing");

            migrationBuilder.DropColumn(
                name: "Task",
                table: "Drawing");
        }
    }
}
