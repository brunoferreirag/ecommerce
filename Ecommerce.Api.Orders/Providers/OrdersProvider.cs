using AutoMapper;
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext context;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext context, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!context.Orders.Any())
            {
                OrderItem item1 = new OrderItem();
                item1.Id = 1;
                item1.OrderId = 1;
                item1.ProductId = 1;
                item1.Quantity = 5;
                item1.UnitPrice = 10;
                List<OrderItem> itens = new List<OrderItem>();
                itens.Add(item1);
                Order order = new Order();
                order.Itens = itens;
                order.OrderDate = DateTime.Now;
                order.Total = 50;
                order.CustomerId = 1;

                OrderItem item2 = new OrderItem();
                item2.Id = 2;
                item2.OrderId = 2;
                item2.ProductId = 2;
                item2.Quantity = 2;
                item2.UnitPrice = 10;
                List<OrderItem> itens2 = new List<OrderItem>();
                itens.Add(item2);
                Order order2 = new Order();
                order2.Itens = itens;
                order2.OrderDate = DateTime.Now;
                order2.Total = 20;
                order2.CustomerId = 2;

                context.OrderItems.Add(item1);
                context.OrderItems.Add(item2);
                context.Orders.Add(order);
                context.Orders.Add(order2);

                context.SaveChanges();

            }
        }

        public async Task<(bool IsSucess, IEnumerable<Model.Order> Orders, String ErrorMessage)> GetOrdersAsync()
        {
            try
            {
                var orders = await this.context.Orders.ToListAsync();
                if (orders != null && orders.Any())
                {
                    return (true, mapper.Map<IEnumerable<Db.Order>, IEnumerable<Model.Order>>(orders), null);
                }
                return (false, null, "Not Found");

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }

        public async Task<(bool IsSucess, IEnumerable<Model.Order> Orders, String ErrorMessage)> GetOrderByIdAsync(int Id)
        {
            try
            {
                var orders = await this.context.Orders.Where(p=>p.CustomerId ==Id).Include(p=>p.Itens).ToListAsync();
                if (orders != null && orders.Any())
                {
                    return (true, mapper.Map<IEnumerable<Order>, IEnumerable<Model.Order>>(orders), null);
                }
                return (false, null, "Not Found");

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }
    }
}
