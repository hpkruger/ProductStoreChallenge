using Microsoft.AspNetCore.Mvc;
using ProductStoreChallenge.Data;
using ProductStoreChallenge.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductStoreChallenge.Controllers.Web
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private IOrderRepository OrderRepository { get; }
        private IOrderService OrderService { get; }

        public OrdersController(IOrderRepository orderRepository, IOrderService orderService)
        {
            OrderRepository = orderRepository;
            OrderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetAsync(string id)
        {
            var order = await OrderRepository.GetAsync(id);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpGet]
        public Task<IEnumerable<Order>> GetAsync()
        {
            return OrderRepository.GetAllAsync();
        }

        [HttpPost]
        public Task<Order> PlaceOrderAsync(Order order)
        {
            return OrderService.PlaceOrderAsync(order);
        }
    }
}
