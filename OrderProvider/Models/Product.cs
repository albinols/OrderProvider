using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public string ProductId { get; set; } = string.Empty;
        [JsonProperty("name")]
        public string ProductName { get; set; } = string.Empty;
        [JsonProperty("price")]
        public decimal UnitPrice { get; set; }
    }
}
