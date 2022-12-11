using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddCousrUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("1f284195-86a2-4f4a-9611-35bb8f0c97f8"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("9cc73429-7f96-41d1-a82a-bb0db4e7d3a4"));

            migrationBuilder.RenameColumn(
                name: "Course_code",
                table: "course",
                newName: "course_code");

            migrationBuilder.CreateTable(
                name: "course_user",
                columns: table => new
                {
                    user_ref = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    course_ref = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_user", x => new { x.course_ref, x.user_ref });
                    table.ForeignKey(
                        name: "FK_course_user_course_course_ref",
                        column: x => x.course_ref,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_user_users_user_ref",
                        column: x => x.user_ref,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("68e688e0-b7ca-4f58-bb44-c00608958e1d"), "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("f7a859f5-a146-4ae9-9bd8-8d1a2ad45302"), "admin@pro.org", "Super User Admin", "$2a$11$O5PJ1Pv6yQ970Rj8s8iO0.0e03qmWw/pL3LhNSb49vXCEs08Sq4qK", "admin", "sudo" });

            migrationBuilder.CreateIndex(
                name: "IX_course_user_user_ref",
                table: "course_user",
                column: "user_ref");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUser_UsersId",
                table: "CourseUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_user");

            migrationBuilder.DropTable(
                name: "CourseUser");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("68e688e0-b7ca-4f58-bb44-c00608958e1d"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f7a859f5-a146-4ae9-9bd8-8d1a2ad45302"));

            migrationBuilder.RenameColumn(
                name: "course_code",
                table: "course",
                newName: "Course_code");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("1f284195-86a2-4f4a-9611-35bb8f0c97f8"), "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("9cc73429-7f96-41d1-a82a-bb0db4e7d3a4"), "admin@pro.org", "Super User Admin", "$2a$11$nGACkMSCIElR1rc.NbDF7udj2jjCx2SfUq9DKM4HMuAlmfIeZZMcK", "admin", "sudo" });
        }
    }
}
