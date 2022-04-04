using Exercise5_UML.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise5_UML
{
    class MealContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }

        public String DbPath { get; }
    }
}
