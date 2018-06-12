using System.Linq;
using Exebite.DataAccess.Repositories;
using Exebite.Model;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    //    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerRepository.Get(0, int.MaxValue);

            return Ok(customers.Select(x => new
            {
                x.Id,
                x.AppUserId,
                x.Balance,
                x.Name,
                x.LocationId
            }));
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _customerRepository.GetByID(id);
            return Ok(new
            {
                customer.Id,
                customer.AppUserId,
                customer.Balance,
                customer.Name,
                customer.LocationId
            });
        }

        // POST: api/Customer
        [HttpPost]
        public IActionResult Post([FromBody]Customer value)
        {
            var id = _customerRepository.Insert(value);
            return Ok(new { id });
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            // _customerRepository.Update()
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
