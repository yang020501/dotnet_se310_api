using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class FixMarkdownDocColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new Guid("8ec2e7a4-7458-4227-b9bf-29aabb9836b1"), null, "admin@pro.org", "Super User Admin", "$2a$11$uuJ/BGMNJR2tVXtBUQwxVOaEbVbHvuK52jVNKxjs6HThtguEJMh46", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("c1002e10-a7bf-48c2-8bd8-f75964e6b42c"), null, "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("8ec2e7a4-7458-4227-b9bf-29aabb9836b1"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("c1002e10-a7bf-48c2-8bd8-f75964e6b42c"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("1abf0dc9-74ea-4e69-ac40-8a03129e8fac"), null, "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("47a73024-e720-452f-a55b-c20e7929017e"), null, "admin@pro.org", "Super User Admin", "$2a$11$XMmFzVTwivDobXRXWLyUse7hvH1Xffu4hFYgZ/kzNfMtjRyrNaYAy", "admin", "sudo" });
        }
    }
}
