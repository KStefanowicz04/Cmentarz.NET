using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deceaseds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeathDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deceaseds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GraveyardSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraveyardSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gravekeepers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GravekeeperContactDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gravekeepers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gravekeepers_ContactDatas_GravekeeperContactDataId",
                        column: x => x.GravekeeperContactDataId,
                        principalTable: "ContactDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlotOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlotOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlotOwners_ContactDatas_ContactDataId",
                        column: x => x.ContactDataId,
                        principalTable: "ContactDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Priests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Priests_ContactDatas_ContactDataId",
                        column: x => x.ContactDataId,
                        principalTable: "ContactDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GraveMaintenances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenanceGravekeeperId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraveMaintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GraveMaintenances_Gravekeepers_MaintenanceGravekeeperId",
                        column: x => x.MaintenanceGravekeeperId,
                        principalTable: "Gravekeepers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlotOwnerId = table.Column<int>(type: "int", nullable: false),
                    GraveyardSectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plots_GraveyardSections_GraveyardSectionId",
                        column: x => x.GraveyardSectionId,
                        principalTable: "GraveyardSections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Plots_PlotOwners_PlotOwnerId",
                        column: x => x.PlotOwnerId,
                        principalTable: "PlotOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funerals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuneralDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FuneralPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeceasedId = table.Column<int>(type: "int", nullable: false),
                    PriestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funerals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funerals_Deceaseds_DeceasedId",
                        column: x => x.DeceasedId,
                        principalTable: "Deceaseds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funerals_Priests_PriestId",
                        column: x => x.PriestId,
                        principalTable: "Priests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Graves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlotId = table.Column<int>(type: "int", nullable: false),
                    DeceasedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Graves_Deceaseds_DeceasedId",
                        column: x => x.DeceasedId,
                        principalTable: "Deceaseds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Graves_Plots_PlotId",
                        column: x => x.PlotId,
                        principalTable: "Plots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuneralGravekeeper",
                columns: table => new
                {
                    FuneralId = table.Column<int>(type: "int", nullable: false),
                    GravekeeperId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuneralGravekeeper", x => new { x.FuneralId, x.GravekeeperId });
                    table.ForeignKey(
                        name: "FK_FuneralGravekeeper_Funerals_FuneralId",
                        column: x => x.FuneralId,
                        principalTable: "Funerals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FuneralGravekeeper_Gravekeepers_GravekeeperId",
                        column: x => x.GravekeeperId,
                        principalTable: "Gravekeepers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuneralGravekeeper_GravekeeperId",
                table: "FuneralGravekeeper",
                column: "GravekeeperId");

            migrationBuilder.CreateIndex(
                name: "IX_Funerals_DeceasedId",
                table: "Funerals",
                column: "DeceasedId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funerals_PriestId",
                table: "Funerals",
                column: "PriestId");

            migrationBuilder.CreateIndex(
                name: "IX_Gravekeepers_GravekeeperContactDataId",
                table: "Gravekeepers",
                column: "GravekeeperContactDataId");

            migrationBuilder.CreateIndex(
                name: "IX_GraveMaintenances_MaintenanceGravekeeperId",
                table: "GraveMaintenances",
                column: "MaintenanceGravekeeperId");

            migrationBuilder.CreateIndex(
                name: "IX_Graves_DeceasedId",
                table: "Graves",
                column: "DeceasedId");

            migrationBuilder.CreateIndex(
                name: "IX_Graves_PlotId",
                table: "Graves",
                column: "PlotId");

            migrationBuilder.CreateIndex(
                name: "IX_PlotOwners_ContactDataId",
                table: "PlotOwners",
                column: "ContactDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Plots_GraveyardSectionId",
                table: "Plots",
                column: "GraveyardSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Plots_PlotOwnerId",
                table: "Plots",
                column: "PlotOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Priests_ContactDataId",
                table: "Priests",
                column: "ContactDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuneralGravekeeper");

            migrationBuilder.DropTable(
                name: "GraveMaintenances");

            migrationBuilder.DropTable(
                name: "Graves");

            migrationBuilder.DropTable(
                name: "Funerals");

            migrationBuilder.DropTable(
                name: "Gravekeepers");

            migrationBuilder.DropTable(
                name: "Plots");

            migrationBuilder.DropTable(
                name: "Deceaseds");

            migrationBuilder.DropTable(
                name: "Priests");

            migrationBuilder.DropTable(
                name: "GraveyardSections");

            migrationBuilder.DropTable(
                name: "PlotOwners");

            migrationBuilder.DropTable(
                name: "ContactDatas");
        }
    }
}
