using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddAvatarToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("68e688e0-b7ca-4f58-bb44-c00608958e1d"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f7a859f5-a146-4ae9-9bd8-8d1a2ad45302"));

            migrationBuilder.AddColumn<string>(
                name: "avatar",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("334bfb54-ecf8-440a-9dcd-fa1a64817d39"), null, "admin@pro.org", "Super User Admin", "$2a$11$UAPTendnfPhZiURvJC9P5ejHMVxQ0N1JYb0fqRYgh08RKIV83aPJm", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("a8190384-f715-45d1-896f-824e60f5324b"), null, "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("334bfb54-ecf8-440a-9dcd-fa1a64817d39"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a8190384-f715-45d1-896f-824e60f5324b"));

            migrationBuilder.DropColumn(
                name: "avatar",
                table: "users");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("68e688e0-b7ca-4f58-bb44-c00608958e1d"), "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("f7a859f5-a146-4ae9-9bd8-8d1a2ad45302"), "admin@pro.org", "Super User Admin", "$2a$11$O5PJ1Pv6yQ970Rj8s8iO0.0e03qmWw/pL3LhNSb49vXCEs08Sq4qK", "admin", "sudo" });
        }
    }
}
