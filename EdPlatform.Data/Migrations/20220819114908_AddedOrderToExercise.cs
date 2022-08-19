using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdPlatform.Data.Migrations
{
    public partial class AddedOrderToExercise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExerciseName",
                table: "Exercise",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Exercise",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Exercise");

            migrationBuilder.AlterColumn<string>(
                name: "ExerciseName",
                table: "Exercise",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
