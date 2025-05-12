using ProductCatalog.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Services.Interfaces
{
    public interface IShoppingCartService
    {
        public Task<List<ShoppingCart>> GetByCustomerId(int custId);
        public Task AddToCart(ShoppingCart cart);
    }
}
