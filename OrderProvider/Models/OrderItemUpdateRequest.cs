using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Models
{
    public class OrderItemUpdateRequest
    {
        public string ProductId { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
