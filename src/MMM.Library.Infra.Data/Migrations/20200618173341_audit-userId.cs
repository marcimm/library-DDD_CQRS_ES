using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MMM.Library.Infra.Data.Migrations
{
    public partial class audituserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Audit_CreateUserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Audit_LastUpdateUserId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Audit_LastUpdateDate",
                table: "Books",
                newName: "LastUpdateDate");

            migrationBuilder.RenameColumn(
                name: "Audit_CreateDate",
                table: "Books",
                newName: "CreateDate");

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "Books",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdateUser",
                table: "Books",
                type: "varchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LastUpdateUser",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "LastUpdateDate",
                table: "Books",
                newName: "Audit_LastUpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Books",
                newName: "Audit_CreateDate");

            migrationBuilder.AddColumn<Guid>(
                name: "Audit_CreateUserId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Audit_LastUpdateUserId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
