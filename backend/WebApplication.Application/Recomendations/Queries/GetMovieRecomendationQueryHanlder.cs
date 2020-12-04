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
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Recomendations.Queries
{
    public class GetMovieRecomendationQueryHandler : IRequestHandler<GetMovieRecomendationQuery, IEnumerable<MovieDto>>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMovieRepository _movieRepository;
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
            IMovieRepository movieRepository,
            IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _userRepository = userRepository;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _movieRecomendationService = movieRecomendationService;
            _mapper = mapper;
            _movieRepository = movieRepository;
        }
        public async Task<IEnumerable<MovieDto>> Handle(GetMovieRecomendationQuery request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeMeetingAccessOrThrow(_httpContextAccessor.HttpContext, request.MeetingID);
            var meeting = await _meetingRepository.GetMeetingByIdAsync(request.MeetingID);
            if (meeting is null)
                throw new NotFoundException();
            var movies = await _movieRecomendationService.GetMovieRecomendations(await GetPreferencesAsync(meeting.GroupID));
            await AddMoviesToRepositoryAsync(movies);
            await UpdateMeetingAsync(meeting,movies);
            return movies.Select(x => _mapper.Map<MovieDto>(x)).ToList();
        }

        private async Task<List<MoviePreferenceDO>> GetPreferencesAsync(string groupID)
        {
            var users = await _userRepository.GetUsersByGroupIdAsync(groupID);
            return  users.Select(x => x.MoviePreference).ToList();
        }

        private async Task UpdateMeetingAsync(MeetingDO meeting, List<MovieDO> movies)
        {
            meeting.MoviePropositions = movies;
            await _meetingRepository.UpdateMeetingAsync(meeting);
        }

        private async Task AddMoviesToRepositoryAsync(List<MovieDO> movies)
        {
            foreach (var movie in movies)
            {
                await _movieRepository.AddMovieAsync(movie);
            }
        }
    }
}
