using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise5_UML.Models
{
    enum MealType { Frokost, Lunsj, Middag, Kveldsmat, Brunsj, Snacks };

    class Meal
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int Calories { get; set; }
        public MealType MealType { get; set; }
        public int Rating { get; set; }
        public bool SharedMeal { get; set; }
        public string Image { get; set; }

    }
}
