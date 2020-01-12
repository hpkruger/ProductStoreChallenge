using ProductStoreChallenge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreChallenge.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository OrderRepository { get; }
        private ICalculationService CalculationService { get; }

        public OrderService(IOrderRepository orderRepository, ICalculationService calculationService)
        {
            OrderRepository = orderRepository;
            CalculationService = calculationService;
        }

        public async Task<Order> PlaceOrderAsync(Order order)
        {
            // Hans: Calculate the total amount of the order and save it
            var calculationSummary = await CalculationService.CalculateAmountsAsync(order);

            order.TotalAmount = calculationSummary.TotalAmount;

            return await OrderRepository.CreateOrUpdateAsync(order);
        }
    }
}
