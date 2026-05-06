using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class PoprawaRelacjiCascadeDeceased : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Casket_Deceaseds_DeceasedId",
                table: "Casket");

            migrationBuilder.DropForeignKey(
                name: "FK_Funerals_Deceaseds_DeceasedId",
                table: "Funerals");

            migrationBuilder.AddForeignKey(
                name: "FK_Casket_Deceaseds_DeceasedId",
                table: "Casket",
                column: "DeceasedId",
                principalTable: "Deceaseds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Funerals_Deceaseds_DeceasedId",
                table: "Funerals",
                column: "DeceasedId",
                principalTable: "Deceaseds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Casket_Deceaseds_DeceasedId",
                table: "Casket");

            migrationBuilder.DropForeignKey(
                name: "FK_Funerals_Deceaseds_DeceasedId",
                table: "Funerals");

            migrationBuilder.AddForeignKey(
                name: "FK_Casket_Deceaseds_DeceasedId",
                table: "Casket",
                column: "DeceasedId",
                principalTable: "Deceaseds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Funerals_Deceaseds_DeceasedId",
                table: "Funerals",
                column: "DeceasedId",
                principalTable: "Deceaseds",
                principalColumn: "Id");
        }
    }
}
