using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using OrderProvider.Interfaces;

namespace OrderProvider.Functions
{
    public class DeleteOrderById
    {
        private readonly ILogger<DeleteOrderById> _logger;
        private readonly IOrderService _orderService;

        public DeleteOrderById(ILogger<DeleteOrderById> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Function("DeleteOrderById")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "order/{orderId}")] HttpRequest req, string orderId)
        {
            try
            {
                var success = await _orderService.DeleteOrderById(orderId);
                if (success)
                {
                    return new OkResult();
                }
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteOrderById.Run() :: {ex.Message}");
            }
            return new BadRequestResult();
        }
    }
}
