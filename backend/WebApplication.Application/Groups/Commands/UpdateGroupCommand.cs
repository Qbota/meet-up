using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Groups.Commands
{
    public class UpdateGroupCommand : IRequest<string>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<string> MemberIDs { get; set; }
        public List<MovieDto> MovieHistory { get; set; }
        public List<MealDto> MealsHistory { get; set; }
    }
}
