using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderProvider.Data.Contexts;
using OrderProvider.Data.Entities;
using OrderProvider.Factories;
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
    public class CreateOrderService : ICreateOrderService
    {
        private readonly DataContext _context;
        private readonly ILogger<CreateOrderService> _logger;
        private readonly IOrderFactory _orderFactory;
        private readonly IProductClient _productClient;

        public CreateOrderService(DataContext context, ILogger<CreateOrderService> logger, IOrderFactory orderFactory, IProductClient productClient)
        {
            _context = context;
            _logger = logger;
            _orderFactory = orderFactory;
            _productClient = productClient;
        }

        public async Task<CreateOrderRequest> UnpackCreateOrderRequest(HttpRequest req)
        {
            try
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                if(!string.IsNullOrEmpty(body))
                {
                    var cpr = JsonConvert.DeserializeObject<CreateOrderRequest>(body);
                    if(cpr != null)
                    {
                        return cpr;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateOrder.UnpackCreateOrderRequest() :: {ex.Message}");
            }

            return null;    
        }

        public async Task<bool> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            try
            {
                var orderId = Guid.NewGuid();

                var products = new List<Product>();

                var quantities = new Dictionary<string, int>();

                foreach (var productOrder in createOrderRequest.Items)
                {
                    var product = await _productClient.GetProductById(productOrder.ProductId);
                    if(product != null)
                    {
                        products.Add(product);
                        quantities[product.ProductId] = productOrder.Quantity;
                    }
                }

                var orderItems = _orderFactory.CreateOrderItems(orderId, products, quantities);

                var order = _orderFactory.CreateOrder(orderId, createOrderRequest, orderItems); 

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
    }
}
