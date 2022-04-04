using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class UniversityContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        public string DbPath { get; }

        public UniversityContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "university.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            SqlConnectionStringBuilder sqlConnectionString = new()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "university.db",
            };

            options.UseSqlServer(sqlConnectionString.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Course>().HasData(new Course
            {
                CourseId = 1,
                CourseName = ".NET",
                Students = null,
            });

            modelBuilder.Entity<Student>().HasData(new Student
            {
                StudentId = 1,
                FirstName = "Peter",
                LastName = "Brannstrom",
                Courses = null,
            });

            modelBuilder.Entity<Student>().HasData(new Student
            {
                StudentId = 2,
                FirstName = "Ola",
                LastName = "Normann",
                Courses = null,
            });


            modelBuilder.Entity<Course>()
                .Property(c => c.CourseId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Student>()
                .HasMany(a => a.Courses)
                .WithMany(b => b.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseStudent",
                        c => c.HasOne<Course>().WithMany().HasForeignKey("CoursesCourseId"),
                        d => d.HasOne<Student>().WithMany().HasForeignKey("StudentsStudentId"),
                        e =>
                        {
                            e.HasKey("CoursesCourseId", "StudentsStudentId");
                            e.HasData(
                                new { CoursesCourseId = 1, StudentsStudentId = 1 },
                                new { CoursesCourseId = 1, StudentsStudentId = 2 }
                                );
                        })
                .Property(s => s.StudentId)
                .ValueGeneratedOnAdd();
        }
    }
}
