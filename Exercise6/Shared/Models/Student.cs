using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    class Student
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
