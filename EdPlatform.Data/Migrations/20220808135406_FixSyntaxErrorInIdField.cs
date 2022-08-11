using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdPlatform.Data.Migrations
{
    public partial class FixSyntaxErrorInIdField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Exercise_QuizExeriseId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_IOCases_Exercise_CodeExerciseExeriseId",
                table: "IOCases");

            migrationBuilder.RenameColumn(
                name: "CodeExerciseExeriseId",
                table: "IOCases",
                newName: "CodeExerciseExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_IOCases_CodeExerciseExeriseId",
                table: "IOCases",
                newName: "IX_IOCases_CodeExerciseExerciseId");

            migrationBuilder.RenameColumn(
                name: "ExeriseId",
                table: "Exercise",
                newName: "ExerciseId");

            migrationBuilder.RenameColumn(
                name: "QuizExeriseId",
                table: "Cases",
                newName: "QuizExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_QuizExeriseId",
                table: "Cases",
                newName: "IX_Cases_QuizExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Exercise_QuizExerciseId",
                table: "Cases",
                column: "QuizExerciseId",
                principalTable: "Exercise",
                principalColumn: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_IOCases_Exercise_CodeExerciseExerciseId",
                table: "IOCases",
                column: "CodeExerciseExerciseId",
                principalTable: "Exercise",
                principalColumn: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Exercise_QuizExerciseId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_IOCases_Exercise_CodeExerciseExerciseId",
                table: "IOCases");

            migrationBuilder.RenameColumn(
                name: "CodeExerciseExerciseId",
                table: "IOCases",
                newName: "CodeExerciseExeriseId");

            migrationBuilder.RenameIndex(
                name: "IX_IOCases_CodeExerciseExerciseId",
                table: "IOCases",
                newName: "IX_IOCases_CodeExerciseExeriseId");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "Exercise",
                newName: "ExeriseId");

            migrationBuilder.RenameColumn(
                name: "QuizExerciseId",
                table: "Cases",
                newName: "QuizExeriseId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_QuizExerciseId",
                table: "Cases",
                newName: "IX_Cases_QuizExeriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Exercise_QuizExeriseId",
                table: "Cases",
                column: "QuizExeriseId",
                principalTable: "Exercise",
                principalColumn: "ExeriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_IOCases_Exercise_CodeExerciseExeriseId",
                table: "IOCases",
                column: "CodeExerciseExeriseId",
                principalTable: "Exercise",
                principalColumn: "ExeriseId");
        }
    }
}
