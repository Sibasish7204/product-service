using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Core.DTO
{
    public class DtoShoppingCart
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
