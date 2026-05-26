using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class DodanoCenyDzialek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlotValue",
                table: "Plots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Payments",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "PlotId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlotOwnerId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PlotId",
                table: "Payments",
                column: "PlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PlotOwnerId",
                table: "Payments",
                column: "PlotOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PlotOwners_PlotOwnerId",
                table: "Payments",
                column: "PlotOwnerId",
                principalTable: "PlotOwners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Plots_PlotId",
                table: "Payments",
                column: "PlotId",
                principalTable: "Plots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PlotOwners_PlotOwnerId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Plots_PlotId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PlotId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PlotOwnerId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PlotValue",
                table: "Plots");

            migrationBuilder.DropColumn(
                name: "PlotId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PlotOwnerId",
                table: "Payments");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
