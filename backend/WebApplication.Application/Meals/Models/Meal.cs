using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Application.Meals.Models
{
    public class Meal
    {
        public string ID { get; private set; }
        public string Name { get; set; }
        public  Cuisine Cuisine { get; set; }

        public IEnumerable<string> Ingredients { get; set; }

        public void SetID(string id)
        {
            this.ID = id;
        }
    }
    public enum Cuisine 
    {
        Polish,
        American,
        Italian,
        Mexican,
        Asian
    }
}
