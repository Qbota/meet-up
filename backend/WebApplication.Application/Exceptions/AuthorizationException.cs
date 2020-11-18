using System;

namespace WebApplication.Application.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException()
        {
            
        }
        public AuthorizationException(string message)
         : base(message)
        {
        }

    }
}
