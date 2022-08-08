using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdPlatform.Data.Migrations
{
    public partial class RenameExerciseIdField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Exercise_QuizId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Lessons_LessonId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_IOCases_Exercise_CodeExerciseId",
                table: "IOCases");

            migrationBuilder.RenameColumn(
                name: "CodeExerciseId",
                table: "IOCases",
                newName: "CodeExerciseExeriseId");

            migrationBuilder.RenameIndex(
                name: "IX_IOCases_CodeExerciseId",
                table: "IOCases",
                newName: "IX_IOCases_CodeExerciseExeriseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Exercise",
                newName: "ExeriseId");

            migrationBuilder.RenameColumn(
                name: "QuizId",
                table: "Cases",
                newName: "QuizExeriseId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_QuizId",
                table: "Cases",
                newName: "IX_Cases_QuizExeriseId");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "Exercise",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Exercise_QuizExeriseId",
                table: "Cases",
                column: "QuizExeriseId",
                principalTable: "Exercise",
                principalColumn: "ExeriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Lessons_LessonId",
                table: "Exercise",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IOCases_Exercise_CodeExerciseExeriseId",
                table: "IOCases",
                column: "CodeExerciseExeriseId",
                principalTable: "Exercise",
                principalColumn: "ExeriseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Exercise_QuizExeriseId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Lessons_LessonId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_IOCases_Exercise_CodeExerciseExeriseId",
                table: "IOCases");

            migrationBuilder.RenameColumn(
                name: "CodeExerciseExeriseId",
                table: "IOCases",
                newName: "CodeExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_IOCases_CodeExerciseExeriseId",
                table: "IOCases",
                newName: "IX_IOCases_CodeExerciseId");

            migrationBuilder.RenameColumn(
                name: "ExeriseId",
                table: "Exercise",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "QuizExeriseId",
                table: "Cases",
                newName: "QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_QuizExeriseId",
                table: "Cases",
                newName: "IX_Cases_QuizId");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "Exercise",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Exercise_QuizId",
                table: "Cases",
                column: "QuizId",
                principalTable: "Exercise",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Lessons_LessonId",
                table: "Exercise",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_IOCases_Exercise_CodeExerciseId",
                table: "IOCases",
                column: "CodeExerciseId",
                principalTable: "Exercise",
                principalColumn: "Id");
        }
    }
}
