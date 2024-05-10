using OrderProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Interfaces
{
    public interface IProductClient
    {
        Task<ProductRequest?> GetProductById(string productId);
    }
}
