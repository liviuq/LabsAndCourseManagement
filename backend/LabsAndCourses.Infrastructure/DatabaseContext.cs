using Microsoft.EntityFrameworkCore;
using LabsAndCoursesManagement.Domain;
using System.Text.RegularExpressions;

namespace LabsAndCoursesManagement.Infrastructure
{
    public class DatabaseContext : DbContext
        //, IDatabaseContext
    {
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Didactic> Didactics => Set<Didactic>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource = LabsAndCoursesManagement.db");
        }

    }
}
