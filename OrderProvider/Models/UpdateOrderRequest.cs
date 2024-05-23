using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Models
{
    public class UpdateOrderRequest
    {
        public string OrderId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public List<OrderItemUpdateRequest> OrderItems { get; set; } = new List<OrderItemUpdateRequest>();
    }
}
