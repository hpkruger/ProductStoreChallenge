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

        public async Task<double> CalculateShippingCostsAsync(Basket basket)
        {
            var products = await ProductRepository.GetByIdsAsync(basket.Items.Select(item => item.ProductId));

            var basketTotalAmount = basket.Items.Join(products, basketItem => basketItem.ProductId, product => product.Id, (basketItem, product) => new
            {
                basketItem.Qty,
                product.Price
            }).Sum(item => item.Qty * item.Price);

            return basketTotalAmount <= 50 ? 10 : 20;
        }
    }
}
