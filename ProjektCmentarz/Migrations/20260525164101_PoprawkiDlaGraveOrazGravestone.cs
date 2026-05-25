using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class PoprawkiDlaGraveOrazGravestone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Gravestones_GraveId",
                table: "Gravestones");

            migrationBuilder.AddColumn<int>(
                name: "GravestoneInscryptionId",
                table: "Gravestones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GravestoneId",
                table: "Graves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Gravestones_GraveId",
                table: "Gravestones",
                column: "GraveId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gravestones_GravestoneInscryptionId",
                table: "Gravestones",
                column: "GravestoneInscryptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gravestones_GravestoneInscryptions_GravestoneInscryptionId",
                table: "Gravestones",
                column: "GravestoneInscryptionId",
                principalTable: "GravestoneInscryptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gravestones_GravestoneInscryptions_GravestoneInscryptionId",
                table: "Gravestones");

            migrationBuilder.DropIndex(
                name: "IX_Gravestones_GraveId",
                table: "Gravestones");

            migrationBuilder.DropIndex(
                name: "IX_Gravestones_GravestoneInscryptionId",
                table: "Gravestones");

            migrationBuilder.DropColumn(
                name: "GravestoneInscryptionId",
                table: "Gravestones");

            migrationBuilder.DropColumn(
                name: "GravestoneId",
                table: "Graves");

            migrationBuilder.CreateIndex(
                name: "IX_Gravestones_GraveId",
                table: "Gravestones",
                column: "GraveId");
        }
    }
}
