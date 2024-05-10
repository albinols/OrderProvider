using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Models
{
    public class ProductRequest
    {
        [JsonProperty("id")]
        public string ProductId { get; set; } = string.Empty;
        [JsonProperty("name")]
        public string ProductName { get; set; } = string.Empty;
        [JsonProperty("price")]
        public decimal UnitPrice { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(ProductId) && !string.IsNullOrEmpty(ProductName) && UnitPrice > 0;
        }
    }
}
