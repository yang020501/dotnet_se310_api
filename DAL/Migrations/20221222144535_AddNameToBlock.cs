using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddNameToBlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("13230ecb-c59c-42e3-8edc-9a7354a02e9b"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("2aa0400d-bd4b-4157-b070-e6015cba513a"));

            migrationBuilder.AddColumn<string>(
                name: "block_name",
                table: "block",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("2da7beb7-9ce8-4782-93f3-950171718b58"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$P.vDkotyXsHKhHDs1vn3OeOJlgPLFR0xMcxMTSTDNvwLPPwpRL1nS", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("5c925eee-3ff0-4e60-abc7-be366bc2a468"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$Sf2.EDUIas5pQ1S/crakXOvAAVk.uqy3KqMt7vPtDfAxWv/764ONy", "admin", "sudo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("2da7beb7-9ce8-4782-93f3-950171718b58"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("5c925eee-3ff0-4e60-abc7-be366bc2a468"));

            migrationBuilder.DropColumn(
                name: "block_name",
                table: "block");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("13230ecb-c59c-42e3-8edc-9a7354a02e9b"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$v3c7Dso9AIy1cQYPIaG29uIv7uAaGUL/BW6eVdzEUH1WMLvrwueea", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("2aa0400d-bd4b-4157-b070-e6015cba513a"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$O95vhhUDuqPd4g18OBz0mejh7QMbS6gf8Y98TvVpFBc.uhkoSNEjG", null, "sample4" });
        }
    }
}
