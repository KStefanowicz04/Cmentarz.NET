using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class PoprawaBleduWContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Casket_Deceaseds_DeceasedId",
                table: "Casket");

            migrationBuilder.AddForeignKey(
                name: "FK_Casket_Deceaseds_DeceasedId",
                table: "Casket",
                column: "DeceasedId",
                principalTable: "Deceaseds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Casket_Deceaseds_DeceasedId",
                table: "Casket");

            migrationBuilder.AddForeignKey(
                name: "FK_Casket_Deceaseds_DeceasedId",
                table: "Casket",
                column: "DeceasedId",
                principalTable: "Deceaseds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
