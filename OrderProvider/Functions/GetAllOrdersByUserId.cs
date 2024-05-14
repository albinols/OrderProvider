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

        //[Function("GetAllOrdersByUserId")]
        //public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/by-user")] HttpRequestData req)
        //{
        //    try
        //    {
        //        var token = TokenHelper.ExtractTokenFromHeader(req, _logger);

        //        if (!string.IsNullOrEmpty(token))
        //        {
        //            var userId = TokenHelper.ExtractUserIdFromToken(token, _logger);

        //            if (!string.IsNullOrEmpty(userId))
        //            {
        //                var orderResponses = await _orderService.GetAllOrdersByUserId(userId);

        //                if (orderResponses != null)
        //                {
        //                    var response = req.CreateResponse(HttpStatusCode.OK);
        //                    await response.WriteAsJsonAsync(orderResponses);
        //                    return response;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"GetAllOrdersByUserId.Run() :: {ex.Message}");
        //    }

        //    var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
        //    await badRequestResponse.WriteStringAsync("Missing or invalid user ID in the token.");
        //    return badRequestResponse;
            // Extract the JWT token using the helper method
            //var token = TokenHelper.ExtractTokenFromHeader(req, _logger);
            //if (string.IsNullOrEmpty(token))
            //{
            //    var unauthorizedResponse = req.CreateResponse(HttpStatusCode.Unauthorized);
            //    await unauthorizedResponse.WriteStringAsync("Missing or invalid Authorization header.");
            //    return unauthorizedResponse;
            //}

            //// Extract the user ID from the token using the helper method
            //var userId = TokenHelper.ExtractUserIdFromToken(token, _logger);
            //if (string.IsNullOrEmpty(userId))
            //{
            //    var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            //    await badRequestResponse.WriteStringAsync("Missing or invalid user ID in the token.");
            //    return badRequestResponse;
            //}

            //// Retrieve all orders for the user
            //var orders = await _orderService.GetAllOrdersByUserId(userId);

            //// If no orders are found, return a 404 response
            //if (orders == null || orders.Count == 0)
            //{
            //    var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
            //    await notFoundResponse.WriteStringAsync($"No orders found for user {userId}.");
            //    return notFoundResponse;
            //}

            //// Create a success response with the orders as JSON
            //var response = req.CreateResponse(HttpStatusCode.OK);
            //await response.WriteAsJsonAsync(orders);
            //return response;
        //}


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
