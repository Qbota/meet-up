using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Meetings.Models
{
    public class MeetingDto
    {
        public string ID { get; private set; }
        public string Title { get; set; }
        public string GroupID { get; set; }
        public string OrganiserID { get; set; }
        public IEnumerable<DateTime> DatePropositions { get; set; }
        public IEnumerable<MovieDto> MoviePropositions { get; set; }
        public IEnumerable<MealDto> MealsPropositions { get; set; }

    }
}
