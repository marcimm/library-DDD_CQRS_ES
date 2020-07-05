using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MMM.Library.Infra.Data.Migrations
{
    public partial class stock2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Stocks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "Stocks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Stocks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdateUser",
                table: "Stocks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "LastUpdateUser",
                table: "Stocks");
        }
    }
}
