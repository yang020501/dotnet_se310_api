using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class addmigrationAddedCourseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("7c91d158-dd7a-4f91-88da-ebfc49fc0e05"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("8c1a2b8a-4ca5-4ac0-93ce-76fc528fd8c7"));

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    coursename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lecture_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Course_code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("08ec2a4f-ed05-49cb-8b55-3e53beaf1050"), "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("e076171e-99b2-4c42-af64-5ac71174ec5b"), "admin@pro.org", "Super User Admin", "$2a$11$cjN5rHxfpTknx3fvOaJXTezK3SBn381CRXGWrEkm5HdDm6k22wr6e", "admin", "sudo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("08ec2a4f-ed05-49cb-8b55-3e53beaf1050"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e076171e-99b2-4c42-af64-5ac71174ec5b"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("7c91d158-dd7a-4f91-88da-ebfc49fc0e05"), "admin@pro.org", "Super User Admin", "$2a$11$2Fzq8e1AEscyUIANZopLCOpl6H.qiWVZc5hJFGNY4FfFFGgBLTPJ6", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("8c1a2b8a-4ca5-4ac0-93ce-76fc528fd8c7"), "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });
        }
    }
}
