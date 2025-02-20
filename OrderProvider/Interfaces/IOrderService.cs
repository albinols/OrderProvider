﻿using Microsoft.AspNetCore.Http;
using OrderProvider.Data.Entities;
using OrderProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Interfaces
{
    public interface IOrderService
    {
        Task<CreateOrderRequest> UnpackCreateOrderRequest(HttpRequest req);
        Task<UpdateOrderRequest> UnpackUpdateOrderRequest(HttpRequest req);
        Task<OrderResponse> CreateOrder(CreateOrderRequest createOrderRequest);
        Task<IEnumerable<OrderResponse>> GetAllOrdersByUserId(string userId);
        Task<OrderResponse> GetOrderByOrderId(string orderId);
        Task<bool> DeleteOrderById(string orderId);
        Task<bool> UpdateOrder(UpdateOrderRequest updateOrderRequest);
    }
}
