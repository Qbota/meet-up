using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class MealPreferenceDO
    {
        public IEnumerable<MealDo> Meals { get; set; }
        public IEnumerable<Cuisine> Cuisines { get; set; }
        public IEnumerable<Allergens> Allergens { get; set; }
    }
}
