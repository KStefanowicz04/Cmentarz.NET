using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class poprawarelacjinieboszczyka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funerals_Deceaseds_DeceasedId",
                table: "Funerals");

            migrationBuilder.AddForeignKey(
                name: "FK_Funerals_Deceaseds_DeceasedId",
                table: "Funerals",
                column: "DeceasedId",
                principalTable: "Deceaseds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funerals_Deceaseds_DeceasedId",
                table: "Funerals");

            migrationBuilder.AddForeignKey(
                name: "FK_Funerals_Deceaseds_DeceasedId",
                table: "Funerals",
                column: "DeceasedId",
                principalTable: "Deceaseds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
