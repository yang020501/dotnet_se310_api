using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddBlockAndMarkdownDoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("334bfb54-ecf8-440a-9dcd-fa1a64817d39"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a8190384-f715-45d1-896f-824e60f5324b"));

            migrationBuilder.CreateTable(
                name: "block",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_block", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "markdown_document",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlockId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_markdown_document", x => x.id);
                    table.ForeignKey(
                        name: "FK_markdown_document_block_BlockId1",
                        column: x => x.BlockId1,
                        principalTable: "block",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("1abf0dc9-74ea-4e69-ac40-8a03129e8fac"), null, "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("47a73024-e720-452f-a55b-c20e7929017e"), null, "admin@pro.org", "Super User Admin", "$2a$11$XMmFzVTwivDobXRXWLyUse7hvH1Xffu4hFYgZ/kzNfMtjRyrNaYAy", "admin", "sudo" });

            migrationBuilder.CreateIndex(
                name: "IX_markdown_document_BlockId1",
                table: "markdown_document",
                column: "BlockId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "markdown_document");

            migrationBuilder.DropTable(
                name: "block");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("1abf0dc9-74ea-4e69-ac40-8a03129e8fac"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("47a73024-e720-452f-a55b-c20e7929017e"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("334bfb54-ecf8-440a-9dcd-fa1a64817d39"), null, "admin@pro.org", "Super User Admin", "$2a$11$UAPTendnfPhZiURvJC9P5ejHMVxQ0N1JYb0fqRYgh08RKIV83aPJm", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("a8190384-f715-45d1-896f-824e60f5324b"), null, "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });
        }
    }
}
