using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopSolution.Data.Migrations
{
    public partial class updateimagedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 6, 16, 14, 53, 151, DateTimeKind.Local).AddTicks(8836),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 6, 11, 38, 53, 123, DateTimeKind.Local).AddTicks(2653));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 6, 11, 38, 53, 123, DateTimeKind.Local).AddTicks(2653),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 6, 16, 14, 53, 151, DateTimeKind.Local).AddTicks(8836));
        }
    }
}
