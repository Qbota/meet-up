using System;
using System.Collections.Generic;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Users.Models
{
    public class User
    {
        public string ID { get; private set; }
        public string Name { get; set; }
        public string Login { get; private set; }
        public IEnumerable<DateTime> AvailableDates { get; set; }
        public IEnumerable<string> GroupIDs { get; set; }
        public IEnumerable<string> MeetingIDs { get; set; }
        public IEnumerable<Movie> MoviePreferences { get; set; }
        public IEnumerable<Meal> MealPreferences { get; set; }
        public IEnumerable<string> Allergens { get; set; }
        public void SetID(string id)
        {
            this.ID = id;
        }
        public void SetLogin(string login)
        {
            this.Login = login;
        }
    }
}
