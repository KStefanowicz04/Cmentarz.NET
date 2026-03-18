using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class allmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gravekeepers_ContactDatas_GravekeeperContactDataId",
                table: "Gravekeepers");

            migrationBuilder.DropForeignKey(
                name: "FK_Plots_GraveyardSections_GraveyardSectionId",
                table: "Plots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GraveyardSections",
                table: "GraveyardSections");

            migrationBuilder.DropColumn(
                name: "FuneralPlace",
                table: "Funerals");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GraveyardSections");

            migrationBuilder.RenameTable(
                name: "GraveyardSections",
                newName: "Sections");

            migrationBuilder.RenameColumn(
                name: "GravekeeperContactDataId",
                table: "Gravekeepers",
                newName: "ContactDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Gravekeepers_GravekeeperContactDataId",
                table: "Gravekeepers",
                newName: "IX_Gravekeepers_ContactDataId");

            migrationBuilder.AddColumn<int>(
                name: "ParishId",
                table: "Priests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BurialDepthId",
                table: "Graves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransferId",
                table: "Gravekeepers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FuneralHomeId",
                table: "Funerals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlotId",
                table: "Funerals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SectionTypeId",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sections",
                table: "Sections",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BurialDepth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Depth = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurialDepth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cremations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CremationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeceasedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cremations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cremations_Deceaseds_DeceasedId",
                        column: x => x.DeceasedId,
                        principalTable: "Deceaseds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeathCertificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CauseOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Issuer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeceasedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeathCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeathCertificates_Deceaseds_DeceasedId",
                        column: x => x.DeceasedId,
                        principalTable: "Deceaseds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuneralHomeName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuneralHomeName", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GraveId = table.Column<int>(type: "int", nullable: false),
                    GravekeepId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceRequests_Gravekeepers_GravekeepId",
                        column: x => x.GravekeepId,
                        principalTable: "Gravekeepers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintenanceRequests_Graves_GraveId",
                        column: x => x.GraveId,
                        principalTable: "Graves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ownerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GraveId = table.Column<int>(type: "int", nullable: false),
                    ContactDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ownerships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ownerships_ContactDatas_ContactDataId",
                        column: x => x.ContactDataId,
                        principalTable: "ContactDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ownerships_Graves_GraveId",
                        column: x => x.GraveId,
                        principalTable: "Graves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Plots_PlotId",
                        column: x => x.PlotId,
                        principalTable: "Plots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeceasedId = table.Column<int>(type: "int", nullable: false),
                    FromGraveId = table.Column<int>(type: "int", nullable: false),
                    ToGraveId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Deceaseds_DeceasedId",
                        column: x => x.DeceasedId,
                        principalTable: "Deceaseds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfers_Graves_FromGraveId",
                        column: x => x.FromGraveId,
                        principalTable: "Graves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Graves_ToGraveId",
                        column: x => x.ToGraveId,
                        principalTable: "Graves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FuneralHomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuneralHomeNameId = table.Column<int>(type: "int", nullable: false),
                    ContactDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuneralHomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuneralHomes_ContactDatas_ContactDataId",
                        column: x => x.ContactDataId,
                        principalTable: "ContactDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuneralHomes_FuneralHomeName_FuneralHomeNameId",
                        column: x => x.FuneralHomeNameId,
                        principalTable: "FuneralHomeName",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Casket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeceasedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Casket_Deceaseds_DeceasedId",
                        column: x => x.DeceasedId,
                        principalTable: "Deceaseds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Casket_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gravestones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConditionId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    GraveId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gravestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gravestones_Condition_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Condition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gravestones_Graves_GraveId",
                        column: x => x.GraveId,
                        principalTable: "Graves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gravestones_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Priests_ParishId",
                table: "Priests",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX_Graves_BurialDepthId",
                table: "Graves",
                column: "BurialDepthId");

            migrationBuilder.CreateIndex(
                name: "IX_Gravekeepers_TransferId",
                table: "Gravekeepers",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_Funerals_FuneralHomeId",
                table: "Funerals",
                column: "FuneralHomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Funerals_PlotId",
                table: "Funerals",
                column: "PlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_SectionTypeId",
                table: "Sections",
                column: "SectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Casket_DeceasedId",
                table: "Casket",
                column: "DeceasedId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Casket_MaterialId",
                table: "Casket",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Cremations_DeceasedId",
                table: "Cremations",
                column: "DeceasedId");

            migrationBuilder.CreateIndex(
                name: "IX_DeathCertificates_DeceasedId",
                table: "DeathCertificates",
                column: "DeceasedId");

            migrationBuilder.CreateIndex(
                name: "IX_FuneralHomes_ContactDataId",
                table: "FuneralHomes",
                column: "ContactDataId");

            migrationBuilder.CreateIndex(
                name: "IX_FuneralHomes_FuneralHomeNameId",
                table: "FuneralHomes",
                column: "FuneralHomeNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Gravestones_ConditionId",
                table: "Gravestones",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Gravestones_GraveId",
                table: "Gravestones",
                column: "GraveId");

            migrationBuilder.CreateIndex(
                name: "IX_Gravestones_MaterialId",
                table: "Gravestones",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_GraveId",
                table: "MaintenanceRequests",
                column: "GraveId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_GravekeepId",
                table: "MaintenanceRequests",
                column: "GravekeepId");

            migrationBuilder.CreateIndex(
                name: "IX_Ownerships_ContactDataId",
                table: "Ownerships",
                column: "ContactDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Ownerships_GraveId",
                table: "Ownerships",
                column: "GraveId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PlotId",
                table: "Reservations",
                column: "PlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_DeceasedId",
                table: "Transfers",
                column: "DeceasedId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_FromGraveId",
                table: "Transfers",
                column: "FromGraveId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ToGraveId",
                table: "Transfers",
                column: "ToGraveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funerals_FuneralHomes_FuneralHomeId",
                table: "Funerals",
                column: "FuneralHomeId",
                principalTable: "FuneralHomes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Funerals_Plots_PlotId",
                table: "Funerals",
                column: "PlotId",
                principalTable: "Plots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gravekeepers_ContactDatas_ContactDataId",
                table: "Gravekeepers",
                column: "ContactDataId",
                principalTable: "ContactDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gravekeepers_Transfers_TransferId",
                table: "Gravekeepers",
                column: "TransferId",
                principalTable: "Transfers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Graves_BurialDepth_BurialDepthId",
                table: "Graves",
                column: "BurialDepthId",
                principalTable: "BurialDepth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plots_Sections_GraveyardSectionId",
                table: "Plots",
                column: "GraveyardSectionId",
                principalTable: "Sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Priests_Parishes_ParishId",
                table: "Priests",
                column: "ParishId",
                principalTable: "Parishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_SectionType_SectionTypeId",
                table: "Sections",
                column: "SectionTypeId",
                principalTable: "SectionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funerals_FuneralHomes_FuneralHomeId",
                table: "Funerals");

            migrationBuilder.DropForeignKey(
                name: "FK_Funerals_Plots_PlotId",
                table: "Funerals");

            migrationBuilder.DropForeignKey(
                name: "FK_Gravekeepers_ContactDatas_ContactDataId",
                table: "Gravekeepers");

            migrationBuilder.DropForeignKey(
                name: "FK_Gravekeepers_Transfers_TransferId",
                table: "Gravekeepers");

            migrationBuilder.DropForeignKey(
                name: "FK_Graves_BurialDepth_BurialDepthId",
                table: "Graves");

            migrationBuilder.DropForeignKey(
                name: "FK_Plots_Sections_GraveyardSectionId",
                table: "Plots");

            migrationBuilder.DropForeignKey(
                name: "FK_Priests_Parishes_ParishId",
                table: "Priests");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_SectionType_SectionTypeId",
                table: "Sections");

            migrationBuilder.DropTable(
                name: "BurialDepth");

            migrationBuilder.DropTable(
                name: "Casket");

            migrationBuilder.DropTable(
                name: "Cremations");

            migrationBuilder.DropTable(
                name: "DeathCertificates");

            migrationBuilder.DropTable(
                name: "FuneralHomes");

            migrationBuilder.DropTable(
                name: "Gravestones");

            migrationBuilder.DropTable(
                name: "MaintenanceRequests");

            migrationBuilder.DropTable(
                name: "Ownerships");

            migrationBuilder.DropTable(
                name: "Parishes");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "SectionType");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "FuneralHomeName");

            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Priests_ParishId",
                table: "Priests");

            migrationBuilder.DropIndex(
                name: "IX_Graves_BurialDepthId",
                table: "Graves");

            migrationBuilder.DropIndex(
                name: "IX_Gravekeepers_TransferId",
                table: "Gravekeepers");

            migrationBuilder.DropIndex(
                name: "IX_Funerals_FuneralHomeId",
                table: "Funerals");

            migrationBuilder.DropIndex(
                name: "IX_Funerals_PlotId",
                table: "Funerals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sections",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_SectionTypeId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "ParishId",
                table: "Priests");

            migrationBuilder.DropColumn(
                name: "BurialDepthId",
                table: "Graves");

            migrationBuilder.DropColumn(
                name: "TransferId",
                table: "Gravekeepers");

            migrationBuilder.DropColumn(
                name: "FuneralHomeId",
                table: "Funerals");

            migrationBuilder.DropColumn(
                name: "PlotId",
                table: "Funerals");

            migrationBuilder.DropColumn(
                name: "SectionTypeId",
                table: "Sections");

            migrationBuilder.RenameTable(
                name: "Sections",
                newName: "GraveyardSections");

            migrationBuilder.RenameColumn(
                name: "ContactDataId",
                table: "Gravekeepers",
                newName: "GravekeeperContactDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Gravekeepers_ContactDataId",
                table: "Gravekeepers",
                newName: "IX_Gravekeepers_GravekeeperContactDataId");

            migrationBuilder.AddColumn<string>(
                name: "FuneralPlace",
                table: "Funerals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GraveyardSections",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GraveyardSections",
                table: "GraveyardSections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gravekeepers_ContactDatas_GravekeeperContactDataId",
                table: "Gravekeepers",
                column: "GravekeeperContactDataId",
                principalTable: "ContactDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plots_GraveyardSections_GraveyardSectionId",
                table: "Plots",
                column: "GraveyardSectionId",
                principalTable: "GraveyardSections",
                principalColumn: "Id");
        }
    }
}
