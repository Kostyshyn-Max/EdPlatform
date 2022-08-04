using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdPlatform.Data.Migrations
{
    public partial class AddUserJoinedCountToCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersJoined",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsersJoined",
                table: "Courses");
        }
    }
}
