using System.Collections.Generic;
using AutoMapper;
using Either;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/customeraliases")]
    public class CustomerAliasesController : ControllerBase
    {
        private readonly ICustomerAliasQueryRepository _queryRepo;
        private readonly ICustomerAliasCommandRepository _commandRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerAliasesController> _logger;

        public CustomerAliasesController(
            ICustomerAliasQueryRepository queryRepo,
            ICustomerAliasCommandRepository commandRepo,
            IMapper mapper,
            ILogger<CustomerAliasesController> logger)
        {
            _mapper = mapper;
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateCustomerAliasDto model) =>
            _commandRepo.Insert(_mapper.Map<CustomerAliasInsertModel>(model))
                        .Map(x => Created(new { id = x }))
                        .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateCustomerAliasDto model) =>
            _commandRepo.Update(id, _mapper.Map<CustomerAliasUpdateModel>(model))
                        .Map(x => AllOk(new { updated = x }))
                        .Reduce(_ => NotFound(), error => error is RecordNotFound)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepo.Delete(id)
                        .Map(_ => OkNoContent())
                        .Reduce(_ => NotFound(), error => error is RecordNotFound)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("Query")]
        public IActionResult Query([FromQuery]CustomerAliasQueryDto query) =>
            _queryRepo.Query(_mapper.Map<CustomerAliasQueryModel>(query))
                      .Map(x => AllOk(_mapper.Map<PagingResult<CustomerAliasDto>>(x)))
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
