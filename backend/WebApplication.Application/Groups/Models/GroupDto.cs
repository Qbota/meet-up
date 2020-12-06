using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Groups.Models
{
    public class GroupDto
    {
        public string ID { get; private set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public List<string> MemberIDs { get; set; }
        public List<string> MeetingIDs { get; set; }
        public List<MovieDto> MovieHistory { get; set; }
        public List<MealDto> MealsHistory { get; set; }

    }
}
