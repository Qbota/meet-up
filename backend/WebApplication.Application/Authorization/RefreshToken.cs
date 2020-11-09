using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Authorization
{
    public class RefreshToken
    {
        public string UserId { get; set; } 
        public string TokenString { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
