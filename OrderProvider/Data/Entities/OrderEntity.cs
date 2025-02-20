﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Data.Entities
{
    public class OrderEntity
    {
        [Key]
        public string OrderId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        public decimal DeliveryCost { get; set; } 
        public DateTime DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
        [Required]
        public string OrderStatus { get; set; } = null!;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow.Date;
        [Required]
        public string OrderNumber { get; set; } = null!;
        public string MaskedCardNumber { get; set; } = null!;
        public string PaymentId { get; set; } = null!;

        public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
    }
}
