﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Models
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal DeliveryCost { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string MaskedCardNumber { get; set; } = null!;
        public string PaymentId { get; set; } = null!;
        public List<OrderItemRequest> OrderItem { get; set; } = new List<OrderItemRequest>();
    }
}
