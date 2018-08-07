using Either;
using Exebite.API.Authorization;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerQueryRepository _queryRepo;
        private readonly ICustomerCommandRepository _commandRepo;
        private readonly IEitherMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
            ICustomerQueryRepository queryRepo,
            ICustomerCommandRepository commandRepo,
            IEitherMapper mapper,
            ILogger<CustomerController> logger)
        {
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Policy = nameof(AccessPolicy.CreateCustomerAccessPolicy))]
        public IActionResult Post([FromBody]CreateCustomerDto createModel) =>
            _mapper.Map<CustomerInsertModel>(createModel)
                    .Map(_commandRepo.Insert)
                        .Map(x => Created(new { id = x }))
                        .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        [Authorize(Policy = nameof(AccessPolicy.UpdateCustomerAccessPolicy))]
        public IActionResult Put(int id, [FromBody] UpdateCustomerDto model) =>
            _mapper.Map<CustomerUpdateModel>(model)
                        .Map(x => _commandRepo.Update(id, x))
                        .Map(x => AllOk(new { updated = x }))
                        .Reduce(_ => NotFound(), error => error is RecordNotFound)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpDelete("{id}")]
        [Authorize(Policy = nameof(AccessPolicy.DeleteCustomerAccessPolicy))]
        public IActionResult Delete(int id) =>
            _commandRepo.Delete(id)
                        .Map(_ => OkNoContent())
                        .Reduce(_ => NotFound(), error => error is RecordNotFound)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("Query")]
        [ProducesResponseType(200, Type = typeof(PagingResult<CustomerDto>))]
        [ProducesResponseType(500, Type = typeof(PagingResult<CustomerDto>))]
        [Authorize(Policy = nameof(AccessPolicy.ReadCustomerAccessPolicy))]
        public IActionResult Query([FromQuery]CustomerQueryDto query) =>
            _mapper.Map<CustomerQueryModel>(query)
                      .Map(_queryRepo.Query, x => _logger.LogTrace("Query called"))
                      .Map(_mapper.Map<PagingResult<CustomerDto>>)
                      .Map(_ => InternalServerError())
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
