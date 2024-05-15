using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Models
{
    public class CreateOrderRequest
    {
        public string CustomerId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public decimal DeliveryCost { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<OrderItemRequest> OrderItemRequests { get; set; } = new List<OrderItemRequest>();
    }
}
