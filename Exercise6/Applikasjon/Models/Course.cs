using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise6.API.Models
{
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
            string studentString = "";
            if(Students != null)
            {
                Students.ForEach(student =>
                {
                    studentString += (
                        student.FirstName + " " + student.LastName + " (ID: " + student.StudentId + "), "
                    );
                });
            }
            else
            {
                studentString = "No students.";
            }
            return CourseName + " (ID: " + CourseId + "), Students: " + studentString;
        }

    }
}
