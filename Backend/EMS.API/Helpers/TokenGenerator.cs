using System.Security.Cryptography;

namespace EMS.API.Helpers
{
    public static class TokenGenerator
    {
        public static string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(randomBytes);
        }
    }
}