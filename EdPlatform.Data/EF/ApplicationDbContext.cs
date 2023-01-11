using EdPlatform.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

        public DbSet<Exercise> Exercise { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=MSI-GF65-THIN;Initial Catalog=EdPlatform;Integrated Security=True");
            optionsBuilder.UseNpgsql("Server = snuffleupagus.db.elephantsql.com; Database = kjzpbnca; Persist Security Info = True; User ID = kjzpbnca; Password = LM2BGDp42J9j8AuGBAZohi0b8vymw-so;");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.Login).IsUnique(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
