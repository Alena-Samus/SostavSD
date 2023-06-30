using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SostavSD.Migrations
{
    /// <inheritdoc />
    public partial class CreateDesignStageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StageId",
                table: "Project",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PrintType",
                table: "Project",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConstructionPhase",
                table: "Project",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CiCVersion",
                table: "Project",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuildingNumber",
                table: "Project",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DesignStage",
                columns: table => new
                {
                    StageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignStage", x => x.StageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_StageId",
                table: "Project",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_DesignStage_StageId",
                table: "Project",
                column: "StageId",
                principalTable: "DesignStage",
                principalColumn: "StageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_DesignStage_StageId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "DesignStage");

            migrationBuilder.DropIndex(
                name: "IX_Project_StageId",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "StageId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrintType",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConstructionPhase",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CiCVersion",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuildingNumber",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
