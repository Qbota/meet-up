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
        public string Description { get; set; }
        public List<DateTime> DatePropositions { get; set; }
        public List<MovieDto> MoviePropositions { get; set; }
        public List<MealDto> MealsPropositions { get; set; }

    }
}
