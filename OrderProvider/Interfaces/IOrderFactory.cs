﻿using OrderProvider.Data.Entities;
using OrderProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Interfaces
{
    public interface IOrderFactory
    {
        OrderEntity CreateOrder(string orderId, int orderNumber, CreateOrderRequest createOrderRequest, List<OrderItemEntity> orderItems);
        List<OrderItemEntity> CreateOrderItems(string orderId, List<ProductRequest> productRequest, Dictionary<string, int> quantities);
        OrderResponse CreateOrderResponse(OrderEntity order);
        IEnumerable<OrderResponse> CreateOrderResponses(IEnumerable<OrderEntity> orders);
    }
}
