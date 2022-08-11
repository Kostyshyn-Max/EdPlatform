using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdPlatform.Data.Migrations
{
    public partial class FixNamingInIOCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IOCases_Exercise_CodeExerciseExerciseId",
                table: "IOCases");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "IOCases");

            migrationBuilder.AlterColumn<int>(
                name: "CodeExerciseExerciseId",
                table: "IOCases",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IOCases_Exercise_CodeExerciseExerciseId",
                table: "IOCases",
                column: "CodeExerciseExerciseId",
                principalTable: "Exercise",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IOCases_Exercise_CodeExerciseExerciseId",
                table: "IOCases");

            migrationBuilder.AlterColumn<int>(
                name: "CodeExerciseExerciseId",
                table: "IOCases",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseId",
                table: "IOCases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_IOCases_Exercise_CodeExerciseExerciseId",
                table: "IOCases",
                column: "CodeExerciseExerciseId",
                principalTable: "Exercise",
                principalColumn: "ExerciseId");
        }
    }
}
