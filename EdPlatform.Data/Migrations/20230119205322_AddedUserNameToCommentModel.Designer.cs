﻿// <auto-generated />
using EdPlatform.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EdPlatform.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230119205322_AddedUserNameToCommentModel")]
    partial class AddedUserNameToCommentModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EdPlatform.Data.Entities.Attempt", b =>
                {
                    b.Property<int>("AttemptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AttemptId"));

                    b.Property<int>("ExerciseId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<string>("UserAnswer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("AttemptId");

                    b.HasIndex("ExerciseId");

                    b.ToTable("Attempts");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Case", b =>
                {
                    b.Property<int>("CaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CaseId"));

                    b.Property<string>("CaseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<int>("QuizExerciseId")
                        .HasColumnType("integer");

                    b.HasKey("CaseId");

                    b.HasIndex("QuizExerciseId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CommentId"));

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<int>("RateStarsCount")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CommentId");

                    b.HasIndex("CourseId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CourseId"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UsersJoined")
                        .HasColumnType("integer");

                    b.HasKey("CourseId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.CourseUser", b =>
                {
                    b.Property<int>("CourseUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CourseUserId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("CourseUserId");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseUsers");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Exercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ExerciseId"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExerciseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LessonId")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<string>("Problem")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ExerciseId");

                    b.HasIndex("LessonId");

                    b.ToTable("Exercise");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Exercise");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.IOCase", b =>
                {
                    b.Property<int>("IOCaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IOCaseId"));

                    b.Property<int>("CodeExerciseExerciseId")
                        .HasColumnType("integer");

                    b.Property<string>("InputData")
                        .HasColumnType("text");

                    b.Property<string>("OutputData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IOCaseId");

                    b.HasIndex("CodeExerciseExerciseId");

                    b.ToTable("IOCases");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Lesson", b =>
                {
                    b.Property<int>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LessonId"));

                    b.Property<string>("LessonContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LessonName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ModuleId")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("text");

                    b.HasKey("LessonId");

                    b.HasIndex("ModuleId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Module", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ModuleId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("ModuleId");

                    b.HasIndex("CourseId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.CodeExercise", b =>
                {
                    b.HasBaseType("EdPlatform.Data.Entities.Exercise");

                    b.HasDiscriminator().HasValue("CodeExercise");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.FillExercise", b =>
                {
                    b.HasBaseType("EdPlatform.Data.Entities.Exercise");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("FillExercise");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Quiz", b =>
                {
                    b.HasBaseType("EdPlatform.Data.Entities.Exercise");

                    b.HasDiscriminator().HasValue("Quiz");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Attempt", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Case", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.Quiz", null)
                        .WithMany("Cases")
                        .HasForeignKey("QuizExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Comment", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.Course", "Course")
                        .WithMany("Comments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Course", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.CourseUser", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.Course", "Course")
                        .WithMany("CourseUsers")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Exercise", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.Lesson", "Lesson")
                        .WithMany("Exercises")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.IOCase", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.CodeExercise", null)
                        .WithMany("IOCases")
                        .HasForeignKey("CodeExerciseExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Lesson", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.Module", "Module")
                        .WithMany("Lessons")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Module", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.Course", "Course")
                        .WithMany("Modules")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Course", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("CourseUsers");

                    b.Navigation("Modules");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Lesson", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Module", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.CodeExercise", b =>
                {
                    b.Navigation("IOCases");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Quiz", b =>
                {
                    b.Navigation("Cases");
                });
#pragma warning restore 612, 618
        }
    }
}
