using Either;
using Exebite.API.Models;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/CustomerAliases")]
    [Authorize]
    public class CustomerAliasesController : ControllerBase
    {
        private readonly ICustomerAliasQueryRepository _queryRepo;
        private readonly ICustomerAliasCommandRepository _commandRepo;
        private readonly IEitherMapper _mapper;
        private readonly ILogger<CustomerAliasesController> _logger;

        public CustomerAliasesController(
            ICustomerAliasQueryRepository queryRepo,
            ICustomerAliasCommandRepository commandRepo,
            IEitherMapper mapper,
            ILogger<CustomerAliasesController> logger)
        {
            _mapper = mapper;
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateCustomerAliasDto model) =>
            _mapper.Map<CustomerAliasInsertModel>(model)
                        .Map(x => _commandRepo.Insert(x))
                        .Map(x => Created(new { id = x }))
                        .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateCustomerAliasDto model) =>
            _mapper.Map<CustomerAliasUpdateModel>(model)
                        .Map(x => _commandRepo.Update(id, x))
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
            _mapper.Map<CustomerAliasQueryModel>(query)
                      .Map(x => _queryRepo.Query(x))
                      .Map(_mapper.Map<PagingResult<CustomerAliasDto>>)
                      .Map(AllOk)
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
