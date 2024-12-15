using backend.Models;
using backend.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _customerRepository.GetAll();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _customerRepository.Add(customer);
            _customerRepository.Save();

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Customer customer)
        {
            if (id != customer.Id)
                return BadRequest("ID mismatch");

            var existingCustomer = _customerRepository.GetById(id);
            if (existingCustomer == null)
                return NotFound();

            _customerRepository.Update(customer);
            _customerRepository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
                return NotFound();

            _customerRepository.Delete(id);
            _customerRepository.Save();

            return NoContent();
        }
    }
}
