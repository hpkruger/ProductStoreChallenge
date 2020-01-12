using ProductStoreChallenge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreChallenge.Services
{
    public class CalculationService : ICalculationService
    {
        public CalculationService(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }
        private IProductRepository ProductRepository { get; }

        // Hans: Using named tuples here. Alternatively introduce some sort of BasketCalculationSummary class
        public async Task<(decimal SubTotalAmount, decimal ShippingAmount, decimal TotalAmount)> CalculateAmountsAsync(Basket basket)
        {
            var products = await ProductRepository.GetByIdsAsync(basket.Items.Select(item => item.ProductId));

            var subTotalAmount = basket.Items.Join(products, basketItem => basketItem.ProductId, product => product.Id, (basketItem, product) => new
            {
                basketItem.Qty,
                product.Price
            }).Sum(item => item.Qty * item.Price);

            var shippingAmount = subTotalAmount <= 50 ? 10 : 20;
            var totalAmount = subTotalAmount + shippingAmount;

            return (subTotalAmount, shippingAmount, totalAmount);
        }

        public Task<(decimal SubTotalAmount, decimal ShippingAmount, decimal TotalAmount)> CalculateAmountsAsync(Order order)
        {
            return CalculateAmountsAsync(new Basket { Items = order.Items.Select(orderItem => new BasketItem { ProductId = orderItem.ProductId, Qty = orderItem.Qty }) });
        }
    }
}
