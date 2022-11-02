using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("7c91d158-dd7a-4f91-88da-ebfc49fc0e05"), "admin@pro.org", "Super User Admin", "$2a$11$2Fzq8e1AEscyUIANZopLCOpl6H.qiWVZc5hJFGNY4FfFFGgBLTPJ6", "admin", "sudo" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "full_name", "password", "role", "username" },
                values: new object[] { new Guid("8c1a2b8a-4ca5-4ac0-93ce-76fc528fd8c7"), "sample4@sample.sample", "Sample User Four", "sampass4", null, "sample4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
