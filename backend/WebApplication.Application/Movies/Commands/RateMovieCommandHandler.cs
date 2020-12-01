using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Movies.Commands
{
    public class RateMovieCommandHandler : IRequestHandler<RateMovieCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RateMovieCommandHandler(
            IUserRepository userRepository,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> Handle(RateMovieCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeAccessOrThrow(_httpContextAccessor.HttpContext, request.UserID);
            var user = await _userRepository.GetUserByIdAsync(request.UserID);
            if (user.MoviePreference.Ratings.ContainsKey(request.MovieID))
            {
                user.MoviePreference.Ratings[request.MovieID] = request.Rating;
            }
            else user.MoviePreference.Ratings.Add(request.MovieID, request.Rating);
            await _userRepository.UpdateUserAsync(user);
            return request.MovieID;
        }
    }
}
