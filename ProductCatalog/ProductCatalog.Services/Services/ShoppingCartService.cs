using ProductCatalog.Data.DbModels;
using ProductCatalog.Data.Interfaces;
using ProductCatalog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Services.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartService(IUnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork;
        }

        public async Task AddToCart(ShoppingCart cart)
        {
            await _unitOfWork.ShoppingCarts.AddAsync(cart);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<ShoppingCart>> GetByCustomerId(int customerId)
        {
            var cartItems = await _unitOfWork.ShoppingCarts
                .FindAllIncludingAsync(c => c.CustomerId == customerId, 
                c=>c.Product, 
                c=>c.Customer);
            return cartItems.ToList();
        }
    }
}
