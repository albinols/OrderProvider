using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderProvider.Data.Contexts;
using OrderProvider.Data.Entities;
using OrderProvider.Factories;
using OrderProvider.Helpers;
using OrderProvider.Interfaces;
using OrderProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderFactory _orderFactory;
        private readonly IProductClient _productClient;

        public OrderService(DataContext context, ILogger<OrderService> logger, IOrderFactory orderFactory, IProductClient productClient)
        {
            _context = context;
            _logger = logger;
            _orderFactory = orderFactory;
            _productClient = productClient;
        }
        public async Task<CreateOrderRequest> UnpackHttpRequest(HttpRequest req)
        {
            var createOrderRequest = await HttpRequestHelper.UnpackHttpRequest<CreateOrderRequest>(req, _logger);

            if (createOrderRequest == null)
            {
                _logger.LogWarning("CreateOrderService.UnpackCreateOrderRequest() :: Failed to unpack the request.");
            }

            return createOrderRequest; 
        }

        public async Task<bool> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            try
            {
                var orderId = Guid.NewGuid().ToString();

                var (products, quantities, success) = await FetchProductsAndQuantities(createOrderRequest);

                if (!success)
                {
                    return false;
                }

                var order = CreateOrderWithFactory(orderId, createOrderRequest, products, quantities);

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateOrderService.CreateOrder() :: {ex.Message}");
            }

            return false;
        }

        private async Task<(List<ProductRequest> Products, Dictionary<string, int> Quantities, bool Success)> FetchProductsAndQuantities(CreateOrderRequest createOrderRequest)
        {
            var products = new List<ProductRequest>();
            var quantities = new Dictionary<string, int>();

            foreach (var productOrder in createOrderRequest.OrderItemRequests)
            {
                var product = await _productClient.GetProductById(productOrder.ProductId);
                if (product != null)
                {
                    products.Add(product);
                    quantities[product.ProductId] = productOrder.Quantity;
                }
                else
                {
                    _logger.LogWarning($"CreateOrderService.FetchProductsAndQuantities() Product not found: {productOrder.ProductId}");
                    return (null, null, false);
                }
            }

            return (products, quantities, true);
        }

        private OrderEntity CreateOrderWithFactory(string orderId, CreateOrderRequest createOrderRequest, List<ProductRequest> products, Dictionary<string, int> quantities)
        {
            var orderItems = _orderFactory.CreateOrderItems(orderId, products, quantities);
            var order = _orderFactory.CreateOrder(orderId, createOrderRequest, orderItems);
            return order;
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrdersByUserId(string userId)
        {
            var orders = await _context.Orders
                .Where(o => o.CustomerId == userId)
                .Include(o => o.OrderItems)
                .ToListAsync();

            return _orderFactory.CreateOrderResponses(orders);
        }
    }
}
