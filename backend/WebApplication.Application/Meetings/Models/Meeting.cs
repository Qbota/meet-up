using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Meetings.Models
{
    public class Meeting
    {
        public string ID { get; private set; }
        public string Title { get; set; }
        public string GroupID { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Movie> MoviePropositions { get; set; }
        public IEnumerable<Meal> MealsPropositions { get; set; }


        public void SetID(string id)
        {
            this.ID = id;
        }
    }
}
