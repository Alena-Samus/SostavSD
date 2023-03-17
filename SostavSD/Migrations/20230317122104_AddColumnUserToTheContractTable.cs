using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnUserToTheContractTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNameId",
                table: "Contract",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_UserNameId",
                table: "Contract",
                column: "UserNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_AspNetUsers_UserNameId",
                table: "Contract",
                column: "UserNameId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_AspNetUsers_UserNameId",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_UserNameId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "UserNameId",
                table: "Contract");
        }
    }
}
