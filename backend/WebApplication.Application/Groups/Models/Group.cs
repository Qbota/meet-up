using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Groups.Models
{
    public class Group
    {
        public string ID { get; private set; }
        public string Title { get; set; }
        public IEnumerable<string> MemberIDs { get; set; }
        public IEnumerable<Movie> MovieHistory { get; set; }
        public IEnumerable<Meal> MealsHistory { get; set; }


        public void SetID(string id)
        {
            this.ID = id;
        }
    }
}
