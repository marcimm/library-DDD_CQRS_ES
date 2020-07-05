using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MMM.Library.Infra.Data.Migrations
{
    public partial class cqrs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "BookingCode",
                startValue: 10000L);

            migrationBuilder.CreateSequence<int>(
                name: "BookingItemCode",
                startValue: 10L,
                incrementBy: 10);

            migrationBuilder.AlterSequence(
                name: "StockCode",
                incrementBy: 2);

            migrationBuilder.RestartSequence(
                name: "StockCode",
                startValue: 1000L);

            migrationBuilder.RestartSequence(
                name: "CategoryCode",
                startValue: 100L);

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BookingCode = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR BookingCode"),
                    UserId = table.Column<Guid>(nullable: false),
                    RegisterDay = table.Column<DateTime>(nullable: false),
                    EBookingStatus = table.Column<int>(nullable: false),
                    Fine = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StockId = table.Column<Guid>(nullable: false),
                    BookingId = table.Column<Guid>(nullable: false),
                    BookingItemCode = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR BookingItemCode"),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingItems_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingItems_BookingId",
                table: "BookingItems",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingItems");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropSequence(
                name: "BookingCode");

            migrationBuilder.DropSequence(
                name: "BookingItemCode");

            migrationBuilder.AlterSequence(
                name: "StockCode",
                oldIncrementBy: 2);

            migrationBuilder.RestartSequence(
                name: "StockCode",
                startValue: 10001L);

            migrationBuilder.RestartSequence(
                name: "CategoryCode",
                startValue: 1002L);
        }
    }
}
