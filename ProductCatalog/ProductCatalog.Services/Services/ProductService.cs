using ProductCatalog.Data.DbModels;
using ProductCatalog.Data.Interfaces;
using ProductCatalog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(Product product)
        {
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return products.ToList();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product != null)
                return product;
            else
                return null;
        }
    }
}
