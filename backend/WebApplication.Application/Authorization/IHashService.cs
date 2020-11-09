using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Authorization
{
    public interface IHashService 
    {
        string GetPasswordHash(string password, out byte[] salt);
        bool ComparePasswords(string inputPassword, string sourceHashedPassword, byte[] salt);
    }
}
