using System.Collections.Generic;
using AutoMapper;
using Either;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerQueryRepository _queryRepo;
        private readonly ICustomerCommandRepository _commandRepo;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerQueryRepository queryRepo, ICustomerCommandRepository commandRepo, IMapper mapper)
        {
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int page, int size) =>
            _queryRepo.Query(new CustomerQueryModel(page, size))
                      .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<CustomerDto>>(x.Items)))
                      .Reduce(InternalServerError);

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
            _queryRepo.Query(new CustomerQueryModel { Id = id })
                      .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<CustomerDto>>(x.Items)))
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                      .Reduce(InternalServerError);

        [HttpGet("Query")]
        public IActionResult Query(CustomerQueryModel query) =>
            _queryRepo.Query(_mapper.Map<CustomerQueryModel>(query))
                      .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<CustomerDto>>(x.Items)))
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                      .Reduce(InternalServerError);

        [HttpPost]
        public IActionResult Post([FromBody]CreateCustomerDto createModel) =>
            _commandRepo.Insert(_mapper.Map<CustomerInsertModel>(createModel))
                        .Map(x => (IActionResult)Ok(x))
                        .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                        .Reduce(InternalServerError);

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCustomerDto model) =>
            _commandRepo.Update(id, _mapper.Map<CustomerUpdateModel>(model))
                        .Map(x => (IActionResult)Ok(x))
                        .Reduce(_ => NotFound(), error => error is RecordNotFound)
                        .Reduce(InternalServerError);

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepo.Delete(id)
                        .Map(_ => (IActionResult)NoContent())
                        .Reduce(_ => NotFound(), error => error is RecordNotFound)
                        .Reduce(InternalServerError);
    }
}
