using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Models
{
    public class CreateOrderRequest
    {
        public string CustomerId { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public decimal DeliveryCost { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();
    }
}
