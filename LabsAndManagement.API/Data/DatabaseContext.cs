using LabsAndManagement.Business;
using Microsoft.EntityFrameworkCore;

namespace LabsAndManagement.API.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lab> Labs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = LabsDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var course1 = new Course
            {
                Id = "1",
                Name = ".net",
                NoCredits = 4,
            };

            var lab1 = new Lab
            {
                Id = "1",
                CourseId = course1.Id,
            };

            var student1 = new Student
            {
                NumarMatricol = "34324RSE123123",
                Nume = "Pavel",
                Prenume = "Petronel",
                InitialaTata = 'C',
                CourseId = course1.Id,
                LabId = lab1.Id
            };
            modelBuilder
                .Entity<Course>()
                .HasData(course1);
            modelBuilder
                .Entity<Student>()
                .HasData(student1);
            modelBuilder
                .Entity<Lab>()
                .HasData(lab1);
        }
    }
}

