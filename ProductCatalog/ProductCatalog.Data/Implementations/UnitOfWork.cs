using ProductCatalog.Data.DbContext;
using ProductCatalog.Data.DbModels;
using ProductCatalog.Data.Implementations.ProductApi.Data;
using ProductCatalog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Product> Products { get; }
        public IRepository<Customer> Customers { get; }
        public IRepository<ShoppingCart> ShoppingCarts { get; }

        public IRepository<User> Users { get; }

        public IUserRepository UserRepo { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new Repository<Product>(_context);
            Customers = new Repository<Customer>(_context);
            ShoppingCarts = new Repository<ShoppingCart>(_context);
            Users = new Repository<User>(_context);
            UserRepo = new UserRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            try
            {                
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) { throw; }
        }

        public void Dispose() => _context.Dispose();
    }

}
