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

            // Remove the "Bearer " prefix if present
            var token = authHeaderValues.First().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
            if (string.IsNullOrEmpty(token))
            {
                logger.LogWarning("Authorization header is present but does not contain a valid token.");
                return null;
            }

            return token;
        }

        // Decode the token and extract the user ID from the `sub` claim
        public static string? ExtractUserIdFromToken(string token, ILogger logger)
        {
            try
            {
                // Initialize a JWT handler to read and decode the token
                var handler = new JwtSecurityTokenHandler();

                // Read and decode the JWT
                var jwtToken = handler.ReadJwtToken(token);

                // Extract the `sub` (subject) claim, which usually represents the user ID
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");

                // Return the user ID if found, otherwise return null
                return userIdClaim?.Value;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during token decoding
                logger.LogError($"Error decoding token: {ex.Message}");
                return null;
            }
        }
    }
}
