using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using OrderProvider.Factories;
using OrderProvider.Interfaces;
using OrderProvider.Models;
using System.Net;

namespace OrderProvider.Functions
{
    public class CreateOrder
    {
        private readonly ILogger<CreateOrder> _logger;
        private readonly IOrderService _orderService;

        public CreateOrder(ILogger<CreateOrder> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Function("CreateOrder")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "create")] HttpRequest req)
        {
            try
            {
                var cpr = await _orderService.UnpackCreateOrderRequest(req);

                if (cpr != null || !string.IsNullOrEmpty(cpr.UserId) || cpr.OrderItem.Count != 0)
                {
                    var result = await _orderService.CreateOrder(cpr);
                    if (result != null)
                    {
                        return new CreatedResult("Order created", result);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateOrder.Run() :: {ex.Message}");
            }

            return new BadRequestResult();
        } 
    }
}
