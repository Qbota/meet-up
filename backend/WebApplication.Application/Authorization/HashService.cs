using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication.Application.Authorization
{
    public class HashService : IHashService
    {
        public bool ComparePasswords(string inputPassword, string sourceHashedPassword, byte[] salt)
        {
            var hashedInputPassword = GetPasswordHash(inputPassword, salt);
            return String.Equals(sourceHashedPassword, hashedInputPassword);
        }

        public string GetPasswordHash(string password, out byte[] salt)
        {
            salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
        private string GetPasswordHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
