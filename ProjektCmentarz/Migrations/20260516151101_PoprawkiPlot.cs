using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class PoprawkiPlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plots_GraveyardSection_GraveyardSectionId",
                table: "Plots");

            migrationBuilder.DropForeignKey(
                name: "FK_Plots_PlotOwners_PlotOwnerId",
                table: "Plots");

            migrationBuilder.AlterColumn<int>(
                name: "PlotOwnerId",
                table: "Plots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GraveyardSectionId",
                table: "Plots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Plots_GraveyardSection_GraveyardSectionId",
                table: "Plots",
                column: "GraveyardSectionId",
                principalTable: "GraveyardSection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plots_PlotOwners_PlotOwnerId",
                table: "Plots",
                column: "PlotOwnerId",
                principalTable: "PlotOwners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plots_GraveyardSection_GraveyardSectionId",
                table: "Plots");

            migrationBuilder.DropForeignKey(
                name: "FK_Plots_PlotOwners_PlotOwnerId",
                table: "Plots");

            migrationBuilder.AlterColumn<int>(
                name: "PlotOwnerId",
                table: "Plots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GraveyardSectionId",
                table: "Plots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Plots_GraveyardSection_GraveyardSectionId",
                table: "Plots",
                column: "GraveyardSectionId",
                principalTable: "GraveyardSection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plots_PlotOwners_PlotOwnerId",
                table: "Plots",
                column: "PlotOwnerId",
                principalTable: "PlotOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
