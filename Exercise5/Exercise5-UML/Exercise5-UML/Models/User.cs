using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise5_UML.Models
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string ProfilePicture { get; set; }
        public bool PublicProfile { get; set; }
        public Meal FavoriteMeal { get; set; }
        public List<Meal> Meals { get; set; }

    }
}
