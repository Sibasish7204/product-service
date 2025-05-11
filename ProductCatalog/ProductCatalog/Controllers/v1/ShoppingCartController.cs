using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Data.DbModels;
using ProductCatalog.Data.Interfaces;

namespace ProductCatalog.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] ShoppingCart cart)
        {
            await _unitOfWork.ShoppingCarts.AddAsync(cart);
            await _unitOfWork.CompleteAsync();
            return Ok(cart);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCartByCustomer(int customerId)
        {
            var cartItems = await _unitOfWork.ShoppingCarts
                .FindAsync(c => c.CustomerId == customerId);

            return Ok(cartItems);
        }
    }

}
