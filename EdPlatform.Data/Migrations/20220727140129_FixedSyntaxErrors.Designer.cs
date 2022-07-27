﻿// <auto-generated />
using System;
using EdPlatform.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EdPlatform.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220727140129_FixedSyntaxErrors")]
    partial class FixedSyntaxErrors
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EdPlatform.Data.Entities.Attempt", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("UserAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Attempts");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Case", b =>
                {
                    b.Property<int>("CaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CaseId"), 1L, 1);

                    b.Property<string>("CaseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int?>("QuizId")
                        .HasColumnType("int");

                    b.HasKey("CaseId");

                    b.HasIndex("QuizId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"), 1L, 1);

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.CourseUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("CourseUsers");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExerciseName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("LessonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("Exercise");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Exercise");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.IOCase", b =>
                {
                    b.Property<int>("IOCaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IOCaseId"), 1L, 1);

                    b.Property<int?>("CodeExerciseId")
                        .HasColumnType("int");

                    b.Property<string>("InputData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OutputData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IOCaseId");

                    b.HasIndex("CodeExerciseId");

                    b.ToTable("IOCases");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Lesson", b =>
                {
                    b.Property<int>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LessonId"), 1L, 1);

                    b.Property<string>("LessonContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LessonName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LessonId");

                    b.HasIndex("ModuleId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Module", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModuleId"), 1L, 1);

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("ModuleId");

                    b.HasIndex("CourseId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.CodeExercise", b =>
                {
                    b.HasBaseType("EdPlatform.Data.Entities.Exercise");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("CodeExercise");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.FillExercise", b =>
                {
                    b.HasBaseType("EdPlatform.Data.Entities.Exercise");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FillExercise_Condition");

                    b.HasDiscriminator().HasValue("FillExercise");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Quiz", b =>
                {
                    b.HasBaseType("EdPlatform.Data.Entities.Exercise");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Quiz_Condition");

                    b.HasDiscriminator().HasValue("Quiz");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Case", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.Quiz", null)
                        .WithMany("Cases")
                        .HasForeignKey("QuizId");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Exercise", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.Lesson", null)
                        .WithMany("Exercises")
                        .HasForeignKey("LessonId");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.IOCase", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.CodeExercise", null)
                        .WithMany("IOCases")
                        .HasForeignKey("CodeExerciseId");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Lesson", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.Module", null)
                        .WithMany("Lessons")
                        .HasForeignKey("ModuleId");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Module", b =>
                {
                    b.HasOne("EdPlatform.Data.Entities.Course", null)
                        .WithMany("Modules")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("EdPlatform.Data.Entities.Course", b =>
                {
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
