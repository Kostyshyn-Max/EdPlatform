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
        public DbSet<User> Users { get; set; }
        public DbSet<CourseUser> CourseUsers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<CodeExercise> CodeExercises { get; set; }
        public DbSet<ExerciseWithAnswer> ExercisesWithAnswer { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<IOCase> IOCases { get; set; }
        public DbSet<Atempt> Atempts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MSI-GF65-THIN;Initial Catalog=EdPlatform;Integrated Security=True");
        }
    }
}
