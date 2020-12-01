using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver.Core.Clusters.ServerSelectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.AIs;
using WebApplication.Application.Authorization;
using WebApplication.Application.Exceptions;
using WebApplication.Application.Meals.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Recomendations.Queries
{
    public class GetMealRecomendationQueryHandler : IRequestHandler<GetMealRecomendationQuery, IEnumerable<MealDto>>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFoodRecomendationService _foodRecomendationService;
        private readonly IMapper _mapper;
        public GetMealRecomendationQueryHandler(
            IFoodRecomendationService foodRecomendationService,
            IMeetingRepository meetingRepository,
            IUserRepository userRepository,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _userRepository = userRepository;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _foodRecomendationService = foodRecomendationService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MealDto>> Handle(GetMealRecomendationQuery request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeMeetingAccessOrThrow(_httpContextAccessor.HttpContext, request.MeetingID);
            var meeting = await _meetingRepository.GetMeetingByIdAsync(request.MeetingID);
            if (meeting is null)
              throw new NotFoundException();
            var users = await _userRepository.GetUsersByGroupIdAsync(meeting.GroupID);
            var preferences = users.Select(x => x.MealPreference).ToList();
            var meals = await _foodRecomendationService.GetMealRecomendations(preferences);
            return meals.Select(x => _mapper.Map<MealDto>(x)).ToList();
        }
    }
}
