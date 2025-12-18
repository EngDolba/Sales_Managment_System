using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales_Managment_System.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dailyReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Date = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    Sum = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CarNumbers = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dailyReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ServiceName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ServiceType = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ServicePrice = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tr_daily",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Time = table.Column<string>(type: "NVARCHAR2(48)", nullable: true),
                    ServiceId = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    CarNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tr_daily", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tr_daily_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tr_hist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TransactionId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Time = table.Column<string>(type: "NVARCHAR2(48)", nullable: true),
                    ServiceId = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    CarNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Date = table.Column<string>(type: "NVARCHAR2(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tr_hist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tr_hist_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tr_daily_ServiceId",
                table: "tr_daily",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tr_hist_ServiceId",
                table: "tr_hist",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dailyReports");

            migrationBuilder.DropTable(
                name: "tr_daily");

            migrationBuilder.DropTable(
                name: "tr_hist");

            migrationBuilder.DropTable(
                name: "services");
        }
    }
}
