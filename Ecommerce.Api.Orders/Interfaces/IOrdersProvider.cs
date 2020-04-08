using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSucess, IEnumerable<Model.Order> Orders, String ErrorMessage)> GetOrdersAsync();
        Task<(bool IsSucess, IEnumerable<Model.Order> Orders, String ErrorMessage)> GetOrderByIdAsync(int Id);
    }
}
