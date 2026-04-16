using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class DodanieSectionsDoContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GraveyardSectionId",
                table: "Plots",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GraveyardSection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraveyardSection", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plots_GraveyardSectionId",
                table: "Plots",
                column: "GraveyardSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plots_GraveyardSection_GraveyardSectionId",
                table: "Plots",
                column: "GraveyardSectionId",
                principalTable: "GraveyardSection",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plots_GraveyardSection_GraveyardSectionId",
                table: "Plots");

            migrationBuilder.DropTable(
                name: "GraveyardSection");

            migrationBuilder.DropIndex(
                name: "IX_Plots_GraveyardSectionId",
                table: "Plots");

            migrationBuilder.DropColumn(
                name: "GraveyardSectionId",
                table: "Plots");
        }
    }
}
