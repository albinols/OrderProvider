using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using OrderProvider.Interfaces;

namespace OrderProvider.Functions
{
    public class GetOrderByOrderId
    {
        private readonly ILogger<GetOrderByOrderId> _logger;
        private readonly IOrderService _orderService;

        public GetOrderByOrderId(ILogger<GetOrderByOrderId> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Function("GetOrderByOrderId")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "order/{orderId}")] HttpRequest req, string orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByOrderId(orderId);
                if (order != null)
                {
                    return new OkObjectResult(order);
                }
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetOrderByOrderId.Run() :: {ex.Message}");
            }
            return new BadRequestResult();
        }
    }
}
