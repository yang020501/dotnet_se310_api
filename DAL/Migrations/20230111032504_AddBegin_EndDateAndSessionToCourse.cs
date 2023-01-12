using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddBegin_EndDateAndSessionToCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("20c3dbf0-b2ca-4c47-8698-1fff7f278e91"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f2e65f73-bc32-4240-b422-120518a64200"));

            migrationBuilder.AddColumn<DateTime>(
                name: "begin_date",
                table: "course",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "end_date",
                table: "course",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "session",
                table: "course",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("10815fc8-38f1-4305-89e8-c0638e1e58a0"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$YDO5BSgjzN5ta8yjHk.xcefYGE4OKG3Y5Mzkvo74skTFFDBJpGHDe", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("d965a870-1888-48ac-a872-aba2939c693c"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$nEmhpH6YxEBdCsfBJTdyT.22WqJlfH8oYmyBNiAkcHdQ.LrD4G77m", "admin", "sudo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("10815fc8-38f1-4305-89e8-c0638e1e58a0"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("d965a870-1888-48ac-a872-aba2939c693c"));

            migrationBuilder.DropColumn(
                name: "begin_date",
                table: "course");

            migrationBuilder.DropColumn(
                name: "end_date",
                table: "course");

            migrationBuilder.DropColumn(
                name: "session",
                table: "course");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("20c3dbf0-b2ca-4c47-8698-1fff7f278e91"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$lkG3/XbZKJPFP8i1kGcbzuk38gHCPaywdwl96qosvWyjzrvfk17YO", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("f2e65f73-bc32-4240-b422-120518a64200"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$1yF9bNKU9GajC8cQviN/nuidJQbJlFu0JVPcpu.m7.hsLOig1YIsa", null, "sample4" });
        }
    }
}
