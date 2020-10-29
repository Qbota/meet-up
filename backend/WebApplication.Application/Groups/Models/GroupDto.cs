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
        public IEnumerable<string> MemberIDs { get; set; }
        public IEnumerable<MovieDto> MovieHistory { get; set; }
        public IEnumerable<MealDto> MealsHistory { get; set; }

    }
}
