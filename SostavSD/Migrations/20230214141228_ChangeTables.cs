using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "Contract",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CompanyID",
                table: "Contract",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Company_CompanyID",
                table: "Contract",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Company_CompanyID",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_CompanyID",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Contract");
        }
    }
}
