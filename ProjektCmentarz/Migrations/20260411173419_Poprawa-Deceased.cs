using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class PoprawaDeceased : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CasketId",
                table: "Deceaseds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FuneralId",
                table: "Deceaseds",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CasketId",
                table: "Deceaseds");

            migrationBuilder.DropColumn(
                name: "FuneralId",
                table: "Deceaseds");
        }
    }
}
