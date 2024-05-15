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
        public string OrderId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string CustomerId { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public decimal DeliveryCost { get; set; } 
        public DateTime DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
        [Required]
        public string OrderStatus { get; set; } = null!;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow.Date;

        public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
    }
}
