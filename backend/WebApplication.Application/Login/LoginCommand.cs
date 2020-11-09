using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Authorization;

namespace WebApplication.Application
{
    public class LoginCommand : IRequest<JWTAuthResult>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
