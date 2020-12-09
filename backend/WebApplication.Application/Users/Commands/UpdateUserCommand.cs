using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<DateTime> AvailableDates { get; set; }
        public List<string> GroupIDs { get; set; }
        public List<string> MeetingIDs { get; set; }
        public MoviePreferenceDto MoviePreference { get; set; }
        public MealPreferenceDto MealPreference { get; set; }
    }
}
