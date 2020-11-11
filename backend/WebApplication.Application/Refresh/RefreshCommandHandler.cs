using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;

namespace WebApplication.Application.Refresh
{
    public class RefreshCommandHandler : IRequestHandler<RefreshCommand, JWTAuthResult>
    {
        private readonly IJWTService _jWTService;
        public RefreshCommandHandler(
            IJWTService jWTService)
        {
            _jWTService = jWTService;
        }
        public async Task<JWTAuthResult> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    throw new UnauthorizedAccessException();
                }
                var principal = _jWTService.GetPrincipalFromExpiredToken(request.AccessToken);
                var accountId = principal.Claims.First(x => x.Type == "id").Value;
                var jwtResult = _jWTService.Refresh(request.RefreshToken, request.AccessToken, DateTime.Now, accountId);
                return new JWTAuthResult
                {
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken
                };
            }
            catch (SecurityTokenException e)
            {
                throw new UnauthorizedAccessException(e.Message); 
            }
        }

    }
}
