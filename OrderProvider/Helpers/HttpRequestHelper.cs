using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderProvider.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Helpers
{
    public static class HttpRequestHelper
    {
        public static async Task<T?> UnpackHttpRequest<T>(HttpRequest req, ILogger logger)
        {
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                if (string.IsNullOrEmpty(requestBody))
                {
                    logger.LogWarning("HttpRequestHelper.UnpackHttpRequest: Received an empty request body.");
                    return default;
                }

                return JsonConvert.DeserializeObject<T>(requestBody);
            }
            catch (Exception ex)
            {
                logger.LogError($"HttpRequestHelper.UnpackHttpRequest: Error while deserializing the request. Exception: {ex.Message}");
                return default;
            }
        }
    }
}
