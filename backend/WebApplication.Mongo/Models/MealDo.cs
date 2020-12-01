using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class MealDO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
    }
}
