using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MongoDeployTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id).ConfigureAwait(false);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _customerService.CreateAsync(customer);
            return Ok(customer.Id);
        }

        [HttpPut("Id")]
        public async Task<IActionResult> Update(Guid id, Customer customerIn)
        {
            var customer = await _customerService.GetByIdAsync(id).ConfigureAwait(false);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.UpdateAsync(id, customerIn).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id).ConfigureAwait(false);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.DeleteAsync(customer.Id).ConfigureAwait(false);
            return NoContent();
        }
    }
}