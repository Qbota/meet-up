using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJWTService _jWTService;

        public JWTMiddleware(RequestDelegate next, IJWTService jWTService)
        {
            _next = next;
            _jWTService = jWTService;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await AttachAccountToContext(context, token, userRepository);
            }
            await _next(context);
        }

        private async Task AttachAccountToContext(HttpContext context, string token, IUserRepository userRepository)
        {
            try
            {
                var (principal, jwtToken) = _jWTService.DecodeJwtToken(token);
                var accountId = jwtToken.Claims.First(x => x.Type == "id").Value;
                context.Items["Account"] = await userRepository.GetUserByIdAsync(accountId);
            }
            catch
            {
                //
            }
        }
    }
}
