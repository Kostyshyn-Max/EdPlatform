using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdPlatform.Data.Migrations
{
    public partial class AddIdToCourseUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseUserId",
                table: "CourseUsers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseUsers",
                table: "CourseUsers",
                column: "CourseUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseUsers",
                table: "CourseUsers");

            migrationBuilder.DropColumn(
                name: "CourseUserId",
                table: "CourseUsers");
        }
    }
}
