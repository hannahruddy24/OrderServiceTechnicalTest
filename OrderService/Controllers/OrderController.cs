using BusinessLogic.Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost(Name = "PlaceOrder")]
        public async Task<IActionResult> PlaceOrderAsync(OrderDTO order)
        {
            if (!await _orderService.ValidateOrder(order) || !await _orderService.ProcessOrder(order))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}