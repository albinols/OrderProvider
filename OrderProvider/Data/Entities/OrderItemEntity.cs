﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Data.Entities
{
    public class OrderItemEntity
    {
        [Key]
        public string OrderItemId { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("OrderEntity")]
        public string OrderId { get; set; } = null!;
        [Required]
        public string ProductId { get; set; } = null!;
        [Required]
        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public OrderEntity Order { get; set; } = null!;
    }
}
