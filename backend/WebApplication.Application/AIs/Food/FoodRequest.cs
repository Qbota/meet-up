using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.AIs.Food
{
    public class FoodRequest
    {
        public List<string> Allergens { get; set; }
        public Dictionary<string, int> Cusines {get; set;}
        public int MealsAmount { get; set; }
    }
}
