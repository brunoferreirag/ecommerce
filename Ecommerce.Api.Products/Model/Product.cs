using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Model
{
    public class Product
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public decimal price { get; set; }

        public int Inventory { get; set; }
    }
}
