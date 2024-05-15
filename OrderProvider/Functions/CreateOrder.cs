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
        private readonly IOrderService _createOrderService;

        public CreateOrder(ILogger<CreateOrder> logger, IOrderService createOrderService)
        {
            _logger = logger;
            _createOrderService = createOrderService;
        }

        [Function("CreateOrder")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "create")] HttpRequest req)
        {
            try
            {
                var cpr = await _createOrderService.UnpackHttpRequest(req);

                if (cpr != null || !string.IsNullOrEmpty(cpr.UserId) || cpr.OrderItem.Count != 0)
                {
                    var result = await _createOrderService.CreateOrder(cpr);
                    if (result)
                    {
                        return new CreatedResult();
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
