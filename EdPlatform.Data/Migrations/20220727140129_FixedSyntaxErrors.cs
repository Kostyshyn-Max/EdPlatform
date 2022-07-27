using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdPlatform.Data.Migrations
{
    public partial class FixedSyntaxErrors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atempts");

            migrationBuilder.RenameColumn(
                name: "ExerciseWithAnswer_Condition",
                table: "Exercise",
                newName: "FillExercise_Condition");

            migrationBuilder.CreateTable(
                name: "Attempts",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    UserAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempts", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attempts");

            migrationBuilder.RenameColumn(
                name: "FillExercise_Condition",
                table: "Exercise",
                newName: "ExerciseWithAnswer_Condition");

            migrationBuilder.CreateTable(
                name: "Atempts",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    UserAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atempts", x => x.UserId);
                });
        }
    }
}
