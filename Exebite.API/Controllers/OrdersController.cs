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
    [Route("api/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderQueryRepository _queryRepo;
        private readonly IOrderCommandRepository _commandRepo;
        private readonly IEitherMapper _mapper;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(
            IOrderQueryRepository queryRepo,
            IOrderCommandRepository commandRepo,
            IEitherMapper mapper,
            ILogger<OrdersController> logger)
        {
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderDto model) =>
            _mapper.Map<OrderInsertModel>(model)
                   .Map(_commandRepo.Insert)
                   .Map(x => Created(new { id = x }))
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateOrderDto model) =>
            _mapper.Map<OrderUpdateModel>(model)
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
        public IActionResult Query([FromQuery]OrderQueryDto query) =>
            _mapper.Map<OrderQueryModel>(query)
                   .Map(_queryRepo.Query)
                   .Map(_mapper.Map<PagingResult<OrderDto>>)
                   .Map(x => AllOk(x))
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("GetAllOrdersForRestaurant")]
        public IActionResult GetAllOrdersForRestaurant(int restaurantId, int page, int size) =>
            _queryRepo.GetAllOrdersForRestaurant(restaurantId, page, size)
                      .Map(_mapper.Map<PagingResult<OrderDto>>)
                      .Map(AllOk)
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}