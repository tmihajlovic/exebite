using System.Collections.Generic;
using AutoMapper;
using Either;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/customeraliases")]
    public class CustomerAliasesController : ControllerBase
    {
        private readonly ICustomerAliasQueryRepository _queryRepo;
        private readonly ICustomerAliasCommandRepository _commandRepo;
        private readonly IMapper _mapper;

        public CustomerAliasesController(
            ICustomerAliasQueryRepository queryRepo,
            ICustomerAliasCommandRepository commandRepo,
            IMapper mapper)
        {
            _mapper = mapper;
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
        }

        [HttpGet]
        public IActionResult Get(int page, int size) =>
            _queryRepo.Query(new CustomerAliasQueryModel(page, size))
                      .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<CustomerAliasDto>>(x.Items)))
                      .Reduce(InternalServerError);

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
            _queryRepo.Query(new CustomerAliasQueryModel { Id = id })
                      .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<CustomerAliasDto>>(x.Items)))
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                      .Reduce(InternalServerError);

        [HttpPost]
        public IActionResult Post([FromBody]CreateCustomerAliasDto model) =>
            _commandRepo.Insert(_mapper.Map<CustomerAliasInsertModel>(model))
                        .Map(x => (IActionResult)Ok(x))
                        .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                        .Reduce(InternalServerError);

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateCustomerAliasDto model) =>
            _commandRepo.Update(id, _mapper.Map<CustomerAliasUpdateModel>(model))
                        .Map(x => (IActionResult)Ok(x))
                        .Reduce(_ => NotFound(), error => error is RecordNotFound)
                        .Reduce(InternalServerError);

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepo.Delete(id)
                        .Map(_ => (IActionResult)NoContent())
                        .Reduce(_ => NotFound(), error => error is RecordNotFound)
                        .Reduce(InternalServerError);

        [HttpGet("Query")]
        public IActionResult Query(CustomerAliasQueryModel query) =>
            _queryRepo.Query(_mapper.Map<CustomerAliasQueryModel>(query))
                      .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<CustomerAliasDto>>(x.Items)))
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                      .Reduce(InternalServerError);
    }
}
