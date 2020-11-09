using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Meals.Models;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.Meals.Models
{
    public class MealPreferenceDto
    {
        public IEnumerable<MealDto> Meals { get; set; }
        public IEnumerable<Cuisine> Cuisines { get; set; }
        public IEnumerable<Allergens> Allergens { get; set; }
    }
}
