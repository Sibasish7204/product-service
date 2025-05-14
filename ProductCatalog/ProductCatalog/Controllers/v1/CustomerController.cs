using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCatalog.Core.DTO;
using ProductCatalog.Core.Models;
using ProductCatalog.Data.DbModels;
using ProductCatalog.Data.Interfaces;

namespace ProductCatalog.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            if (customers == null)
            {
                return NotFound(JsonConvert.SerializeObject(GenericResponseModel<Product>.FailureResponse(
                    new List<ErrorModel> { ErrorLibrary.NotFound("Customer") })));
            }
            return Ok(JsonConvert.SerializeObject(GenericResponseModel<List<Customer>>.SuccessResponse(customers.ToList())));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound(JsonConvert.SerializeObject(GenericResponseModel<Product>.FailureResponse(
                    new List<ErrorModel> { ErrorLibrary.NotFound("Customer") })));
            }
            return Ok(JsonConvert.SerializeObject(GenericResponseModel<Customer>.SuccessResponse(customer)));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Customer updatedCustomer)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null) return NotFound();

            customer.Name = updatedCustomer.Name;

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null) return NotFound();

            _unitOfWork.Customers.Remove(customer);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }

}
