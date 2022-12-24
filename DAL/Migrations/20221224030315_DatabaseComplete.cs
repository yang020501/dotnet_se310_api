using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class DatabaseComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "block",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    block_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    course_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    markdown_document = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_block", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    coursename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lecture_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    course_code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.id);
                });

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
                });

            migrationBuilder.CreateTable(
                name: "markdown_document",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    block_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    markdown = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_markdown_document", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("20c3dbf0-b2ca-4c47-8698-1fff7f278e91"), null, null, "admin@pro.org", "Super User Admin", "$2a$11$lkG3/XbZKJPFP8i1kGcbzuk38gHCPaywdwl96qosvWyjzrvfk17YO", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("f2e65f73-bc32-4240-b422-120518a64200"), null, null, "sample4@sample.sample", "Sample User Four", "$2a$11$1yF9bNKU9GajC8cQviN/nuidJQbJlFu0JVPcpu.m7.hsLOig1YIsa", null, "sample4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "block");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "course_user");

            migrationBuilder.DropTable(
                name: "markdown_document");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
