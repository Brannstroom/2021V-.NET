using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise6.API.Models
{
    public class Student
    {

        [Required(ErrorMessage = "StudentId is required")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(50, MinimumLength = 1)]
        public String FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        [StringLength(50, MinimumLength = 1)]
        public String LastName { get; set; }

        public List<Course> Courses { get; set; }

        public string GetString(bool includeCourses)
        {
            if (includeCourses && Courses != null)
            {
                if (Courses.Count > 0)
                {
                    return StudentId + " | " + FirstName + " " + LastName + " | " + Courses.Count + " courses";
                }
                else
                {
                    return StudentId + " | " + FirstName + " " + LastName + " | " + "0 courses";
                }
            }
            else
            {
                return StudentId + " | " + FirstName + " " + LastName;
            }
        }
    }
}
