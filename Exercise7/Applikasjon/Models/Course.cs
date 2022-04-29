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
        public int CourseId { get; set; }
        [Required(ErrorMessage = "CourseName is required")]
        [StringLength(50, MinimumLength = 1)]
        public String CourseName { get; set; }

        public List<Student> Students { get; set; }

        public string GetString(bool includeStudents)
        {
            if(includeStudents && Students != null)
            {
                if(Students.Count > 0)
                {
                    return CourseId + " | " + CourseName + " | " + Students.Count + " students";
                }
                else
                {
                    return CourseId + " | " + CourseName + " | "  + "0 students";
                }
            }
            else
            {
                return CourseId + " | " + CourseName;
            }
        }

    }
}
