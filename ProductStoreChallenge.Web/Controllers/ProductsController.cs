using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductStoreChallenge.Data;

namespace ProductStoreChallenge.Controllers.Web
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductRepository ProductRepository { get; }

        public ProductsController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        [HttpGet]
        public Task<IEnumerable<Product>> GetAsync()
        {
            return ProductRepository.GetAllAsync();
        }
    }
}
