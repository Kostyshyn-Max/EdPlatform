using EdPlatform.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Data.EF
{
    public class ApplicationDbContext : DbContext
    {
#pragma warning disable CS8618
        public DbSet<User> Users { get; }
        public DbSet<CourseUser> CourseUsers { get; }
        public DbSet<Course> Courses { get; }
        public DbSet<Module> Modules { get; }
        public DbSet<Lesson> Lessons { get; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<CodeExercise> CodeExercises { get; }
        public DbSet<ExerciseWithAnswer> ExercisesWithAnswer { get; }
        public DbSet<Case> Cases { get; }
        public DbSet<IOCase> IOCases { get; }
        public DbSet<Atempt> Atempts { get; }
        public DbSet<Comment> Comments { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MSI-GF65-THIN;Initial Catalog=EdPlatform;Integrated Security=True");
        }
    }
}
