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
            // Hans: In the real-world, structured logging should be used throughout the code base, e.g. inject ILogger<ProductsController> here
            ProductRepository = productRepository;
        }

        [HttpGet]
        public Task<IEnumerable<Product>> GetAsync()
        {
            return ProductRepository.GetAllAsync();
        }
    }
}
