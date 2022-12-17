using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class FixMacDownDOc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_markdown_document_block_BlockId1",
                table: "markdown_document");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("75fe9ae0-4f90-4a58-a12d-d61b2dcc33b2"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("c699e511-bb5f-4b26-9956-625b0bd6facc"));

            migrationBuilder.RenameColumn(
                name: "BlockId1",
                table: "markdown_document",
                newName: "block_id");

            migrationBuilder.RenameIndex(
                name: "IX_markdown_document_BlockId1",
                table: "markdown_document",
                newName: "IX_markdown_document_block_id");

            migrationBuilder.AddColumn<string>(
                name: "markdown",
                table: "markdown_document",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("638ffa27-f3a0-4b4b-8f99-caea7f2cd9b5"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$R0XRR3ek2TCupNNBwcPbQuxQlEWIDKC5RJrAOLPC3mwaiUXNZGdTq", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("feed1d90-e0aa-450e-8873-473c04a7bdb3"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$JEnxePgootd33GqULXJbVOgMxc1YoX/aTl6/FgLh9gVyB0BaYLFYm", null, "sample4" });

            migrationBuilder.AddForeignKey(
                name: "FK_markdown_document_block_block_id",
                table: "markdown_document",
                column: "block_id",
                principalTable: "block",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_markdown_document_block_block_id",
                table: "markdown_document");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("638ffa27-f3a0-4b4b-8f99-caea7f2cd9b5"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("feed1d90-e0aa-450e-8873-473c04a7bdb3"));

            migrationBuilder.DropColumn(
                name: "markdown",
                table: "markdown_document");

            migrationBuilder.RenameColumn(
                name: "block_id",
                table: "markdown_document",
                newName: "BlockId1");

            migrationBuilder.RenameIndex(
                name: "IX_markdown_document_block_id",
                table: "markdown_document",
                newName: "IX_markdown_document_BlockId1");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("75fe9ae0-4f90-4a58-a12d-d61b2dcc33b2"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$3zCRToQc62T6Apq50FpOfO.SF0EjPbyspm8pIQ3VY42216BTlnDKW", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("c699e511-bb5f-4b26-9956-625b0bd6facc"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$NX/3DbWrDEuFn3Ua5rgsge7i3hcoe7Zzend3irQbmblPgdn0M/AlW", null, "sample4" });

            migrationBuilder.AddForeignKey(
                name: "FK_markdown_document_block_BlockId1",
                table: "markdown_document",
                column: "BlockId1",
                principalTable: "block",
                principalColumn: "id");
        }
    }
}
