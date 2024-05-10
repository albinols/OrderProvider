using OrderProvider.Data.Entities;
using OrderProvider.Interfaces;
using OrderProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Factories
{
    public class OrderFactory : IOrderFactory
    {
        public OrderEntity CreateOrder(Guid orderId, CreateOrderRequest createOrderRequest, List<OrderItemEntity> orderItems)
        {
            decimal itemTotal = orderItems.Sum(item => item.UnitPrice * item.Quantity);
            decimal totalAmount = itemTotal + createOrderRequest.DeliveryCost;

            return new OrderEntity
            {
                OrderId = orderId,
                CustomerId = createOrderRequest.CustomerId,
                DeliveryAddress = createOrderRequest.DeliveryAddress,
                DeliveryCost = createOrderRequest.DeliveryCost,
                DeliveryDate = createOrderRequest.DeliveryDate,
                TotalAmount = totalAmount,
                OrderStatus = "Created",
                OrderDate = DateTime.UtcNow,
                Items = orderItems
            };
        }

        public List<OrderItemEntity> CreateOrderItems(Guid orderId, List<ProductRequest> productRequest, Dictionary<string, int> quantities)
        {
            return productRequest.Select(productRequest => new OrderItemEntity
            {
                OrderItemId = Guid.NewGuid(),
                OrderId = orderId,
                ProductId = productRequest.ProductId,
                ProductName = productRequest.ProductName,
                UnitPrice = productRequest.UnitPrice,
                Quantity = quantities[productRequest.ProductId]
            }).ToList();
        }
    }
}
