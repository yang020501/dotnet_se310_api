using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddDateOfWeekToCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("10815fc8-38f1-4305-89e8-c0638e1e58a0"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("d965a870-1888-48ac-a872-aba2939c693c"));

            migrationBuilder.AddColumn<int>(
                name: "date_of_week",
                table: "course",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("060ab550-d22e-49f0-b3b3-33e04b315ce9"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$O1fK7.yeK337ylAelzDakex253p.05ZtHre9sMFwZmKUyNTKo/GNW", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("4f0caf7a-3386-4fa6-9eb2-c536f5b6d4b3"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$YJadVDVIPAbJ4ZVtwXsTbuQNCpN2Cjv7ooBbB0ATXbi0INGqe1bC6", "admin", "sudo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("060ab550-d22e-49f0-b3b3-33e04b315ce9"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("4f0caf7a-3386-4fa6-9eb2-c536f5b6d4b3"));

            migrationBuilder.DropColumn(
                name: "date_of_week",
                table: "course");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("10815fc8-38f1-4305-89e8-c0638e1e58a0"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$YDO5BSgjzN5ta8yjHk.xcefYGE4OKG3Y5Mzkvo74skTFFDBJpGHDe", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("d965a870-1888-48ac-a872-aba2939c693c"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$nEmhpH6YxEBdCsfBJTdyT.22WqJlfH8oYmyBNiAkcHdQ.LrD4G77m", "admin", "sudo" });
        }
    }
}
