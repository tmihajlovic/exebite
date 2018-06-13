using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Exebite.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        // GET: api/Customer
        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerRepository.Get(0, int.MaxValue);
            return Ok(_mapper.Map<IEnumerable<CustomerModel>>(customers));
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _customerRepository.GetByID(id);
            return Ok(_mapper.Map<CustomerModel>(customer));
        }

        [HttpGet("Query")]
        public IActionResult Query(CustomerQueryModel query)
        {
            var customers = _customerRepository.Query(query);
            return Ok(_mapper.Map<IEnumerable<CustomerModel>>(customers));
        }

        // POST: api/Customer
        [HttpPost]
        public IActionResult Post([FromBody]CreateCustomerModel createModel)
        {
            var customer = _mapper.Map<Customer>(createModel);
            var id = _customerRepository.Insert(customer);
            return Ok(new { id });
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public IActionResult Put(UpdateCustomerModel customerViewModel)
        {
            var customer = _mapper.Map<Customer>(customerViewModel);
            _customerRepository.Update(customer);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _customerRepository.Delete(id);
            return NoContent();
        }
    }
}
