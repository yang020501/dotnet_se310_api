using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddCourseIDColumnToBlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("638ffa27-f3a0-4b4b-8f99-caea7f2cd9b5"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("feed1d90-e0aa-450e-8873-473c04a7bdb3"));

            migrationBuilder.AddColumn<Guid>(
                name: "course_id",
                table: "block",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("13230ecb-c59c-42e3-8edc-9a7354a02e9b"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$v3c7Dso9AIy1cQYPIaG29uIv7uAaGUL/BW6eVdzEUH1WMLvrwueea", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("2aa0400d-bd4b-4157-b070-e6015cba513a"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$O95vhhUDuqPd4g18OBz0mejh7QMbS6gf8Y98TvVpFBc.uhkoSNEjG", null, "sample4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("13230ecb-c59c-42e3-8edc-9a7354a02e9b"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("2aa0400d-bd4b-4157-b070-e6015cba513a"));

            migrationBuilder.DropColumn(
                name: "course_id",
                table: "block");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("638ffa27-f3a0-4b4b-8f99-caea7f2cd9b5"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$R0XRR3ek2TCupNNBwcPbQuxQlEWIDKC5RJrAOLPC3mwaiUXNZGdTq", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("feed1d90-e0aa-450e-8873-473c04a7bdb3"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$JEnxePgootd33GqULXJbVOgMxc1YoX/aTl6/FgLh9gVyB0BaYLFYm", null, "sample4" });
        }
    }
}
