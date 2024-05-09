using System;
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
        public Guid OrderId { get; set; }
        [Required]
        public string CustomerId { get; set; } = null!;
        [Required]
        public string DeliveryAddress { get; set; } = null!;
        public decimal DeliveryCost { get; set; } 
        public DateTime DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
        [Required]
        public string OrderStatus { get; set; } = null!;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItemEntity> Items { get; set; } = new List<OrderItemEntity>();
    }
}
