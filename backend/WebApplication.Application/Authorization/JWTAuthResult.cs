using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Users.Models;

namespace WebApplication.Application.Authorization
{
    public class JWTAuthResult
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public UserDto User { get; set; }
    }
}
