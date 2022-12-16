using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddDateOfBirthToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("78123461-35b0-4508-a6b9-30b9c32ae5c4"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("c4cd6af4-4d03-40a7-b391-88c787eff3ea"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("bfadaad9-5452-477f-ac1a-98ff2a48d3ab"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$SHvYyq.wDZxBForvmg1SF.OvfHhYHtf2St.w8veXk7Y56OGqHiFqy", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("c480af04-9a28-42d7-9ebd-eed129f31cc6"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$1N3no/wQiNiL8LQrK3IScuqByBzhTg94ruzR2Qkwf2zuS5coiJH9y", null, "sample4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("bfadaad9-5452-477f-ac1a-98ff2a48d3ab"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("c480af04-9a28-42d7-9ebd-eed129f31cc6"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("78123461-35b0-4508-a6b9-30b9c32ae5c4"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$jM/v6ucu2V5.24wFn0BQ3.iqUNd7Cv2Sg4W/xFmrwrxU1P50i4igK", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("c4cd6af4-4d03-40a7-b391-88c787eff3ea"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$eTD3PAVpJv8Lqi88uUHS/OW5J6TvOt3iD93L2Sf5nvE./qO1lavkm", null, "sample4" });
        }
    }
}
