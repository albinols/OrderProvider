using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using OrderProvider.Helpers;
using OrderProvider.Interfaces;
using System.Net;

namespace OrderProvider.Functions
{
    public class GetAllOrdersByUserId
    {
        private readonly ILogger<GetAllOrdersByUserId> _logger;
        private readonly IOrderService _orderService;

        public GetAllOrdersByUserId(ILogger<GetAllOrdersByUserId> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Function("GetAllOrdersByUserId")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/{userId}")] HttpRequestData req, string userId)
        {
            try
            {
                var orders = await _orderService.GetAllOrdersByUserId(userId);
                if(orders != null)
                {
                    return new OkObjectResult(orders);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllOrdersByUserId.Run() :: {ex.Message}");
            }
            return new BadRequestResult();
        }
    }
}
