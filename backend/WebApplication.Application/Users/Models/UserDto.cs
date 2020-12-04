using System;
using System.Collections.Generic;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Users.Models
{
    public class UserDto
    {
        public string ID { get; private set; }
        public string Name { get; set; }
        public string Login { get; private set; }
        public List<DateTime> AvailableDates { get; set; }
        public List<string> GroupIDs { get; set; }
        public List<string> MeetingIDs { get; set; }
        public MoviePreferenceDto MoviePreference { get; set; }
        public MealPreferenceDto MealPreference { get; set; }

        public void SetLogin(string login)
        {
            this.Login = login;
        }
    }
}
