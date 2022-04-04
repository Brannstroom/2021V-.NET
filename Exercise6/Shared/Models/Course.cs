using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    class Course
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
}
