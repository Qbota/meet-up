using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class MealPreferenceDO
    {
        public List<string> Cousines { get; set; }
        public List<string> Allergens { get; set; }
    }
}
