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
    [Route("api/Payment")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentQueryRepository _queryRepo;
        private readonly IPaymentCommandRepository _commandRepo;
        private readonly IEitherMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public PaymentController(
            IPaymentQueryRepository queryRepo,
            IPaymentCommandRepository commandRepo,
            IEitherMapper mapper,
            ILogger<CustomerController> logger)
        {
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreatePaymentDto createModel) =>
            _mapper.Map<PaymentInsertModel>(createModel)
                   .Map(_commandRepo.Insert)
                   .Map(x => Created(new { id = x }))
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatePaymentDto model) =>
            _mapper.Map<PaymentUpdateModel>(model)
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
        public IActionResult Query([FromQuery]PaymentQueryDto query) =>
            _mapper.Map<PaymentQueryModel>(query)
                      .Map(_queryRepo.Query)
                      .Map(_mapper.Map<PagingResult<PaymentDto>>)
                      .Map(AllOk)
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
