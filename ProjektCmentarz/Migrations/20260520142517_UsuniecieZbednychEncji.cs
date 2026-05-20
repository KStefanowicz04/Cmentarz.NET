using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektCmentarz.Migrations
{
    /// <inheritdoc />
    public partial class UsuniecieZbednychEncji : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gravekeepers_Transfers_TransferId",
                table: "Gravekeepers");

            migrationBuilder.DropForeignKey(
                name: "FK_Gravestones_Condition_ConditionId",
                table: "Gravestones");

            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropTable(
                name: "Cremations");

            migrationBuilder.DropTable(
                name: "GraveMaintenances");

            migrationBuilder.DropTable(
                name: "MaintenanceRequests");

            migrationBuilder.DropTable(
                name: "Ownerships");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Gravestones_ConditionId",
                table: "Gravestones");

            migrationBuilder.DropIndex(
                name: "IX_Gravekeepers_TransferId",
                table: "Gravekeepers");

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "Gravestones");

            migrationBuilder.DropColumn(
                name: "TransferId",
                table: "Gravekeepers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConditionId",
                table: "Gravestones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransferId",
                table: "Gravekeepers",
                type: "int",
                nullable: true);

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
                    DeceasedId = table.Column<int>(type: "int", nullable: false),
                    CremationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "MaintenanceRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GraveId = table.Column<int>(type: "int", nullable: false),
                    GravekeepId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "Ownerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactDataId = table.Column<int>(type: "int", nullable: false),
                    GraveId = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlotId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeceasedId = table.Column<int>(type: "int", nullable: false),
                    FromGraveId = table.Column<int>(type: "int", nullable: false),
                    ToGraveId = table.Column<int>(type: "int", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Gravestones_ConditionId",
                table: "Gravestones",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Gravekeepers_TransferId",
                table: "Gravekeepers",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_Cremations_DeceasedId",
                table: "Cremations",
                column: "DeceasedId");

            migrationBuilder.CreateIndex(
                name: "IX_GraveMaintenances_MaintenanceGravekeeperId",
                table: "GraveMaintenances",
                column: "MaintenanceGravekeeperId");

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
                name: "FK_Gravekeepers_Transfers_TransferId",
                table: "Gravekeepers",
                column: "TransferId",
                principalTable: "Transfers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gravestones_Condition_ConditionId",
                table: "Gravestones",
                column: "ConditionId",
                principalTable: "Condition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
