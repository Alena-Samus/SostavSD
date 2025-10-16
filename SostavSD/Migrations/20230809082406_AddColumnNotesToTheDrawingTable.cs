using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnNotesToTheDrawingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "DrawingEstimate");

            migrationBuilder.RenameColumn(
                name: "DrawingDateOfAdmissionToDepartmetn",
                table: "Drawing",
                newName: "DrawingDateOfAdmissionToDepartment");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Drawing",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Drawing");

            migrationBuilder.RenameColumn(
                name: "DrawingDateOfAdmissionToDepartment",
                table: "Drawing",
                newName: "DrawingDateOfAdmissionToDepartmetn");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "DrawingEstimate",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
