using ProductStoreChallenge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreChallenge.Services
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(Order order);
    }
}
