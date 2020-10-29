using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class MealDo
    {
        public string ID { get; private set; }
        public string Name { get; set; }
        public Cuisine Cuisine { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
    }
}
