using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Meals.Models;

namespace WebApplication.Application.Recomendations.Queries
{
    public class GetMealRecomendationQuery : IRequest<IEnumerable<MealDto>>
    {
        public string MeetingID;
    }
}
