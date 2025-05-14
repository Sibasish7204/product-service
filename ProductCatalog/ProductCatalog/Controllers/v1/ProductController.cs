using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCatalog.Core.DTO;
using ProductCatalog.Core.Models;
using ProductCatalog.Data.DbModels;
using ProductCatalog.Data.Interfaces;
using ProductCatalog.Services.Interfaces;

namespace ProductCatalog.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IProductService productService)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            if (products == null)
            {
                return NotFound(JsonConvert.SerializeObject(GenericResponseModel<Product>.FailureResponse(
                    new List<ErrorModel> { ErrorLibrary.NotFound("Product") })));
            }
            var dtoProducts = _mapper.Map<List<DtoProduct>>(products);

            return Ok(JsonConvert.SerializeObject(GenericResponseModel<List<DtoProduct>>.SuccessResponse(dtoProducts)));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) { 
                
                return NotFound(JsonConvert.SerializeObject(GenericResponseModel<Product>.FailureResponse(
                    new List<ErrorModel> { ErrorLibrary.NotFound("Product")}))); 
            }

            var dtoProduct = _mapper.Map<DtoProduct>(product);
            return Ok(JsonConvert.SerializeObject(GenericResponseModel<DtoProduct>.SuccessResponse(dtoProduct)));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(DtoProduct dtoProduct)
        {
                var product = dtoProduct.Adapt<Product>();
                await _productService.Create(product);
                return Ok(JsonConvert.SerializeObject(GenericResponseModel<Product>.SuccessResponse(product)));            
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, Product updatedProduct)
        //{
        //    var product = await _unitOfWork.Products.GetByIdAsync(id);
        //    if (product == null) return NotFound();

        //    product.Name = updatedProduct.Name;
        //    product.Price = updatedProduct.Price;

        //    await _unitOfWork.CompleteAsync();
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var product = await _unitOfWork.Products.GetByIdAsync(id);
        //    if (product == null) return NotFound();

        //    _unitOfWork.Products.Remove(product);
        //    await _unitOfWork.CompleteAsync();
        //    return NoContent();
        //}
    }

}
