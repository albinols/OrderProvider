using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using OrderProvider.Interfaces;
using OrderProvider.Models;

namespace OrderProvider.Functions
{
    public class UpdateOrder
    {
        private readonly ILogger<UpdateOrder> _logger;
        private readonly IOrderService _orderService;

        public UpdateOrder(ILogger<UpdateOrder> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Function("UpdateOrder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "update/{orderId}")] HttpRequest req, string orderId)
        {
            try
            {
                var updateOrderRequest = await _orderService.UnpackUpdateOrderRequest(req);

                if (updateOrderRequest == null)
                {
                    _logger.LogWarning("UpdateOrder.Run() :: Invalid request.");
                    return new BadRequestResult();
                }

                updateOrderRequest.OrderId = orderId;

                var result = await _orderService.UpdateOrder(updateOrderRequest);
                if (result)
                {
                    var updatedOrder = await _orderService.GetOrderByOrderId(updateOrderRequest.OrderId);
                    return new OkObjectResult(updatedOrder);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateOrder.Run() :: {ex.Message}");
            }

            return new BadRequestResult();
        }
    }
}
