using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Meetings.Commands
{
    public class UpdateMeetingCommand : IRequest<string>
    {
        public string ID { get; private set; }
        public string Title { get; set; }
        public IEnumerable<DateTime> DatePropositions { get; set; }
        public IEnumerable<MovieDto> MoviePropositions { get; set; }
        public IEnumerable<MealDto> MealsPropositions { get; set; }
    }
}
