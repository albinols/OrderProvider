﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Models
{
    public class OrderResponse
    {
        public string OrderId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal DeliveryCost { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; } = null!;
        public string MaskedCardNumber { get; set; } = null!;
        public string PaymentId { get; set; } = null!;
        public List<OrderItemResponse> OrderItems { get; set; } = new List<OrderItemResponse>();
    }
}
