using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise4
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
            }) ;


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

    public class Course
    {
        [Required(ErrorMessage = "CourseId is required")]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "CourseName is required")]
        [StringLength(50, MinimumLength = 1)]
        public String CourseName { get; set; }

        public List<Student> Students { get; set; }

        public string GetString()
        {
            return "CourseId: " + CourseId + ", CourseName: " + CourseName;
        }
    }

    public class Student
    {

        [Required(ErrorMessage = "StudentId is required")]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(50, MinimumLength = 1)]
        public String FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        [StringLength(50, MinimumLength = 1)]
        public String LastName { get; set; }

        public List<Course> Courses { get; set; }

        public string GetString()
        {
            return "StudentId: " + StudentId + ", FirstName: " + FirstName + ", LastName: " + LastName;
        }
    }
}
