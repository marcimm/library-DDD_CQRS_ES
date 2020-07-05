using Microsoft.EntityFrameworkCore.Migrations;

namespace MMM.Library.Infra.Data.Migrations
{
    public partial class audit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Audit_LastUpdateUserId",
                table: "Books",
                newName: "LastUpdateUserId");

            migrationBuilder.RenameColumn(
                name: "Audit_LastUpdateDate",
                table: "Books",
                newName: "LastUpdateDate");

            migrationBuilder.RenameColumn(
                name: "Audit_CreateUserId",
                table: "Books",
                newName: "CreateUserId");

            migrationBuilder.RenameColumn(
                name: "Audit_CreateDate",
                table: "Books",
                newName: "CreateDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdateUserId",
                table: "Books",
                newName: "Audit_LastUpdateUserId");

            migrationBuilder.RenameColumn(
                name: "LastUpdateDate",
                table: "Books",
                newName: "Audit_LastUpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreateUserId",
                table: "Books",
                newName: "Audit_CreateUserId");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Books",
                newName: "Audit_CreateDate");
        }
    }
}
