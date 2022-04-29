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
        public String CourseName { get; set; }

        public List<Student> Students { get; set; }

        public string GetString()
        {
            return "CourseId: " + CourseId + ", CourseName: " + CourseName;
        }
    }
}
