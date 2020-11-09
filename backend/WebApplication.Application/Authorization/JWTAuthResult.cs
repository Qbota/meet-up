using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Authorization
{
    public class JWTAuthResult
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
