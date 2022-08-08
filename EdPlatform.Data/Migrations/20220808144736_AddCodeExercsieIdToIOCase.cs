using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdPlatform.Data.Migrations
{
    public partial class AddCodeExercsieIdToIOCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InputData",
                table: "IOCases",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseId",
                table: "IOCases",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "IOCases");

            migrationBuilder.AlterColumn<string>(
                name: "InputData",
                table: "IOCases",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
