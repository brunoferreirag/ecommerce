using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool IsSucess, IEnumerable<Model.Customer> Customers, String ErrorMessage)> getCustomersAsync();
        Task<(bool IsSucess, Model.Customer Customer, String ErrorMessage)> getCustomersByIdAsync(int id);
    }
}
