using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class FixNameOFTableCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("08ec2a4f-ed05-49cb-8b55-3e53beaf1050"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("e076171e-99b2-4c42-af64-5ac71174ec5b"));

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "course");

            migrationBuilder.AddPrimaryKey(
                name: "PK_course",
                table: "course",
                column: "id");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("1f284195-86a2-4f4a-9611-35bb8f0c97f8"), "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("9cc73429-7f96-41d1-a82a-bb0db4e7d3a4"), "admin@pro.org", "Super User Admin", "$2a$11$nGACkMSCIElR1rc.NbDF7udj2jjCx2SfUq9DKM4HMuAlmfIeZZMcK", "admin", "sudo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_course",
                table: "course");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("1f284195-86a2-4f4a-9611-35bb8f0c97f8"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("9cc73429-7f96-41d1-a82a-bb0db4e7d3a4"));

            migrationBuilder.RenameTable(
                name: "course",
                newName: "Courses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "id");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("08ec2a4f-ed05-49cb-8b55-3e53beaf1050"), "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("e076171e-99b2-4c42-af64-5ac71174ec5b"), "admin@pro.org", "Super User Admin", "$2a$11$cjN5rHxfpTknx3fvOaJXTezK3SBn381CRXGWrEkm5HdDm6k22wr6e", "admin", "sudo" });
        }
    }
}
