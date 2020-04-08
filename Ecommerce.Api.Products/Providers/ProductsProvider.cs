using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductDbContext context;
        private ILogger<ProductsProvider> logger;
        private IMapper mapper;
        public ProductsProvider(ProductDbContext context, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }
        public async Task<(bool IsSuccess, IEnumerable<Model.Product> Products, string ErrorMessage)> GetProductAsync()
        {
            try
            {
                var products = await context.Products.ToListAsync();
                if(products!=null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Model.Product>>(products);
                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch(Exception ex )
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Model.Product Product, string ErrorMessage)> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await context.Products.FirstOrDefaultAsync(p=>p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Model.Product>(product);
                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        private void SeedData()
        {
            if (!context.Products.Any())
            {
                context.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Inventory = 20, Price = 10 });
                context.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Inventory = 5, Price = 10 });
                context.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Inventory = 150, Price = 10 });
                context.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Inventory = 200, Price = 10 });
                context.SaveChanges();
            }
        }
    }
}
