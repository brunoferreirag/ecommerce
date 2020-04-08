using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomersDbContext context;
        private readonly ILogger<CustomerProvider> logger;
        private readonly IMapper mapper;
        public CustomerProvider(CustomersDbContext context, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!context.Customers.Any())
            {
                Customer customer = new Customer();
                customer.Address = "Antonio Araújo Hummel, 245";
                customer.Id = 1;
                customer.Name = "Bruno Ferreira Gonçalves";

                Customer customer1 = new Customer();
                customer1.Address = "Teste";
                customer1.Id = 2;
                customer1.Name = "Teste Teste";

                context.Customers.Add(customer);
                context.Customers.Add(customer1);

                context.SaveChanges();
            }
        }

        public async Task<(bool IsSucess, IEnumerable<Model.Customer> Customers, String ErrorMessage)> getCustomersAsync()
        {
            try
            {
                var customers = await context.Customers.ToListAsync();
                if(customers!=null && customers.Any())
                {
                    return (true, mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Model.Customer>>(customers), null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSucess, Model.Customer Customer, String ErrorMessage)> getCustomersByIdAsync(int id)
        {
            try
            {
                var customer = await context.Customers.FirstOrDefaultAsync(p=>p.Id ==id);
                if (customer != null)
                {
                    return (true, mapper.Map<Db.Customer, Model.Customer>(customer), null);
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
