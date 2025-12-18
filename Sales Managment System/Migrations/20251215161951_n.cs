using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales_Managment_System.Migrations
{
    /// <inheritdoc />
    public partial class n : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tr_daily_services_ServiceId",
                table: "tr_daily");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "tr_daily",
                type: "RAW(16)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "RAW(16)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tr_daily_services_ServiceId",
                table: "tr_daily",
                column: "ServiceId",
                principalTable: "services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tr_daily_services_ServiceId",
                table: "tr_daily");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "tr_daily",
                type: "RAW(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "RAW(16)");

            migrationBuilder.AddForeignKey(
                name: "FK_tr_daily_services_ServiceId",
                table: "tr_daily",
                column: "ServiceId",
                principalTable: "services",
                principalColumn: "Id");
        }
    }
}
