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
        public OrderEntity CreateOrder(string orderId, int orderNumber, CreateOrderRequest createOrderRequest, List<OrderItemEntity> orderItems)
        {
            decimal itemTotal = orderItems.Sum(item => item.UnitPrice * item.Quantity);
            decimal totalAmount = itemTotal + createOrderRequest.DeliveryCost;

            return new OrderEntity
            {
                OrderId = orderId,
                UserId = createOrderRequest.UserId,
                Address = createOrderRequest.Address,
                DeliveryCost = createOrderRequest.DeliveryCost,
                DeliveryDate = createOrderRequest.DeliveryDate.Date,
                TotalAmount = totalAmount,
                OrderStatus = "Created",
                OrderDate = DateTime.UtcNow.Date,
                OrderNumber = orderNumber.ToString(),
                MaskedCardNumber = createOrderRequest.MaskedCardNumber,
                PaymentId = createOrderRequest.PaymentId,
                OrderItems = orderItems
            };
        }

        public List<OrderItemEntity> CreateOrderItems(string orderId, List<ProductRequest> productRequest, Dictionary<string, int> quantities)
        {
            return productRequest.Select(productRequest => new OrderItemEntity
            {
                OrderItemId = Guid.NewGuid().ToString(),
                OrderId = orderId,
                ProductId = productRequest.ProductId,
                ProductName = productRequest.ProductName,
                UnitPrice = productRequest.UnitPrice,
                Quantity = quantities[productRequest.ProductId]
            }).ToList();
        }

        public OrderResponse CreateOrderResponse(OrderEntity order)
        {
            return new OrderResponse
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                Address = order.Address,
                DeliveryCost = order.DeliveryCost,
                DeliveryDate = order.DeliveryDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                OrderDate = order.OrderDate,
                OrderNumber = order.OrderNumber,
                MaskedCardNumber = order.MaskedCardNumber,
                PaymentId = order.PaymentId,
                OrderItems = order.OrderItems.Select(i => new OrderItemResponse
                {
                    OrderItemId = i.OrderItemId,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };
        }

        public IEnumerable<OrderResponse> CreateOrderResponses(IEnumerable<OrderEntity> orders)
        {
            return orders.Select(CreateOrderResponse).ToList();
        }
    }
}
