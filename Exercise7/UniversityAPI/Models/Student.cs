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
        public String FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public String LastName { get; set; }

        public List<Course> Courses { get; set; }

        public string GetString()
        {
            return "StudentId: " + StudentId + ", FirstName: " + FirstName + ", LastName: " + LastName;
        }
    }
}
