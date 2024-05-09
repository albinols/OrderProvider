using Microsoft.AspNetCore.Http;
using OrderProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Interfaces
{
    public interface ICreateOrderService
    {
        Task<CreateOrderRequest> UnpackCreateOrderRequest(HttpRequest req);
        Task<bool> CreateOrder(CreateOrderRequest createOrderRequest);
    }
}
