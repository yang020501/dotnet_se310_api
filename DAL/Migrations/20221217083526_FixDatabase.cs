using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class FixDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_user_course_course_ref",
                table: "course_user");

            migrationBuilder.DropForeignKey(
                name: "FK_course_user_users_user_ref",
                table: "course_user");

            migrationBuilder.DropTable(
                name: "CourseUser");

            migrationBuilder.DropIndex(
                name: "IX_course_user_user_ref",
                table: "course_user");

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
                values: new object[] { new Guid("75fe9ae0-4f90-4a58-a12d-d61b2dcc33b2"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$3zCRToQc62T6Apq50FpOfO.SF0EjPbyspm8pIQ3VY42216BTlnDKW", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("c699e511-bb5f-4b26-9956-625b0bd6facc"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$NX/3DbWrDEuFn3Ua5rgsge7i3hcoe7Zzend3irQbmblPgdn0M/AlW", null, "sample4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("75fe9ae0-4f90-4a58-a12d-d61b2dcc33b2"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("c699e511-bb5f-4b26-9956-625b0bd6facc"));

            migrationBuilder.CreateTable(
                name: "CourseUser",
                columns: table => new
                {
                    CoursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUser", x => new { x.CoursesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_CourseUser_course_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUser_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("bfadaad9-5452-477f-ac1a-98ff2a48d3ab"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$SHvYyq.wDZxBForvmg1SF.OvfHhYHtf2St.w8veXk7Y56OGqHiFqy", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("c480af04-9a28-42d7-9ebd-eed129f31cc6"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$1N3no/wQiNiL8LQrK3IScuqByBzhTg94ruzR2Qkwf2zuS5coiJH9y", null, "sample4" });

            migrationBuilder.CreateIndex(
                name: "IX_course_user_user_ref",
                table: "course_user",
                column: "user_ref");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUser_UsersId",
                table: "CourseUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_course_user_course_course_ref",
                table: "course_user",
                column: "course_ref",
                principalTable: "course",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_course_user_users_user_ref",
                table: "course_user",
                column: "user_ref",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
