using Microsoft.AspNetCore.Mvc;
using UnitTestingUsingMoq.Models;
using UnitTestingUsingMoq.Repositories;

namespace UnitTestingUsingMoq.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;

        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _orderRepo.AddOrder(order);
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _orderRepo.GetByOrderId(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Order order)
        {
            await _orderRepo.UpdateOrder(order);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderRepo.DeleteOrder(id);
            return NoContent();
        }
    }
}
