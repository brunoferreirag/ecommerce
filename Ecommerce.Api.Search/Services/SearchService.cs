using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;

        public SearchService(IOrdersService orderService, IProductService productService, ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }
        public async  Task<(bool isSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await orderService.GetOrdersAsync(customerId);
            var productsResult = await productService.GetProductsAsync();
            var customerResult =await  customerService.getCustomersByIdAsync(customerId);
            if (ordersResult.IsSucess)
            {
                foreach(var order in ordersResult.Orders)
                {
                    foreach(var item in order.Itens)
                    {
                        item.ProductName = (productsResult.IsSucess)? productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name: "The product name is not available";
                    }
                }
                Customer customer = null;
                if (customerResult.IsSucess)
                {
                    customer = customerResult.Customer;
                }
                var result = new { ordersResult.Orders , customer};
                return (true, result);
            }
            return (false, null);
        }
    }
}
