using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/customeraliases")]
    public class CustomerAliasesController : Controller
    {
        private readonly ICustomerAliasRepository _customerAliasRepository;
        private readonly IMapper _exebiteMapper;

        public CustomerAliasesController(ICustomerAliasRepository customerAliasesRepository, IMapper exebiteMapper)
        {
            _exebiteMapper = exebiteMapper;
            _customerAliasRepository = customerAliasesRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var customerAliases = _exebiteMapper.Map<IEnumerable<MealModel>>(_customerAliasRepository.Get(0, int.MaxValue));
            return Ok(customerAliases);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customerAlias = _customerAliasRepository.GetByID(id);
            if (customerAlias == null)
            {
                return NotFound();
            }

            return Ok(_exebiteMapper.Map<CustomerAliasModel>(customerAlias));
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateCustomerAliasModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var createdCustomerAlias = _customerAliasRepository.Insert(_exebiteMapper.Map<Model.CustomerAliases>(model));
            return Ok(new { createdCustomerAlias.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateCustomerAliasModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var currentCustomerAlias = _customerAliasRepository.GetByID(id);
            if (currentCustomerAlias == null)
            {
                return NotFound();
            }

            _exebiteMapper.Map(model, currentCustomerAlias);

            var updatedMeal = _customerAliasRepository.Update(currentCustomerAlias);
            return Ok(new { updatedMeal.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _customerAliasRepository.Delete(id);
            return NoContent();
        }
    }
}
