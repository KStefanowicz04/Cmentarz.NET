using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class CasketDeceasedNotRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Casket_DeceasedId",
                table: "Casket");

            migrationBuilder.AlterColumn<int>(
                name: "DeceasedId",
                table: "Casket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Casket_DeceasedId",
                table: "Casket",
                column: "DeceasedId",
                unique: true,
                filter: "[DeceasedId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Casket_DeceasedId",
                table: "Casket");

            migrationBuilder.AlterColumn<int>(
                name: "DeceasedId",
                table: "Casket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Casket_DeceasedId",
                table: "Casket",
                column: "DeceasedId",
                unique: true);
        }
    }
}
