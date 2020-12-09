using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.Meals.Models
{
    public class MealDto
    {
        public string ID { get; private set; }
        public string Name { get; set; }
        public  string Cuisine { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
