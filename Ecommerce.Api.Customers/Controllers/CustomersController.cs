using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Api.Customers.Interfaces;
using Ecommerce.Api.Customers.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerProvider provider;
        public CustomersController(ICustomerProvider provider)
        {
            this.provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var customers = await this.provider.getCustomersAsync();
            if (customers.IsSucess)
            {
                return Ok(customers.Customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomersByIdAsync(int id)
        {
            var customer = await this.provider.getCustomersByIdAsync(id);
            if (customer.IsSucess)
            {
                return Ok(customer.Customer);
            }
            return NotFound();
        }
    }
}