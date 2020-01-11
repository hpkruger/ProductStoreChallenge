using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductStoreChallenge.Services;
using ProductStoreChallenge.Data;

namespace ProductStoreChallenge.Controllers.Web
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationsController : ControllerBase
    {
        private ICalculationService CalculationService { get; }

        public CalculationsController(ICalculationService calculationService)
        {
            // Hans: In the real-world, structured logging should be used throughout the code base, e.g. inject ILogger<CalculationsController> here
            CalculationService = calculationService;
        }

        [HttpPost("/ShippingCosts")]
        public async Task<ActionResult<double>> CalculateShippingCostsAsync(Basket basket)
        {
            if (basket.Items == null || !basket.Items.Any())
            {
                return BadRequest("Basket is empty");
            }
            // Hans: the asp.net web api controller layer should be kept very simple and besides doing some validation and returning meaningful errors to the caller, it should simply delegate to the service layer (which represents the business logic)
            return await CalculationService.CalculateShippingCostsAsync(basket);
        }
    }
}
