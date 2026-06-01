using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class PoprawaDeathCert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Issuer",
                table: "DeathCertificates");

            migrationBuilder.AddColumn<int>(
                name: "IssuerId",
                table: "DeathCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeathCertificates_IssuerId",
                table: "DeathCertificates",
                column: "IssuerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeathCertificates_FuneralHomes_IssuerId",
                table: "DeathCertificates",
                column: "IssuerId",
                principalTable: "FuneralHomes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeathCertificates_FuneralHomes_IssuerId",
                table: "DeathCertificates");

            migrationBuilder.DropIndex(
                name: "IX_DeathCertificates_IssuerId",
                table: "DeathCertificates");

            migrationBuilder.DropColumn(
                name: "IssuerId",
                table: "DeathCertificates");

            migrationBuilder.AddColumn<string>(
                name: "Issuer",
                table: "DeathCertificates",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
