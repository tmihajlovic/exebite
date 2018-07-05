using AutoMapper;
using Either;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
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
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
            ICustomerQueryRepository queryRepo,
            ICustomerCommandRepository commandRepo,
            IMapper mapper,
            ILogger<CustomerController> logger)
        {
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(int page, int size) =>
            _queryRepo.Query(new CustomerQueryModel(page, size))
                      .Map(x => AllOk(_mapper.Map<PagingResult<CustomerDto>>(x)))
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
            _queryRepo.Query(new CustomerQueryModel { Id = id })
                      .Map(x => AllOk(_mapper.Map<PagingResult<CustomerDto>>(x)))
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPost]
        public IActionResult Post([FromBody]CreateCustomerDto createModel) =>
            _commandRepo.Insert(_mapper.Map<CustomerInsertModel>(createModel))
                        .Map(x => Created(new { id = x }))
                        .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCustomerDto model) =>
            _commandRepo.Update(id, _mapper.Map<CustomerUpdateModel>(model))
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
        public IActionResult Query([FromQuery]CustomerQueryDto query) =>
            _queryRepo.Query(_mapper.Map<CustomerQueryModel>(query))
                      .Map(x => AllOk(_mapper.Map<PagingResult<CustomerDto>>(x)))
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
