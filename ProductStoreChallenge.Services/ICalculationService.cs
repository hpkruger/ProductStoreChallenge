using ProductStoreChallenge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreChallenge.Services
{
    public interface ICalculationService
    {
        Task<(decimal SubTotalAmount, decimal ShippingAmount, decimal TotalAmount)> CalculateAmountsAsync(Basket basket);
        Task<(decimal SubTotalAmount, decimal ShippingAmount, decimal TotalAmount)> CalculateAmountsAsync(Order order);
    }
}
