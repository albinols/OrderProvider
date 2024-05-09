using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderProvider.Functions;
using OrderProvider.Interfaces;
using OrderProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Services
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductClient> _logger;
        private const string ProductBaseUrl = "https://maneroproductprovider.azurewebsites.net/api/products/";
        private const string AccessCode = "fe9JA5ZB5N8Fxfys_qhOezJQAh4Qrr5FBEziMgpcO7GQAzFu3kV3YQ==";

        public ProductClient(HttpClient httpClient, ILogger<ProductClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }


        public async Task<Product?> GetProductById(string productId)
        {
            string url = $"{ProductBaseUrl}{productId}?code={AccessCode}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        var product = JsonConvert.DeserializeObject<Product>(result);
                        if (product != null)
                        {
                            return product;
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"ProductClient.GetProductById() :: {ex.Message}");
            }
            return null;
        }
    }
}
