using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly IProductsProvider productProvider;

        public ProductsController(IProductsProvider productProvider)
        {
            this.productProvider = productProvider;
        }

        [HttpGet]
        public async Task<IActionResult> getProductsAsync()
        {
            var result = await this.productProvider.GetProductAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getProductById(int id)
        {
            var result = await this.productProvider.GetProductByIdAsync(id);
            if(result.IsSuccess)
            {
                return Ok(result.Product);
            }
            return NotFound();
        }
    }
}
