using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Meals.Models;

namespace WebApplication.Application.Recomendations.Queries
{
    public class GetMealRecomendationQueryHandler : IRequestHandler<GetMealRecomendationQuery, IEnumerable<MealDto>>
    {
        public GetMealRecomendationQueryHandler()
        { 
        }
        public Task<IEnumerable<MealDto>> Handle(GetMealRecomendationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
