using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.DTO;
using ProductCatalog.Data.DbModels;
using ProductCatalog.Data.Interfaces;
using ProductCatalog.Services.Interfaces;

namespace ProductCatalog.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCart;
        private readonly IMapper _mapper;

        public ShoppingCartController(IShoppingCartService shoppingCart, IMapper mapper)
        {
            _shoppingCart = shoppingCart;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] DtoShoppingCartCreate cart)
        {
            var shoppingCart = cart.Adapt<ShoppingCart>();
            _shoppingCart.AddToCart(shoppingCart);
            return Ok(cart);
        }

        [Authorize]
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCartByCustomer(int customerId)
        {
           var cartItems = await _shoppingCart.GetByCustomerId(customerId);
            var dtoCarts = _mapper.Map<List<DtoShoppingCart>>(cartItems);
            return Ok(dtoCarts);
        }
    }

}
