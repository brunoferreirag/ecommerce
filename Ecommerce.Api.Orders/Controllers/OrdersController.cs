using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider provider;

        public OrdersController(IOrdersProvider provider)
        {
            this.provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var orders = await this.provider.GetOrdersAsync();
            if (orders.IsSucess)
            {
                return Ok(orders.Orders);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersByIdAsync(int id)
        {
            var order = await this.provider.GetOrderByIdAsync(id);
            if (order.IsSucess)
            {
                return Ok(order.Orders);
            }
            return NotFound();
        }
    }
}