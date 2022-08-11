using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdPlatform.Data.Migrations
{
    public partial class AddedExerciseFieldToAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FillExercise_Condition",
                table: "Exercise",
                newName: "Problem");

            migrationBuilder.CreateIndex(
                name: "IX_Attempts_ExerciseId",
                table: "Attempts",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attempts_Exercise_ExerciseId",
                table: "Attempts",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attempts_Exercise_ExerciseId",
                table: "Attempts");

            migrationBuilder.DropIndex(
                name: "IX_Attempts_ExerciseId",
                table: "Attempts");

            migrationBuilder.RenameColumn(
                name: "Problem",
                table: "Exercise",
                newName: "FillExercise_Condition");
        }
    }
}
