using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.AIs
{
    public class RecomendationService : IRecomendationService
    {
        private readonly IDatePickerService _datePickerService;
        private readonly IMovieRecomendationService _movieRecomendationService;
        private readonly IFoodRecomendationService _foodRecomendationService;
        private readonly IUserRepository _userRepository;

        public RecomendationService(
            IUserRepository userRepository,
            IDatePickerService datePickerService,
            IMovieRecomendationService movieRecomendationService,
            IFoodRecomendationService foodRecomendationService)
        {
            _userRepository = userRepository;
            _datePickerService = datePickerService;
            _foodRecomendationService = foodRecomendationService;
            _movieRecomendationService = movieRecomendationService;
        }
        public async Task<MeetingDO> GetRecomendations(MeetingDO meeting)
        {
            var users = await _userRepository.GetUsersByGroupIdAsync(meeting.GroupID);
            var mealPreferences = users.Select(x => x.MealPreference).ToList();
            var moviePreferences = users.Select(x => x.MoviePreference).ToList();
            var dates = GetDates(users);
            var mealTask = _foodRecomendationService.GetMealRecomendations(mealPreferences);
            var movieTask = _movieRecomendationService.GetMovieRecomendations(moviePreferences);
            await Task.WhenAll(mealTask, movieTask);
            meeting.MealsPropositions = mealTask.Result;
            meeting.MoviePropositions = movieTask.Result;
            meeting.DateProposition =  _datePickerService.PickDate(dates);
            return meeting;
        }
        private List<DateTime> GetDates(IEnumerable<UserDO> users)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (var user in users)
            {
                dates.AddRange(user.AvailableDates.Where(x => x > DateTime.Now));
            }
            return dates;
        }
    }
}
