using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.AIs;
using WebApplication.Application.Authorization;
using WebApplication.Application.Exceptions;
using WebApplication.Application.Movies.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Recomendations.Queries
{
    public class GetMovieRecomendationQueryHandler : IRequestHandler<GetMovieRecomendationQuery, IEnumerable<MovieDto>>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMovieRecomendationService _movieRecomendationService;
        private readonly IMapper _mapper;
        public GetMovieRecomendationQueryHandler(
            IMovieRecomendationService movieRecomendationService,
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
            _movieRecomendationService = movieRecomendationService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MovieDto>> Handle(GetMovieRecomendationQuery request, CancellationToken cancellationToken)
        {
            /*_authorizationService.AuthorizeMeetingAccessOrThrow(_httpContextAccessor.HttpContext, request.MeetingID);
            var meeting = await _meetingRepository.GetMeetingByIdAsync(request.MeetingID);
            if (meeting is null)
                throw new NotFoundException();
            var users = await _userRepository.GetUsersByGroupIdAsync(meeting.GroupID);
            var preferences = users.Select(x => x.MoviePreference).ToList();*/
            var meals = await _movieRecomendationService.GetMovieRecomendations(null);//preferences);
            return meals.Select(x => _mapper.Map<MovieDto>(x)).ToList();
        }
    }
}
