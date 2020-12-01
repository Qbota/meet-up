using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class MealPreferenceDO
    {
        public IEnumerable<Cuisine> Cuisines { get; set; }
        public IEnumerable<String> Allergens { get; set; }
    }
}
