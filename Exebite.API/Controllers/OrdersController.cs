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
    [Route("api/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderQueryRepository _queryRepo;
        private readonly IOrderCommandRepository _commandRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(
            IOrderQueryRepository queryRepo,
            IOrderCommandRepository commandRepo,
            IMapper mapper,
            ILogger<OrdersController> logger)
        {
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderDto model) =>
            _commandRepo.Insert(_mapper.Map<OrderInsertModel>(model))
                        .Map(x => Created(new { id = x }))
                        .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateOrderDto model) =>
            _commandRepo.Update(id, _mapper.Map<OrderUpdateModel>(model))
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
            _queryRepo.Query(_mapper.Map<OrderQueryModel>(query))
                      .Map(x => AllOk(_mapper.Map<PagingResult<OrderDto>>(x)))
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("GetAllOrdersForRestaurant")]
        public IActionResult GetAllOrdersForRestaurant(int restaurantId, int page, int size) =>
            _queryRepo.GetAllOrdersForRestaurant(restaurantId, page, size)
                      .Map(x => AllOk(_mapper.Map<PagingResult<OrderDto>>(x)))
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}