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
        public DbSet<FillExercise> FillExercises { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<IOCase> IOCases { get; set; }
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server = hattie.db.elephantsql.com; Database = nqhbqzai; Persist Security Info = True; User ID = nqhbqzai; Password = SA3VjkjKiCLxwMTpUoHBnrmc4AOKb9DJ");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
