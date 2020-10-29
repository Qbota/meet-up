using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Application.Meals.Models
{
    public class MealDto
    {
        public string ID { get; private set; }
        public string Name { get; set; }
        public  Cuisine Cuisine { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
    }
}
