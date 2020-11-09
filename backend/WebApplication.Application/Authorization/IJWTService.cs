using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.Authorization
{
    public interface IJWTService
    {
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
        JWTAuthResult GenerateTokens(UserDO user);
        JWTAuthResult Refresh(string refreshToken, string accessToken, DateTime now, string userName);
        void RemoveExpiredRefreshTokens(DateTime now);
        void RemoveRefreshTokenByUserId(string userName);
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
