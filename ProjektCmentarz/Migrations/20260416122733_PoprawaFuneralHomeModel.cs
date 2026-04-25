using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class PoprawaFuneralHomeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuneralHomes_FuneralHomeName_FuneralHomeNameId",
                table: "FuneralHomes");

            migrationBuilder.DropTable(
                name: "FuneralHomeName");

            migrationBuilder.DropIndex(
                name: "IX_FuneralHomes_FuneralHomeNameId",
                table: "FuneralHomes");

            migrationBuilder.DropColumn(
                name: "FuneralHomeNameId",
                table: "FuneralHomes");

            migrationBuilder.AddColumn<string>(
                name: "FuneralHomeName",
                table: "FuneralHomes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FuneralHomeName",
                table: "FuneralHomes");

            migrationBuilder.AddColumn<int>(
                name: "FuneralHomeNameId",
                table: "FuneralHomes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FuneralHomeName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuneralHomeName", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuneralHomes_FuneralHomeNameId",
                table: "FuneralHomes",
                column: "FuneralHomeNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_FuneralHomes_FuneralHomeName_FuneralHomeNameId",
                table: "FuneralHomes",
                column: "FuneralHomeNameId",
                principalTable: "FuneralHomeName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
