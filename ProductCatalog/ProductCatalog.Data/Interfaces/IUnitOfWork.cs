using ProductCatalog.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IRepository<Product> Products { get; }
        IRepository<ShoppingCart> ShoppingCarts { get; }
        IRepository<User> Users { get; }
        public IUserRepository UserRepo { get; }

        Task<int> CompleteAsync();
    }
}
