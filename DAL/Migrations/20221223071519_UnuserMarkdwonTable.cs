using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class UnuserMarkdwonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_markdown_document_block_block_id",
                table: "markdown_document");

            migrationBuilder.DropIndex(
                name: "IX_markdown_document_block_id",
                table: "markdown_document");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("2da7beb7-9ce8-4782-93f3-950171718b58"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("5c925eee-3ff0-4e60-abc7-be366bc2a468"));

            migrationBuilder.AddColumn<string>(
                name: "markdown_document",
                table: "block",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("7ff0e94d-5104-415f-86ca-afe537d8dad9"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$Wtfo.zgikSUJWaA0fKqGtuy1NXrGs9PgeiMAVwRsT9ccqDZ4/weM6", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("be299c50-8d25-4fab-ac1e-f41cc6b82190"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$8FySfp6q5/4zrINhc8xhl.KUPskHJj5UhdNcBIQaj05aCE1rn/2N2", "admin", "sudo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("7ff0e94d-5104-415f-86ca-afe537d8dad9"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("be299c50-8d25-4fab-ac1e-f41cc6b82190"));

            migrationBuilder.DropColumn(
                name: "markdown_document",
                table: "block");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("2da7beb7-9ce8-4782-93f3-950171718b58"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$P.vDkotyXsHKhHDs1vn3OeOJlgPLFR0xMcxMTSTDNvwLPPwpRL1nS", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("5c925eee-3ff0-4e60-abc7-be366bc2a468"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$Sf2.EDUIas5pQ1S/crakXOvAAVk.uqy3KqMt7vPtDfAxWv/764ONy", "admin", "sudo" });

            migrationBuilder.CreateIndex(
                name: "IX_markdown_document_block_id",
                table: "markdown_document",
                column: "block_id");

            migrationBuilder.AddForeignKey(
                name: "FK_markdown_document_block_block_id",
                table: "markdown_document",
                column: "block_id",
                principalTable: "block",
                principalColumn: "id");
        }
    }
}
