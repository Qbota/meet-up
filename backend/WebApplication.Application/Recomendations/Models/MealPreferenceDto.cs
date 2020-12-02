using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Meals.Models;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.Meals.Models
{
    public class MealPreferenceDto
    {
        public IEnumerable<string> Cousines { get; set; }
        public IEnumerable<string> Allergens { get; set; }
    }
}
