using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Groups.Commands
{
    public class CreateGroupCommand : IRequest<string>
    {
        public string Name { get; set; }
        public IEnumerable<string> MemberIDs { get; set; }
        public IEnumerable<MovieDto> MovieHistory { get; set; }
        public IEnumerable<MealDto> MealsHistory { get; set; }
    }
}
