using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Helpers
{
    public static class TokenHelper
    {
        public static string? ExtractTokenFromHeader(HttpRequestData req, ILogger logger)
        {
            if (!req.Headers.TryGetValues("Authorization", out var authHeaderValues) || string.IsNullOrEmpty(authHeaderValues?.FirstOrDefault()))
            {
                logger.LogWarning("Authorization header is missing.");
                return null;
            }

            var token = authHeaderValues.First().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
            if (string.IsNullOrEmpty(token))
            {
                logger.LogWarning("Authorization header is present but does not contain a valid token.");
                return null;
            }

            return token;
        }

        public static string? ExtractUserIdFromToken(string token, ILogger logger)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                var jwtToken = handler.ReadJwtToken(token);

                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");

                return userIdClaim?.Value;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error decoding token: {ex.Message}");
                return null;
            }
        }
    }
}
