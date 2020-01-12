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

        [HttpPost("Shipping")]
        public async Task<ActionResult<decimal>> CalculateShippingAsync(Basket basket)
        {
            if (basket.Items == null || !basket.Items.Any())
            {
                return BadRequest("Basket is empty");
            }
            // Hans: the asp.net web api controller layer should be kept very simple and besides doing some validation and returning meaningful errors to the caller, it should simply delegate to the service layer (which represents the business logic)
            // Hans: Normally I would return the entire calculation (including SubTotalAmount and TotalAmount), but Challenge description says that only the shipping costs should be calculated on the server side
            return (await CalculationService.CalculateAmountsAsync(basket)).ShippingAmount;
        }
    }
}
