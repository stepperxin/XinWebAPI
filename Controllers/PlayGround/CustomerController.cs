using Microsoft.AspNetCore.Mvc;
using XinWebAPI.Models.DTO.PlayGround;
using XinWebAPI.Models.PlayGround;
using XinWebAPI.Services.PlayGround;

namespace XinWebAPI.Controllers.PlayGround
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> getCustomers()
        {
            var customer = await _customerService.getAllCustomersAsync();
            return Ok(customer);

        }

        [HttpPost("add")]
        public async Task<IActionResult> addCustomer(CustomerDTO customerDTO)
        {
            //var customer = await _customerService.getCustomer(customerDTO.Id);
            //if(customer == null)
            //{
            if (customerDTO.Id > 0) customerDTO.Id = 0;
            var customer = await _customerService.addCustomer(customerDTO);
            return Ok(customer);
            //}
            //else
            //{
            //    return BadRequest($"Employee with Id = {customerDTO.Id} already exist");
            //}
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getCustomer(int id)
        {
            var customer = await _customerService.getCustomer(id);
            if (customer == null)
                return NotFound($"Employee with Id = {id} not found");
            else
                return Ok(customer);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> updateCustomer(int id, [FromBody] CustomerDTO customerDTO)
        {
            try
            {
                var customer = await _customerService.updateCustomer(id, customerDTO);
                if (customer == null)
                    return NotFound($"Employee with Id = {id} not found");
                else
                    return Ok(customer);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating customer with Id = {id} (Ex: {ex.Message})");
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> deleteCustomer(int id)
        {
            try
            {
                var customerToDelete = await _customerService.getCustomer(id);

                if (customerToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                return await _customerService.deleteCustomer(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
