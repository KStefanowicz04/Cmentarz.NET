using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class PoprawionoDeathCertificateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CauseOfDeath",
                table: "DeathCertificates");

            migrationBuilder.AddColumn<int>(
                name: "CauseOfDeathId",
                table: "DeathCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeathCertificates_CauseOfDeathId",
                table: "DeathCertificates",
                column: "CauseOfDeathId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeathCertificates_CauseOfDeath_CauseOfDeathId",
                table: "DeathCertificates",
                column: "CauseOfDeathId",
                principalTable: "CauseOfDeath",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeathCertificates_CauseOfDeath_CauseOfDeathId",
                table: "DeathCertificates");

            migrationBuilder.DropIndex(
                name: "IX_DeathCertificates_CauseOfDeathId",
                table: "DeathCertificates");

            migrationBuilder.DropColumn(
                name: "CauseOfDeathId",
                table: "DeathCertificates");

            migrationBuilder.AddColumn<string>(
                name: "CauseOfDeath",
                table: "DeathCertificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
