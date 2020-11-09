using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Authorization;

namespace WebApplication.Application.Refresh
{
    public class RefreshCommand : IRequest<JWTAuthResult>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
