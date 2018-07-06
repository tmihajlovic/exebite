using AutoMapper;
using Either;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/restaurant")]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantQueryRepository _queryRepository;
        private readonly IRestaurantCommandRepository _commandRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(
            IRestaurantQueryRepository queryRepository,
            IRestaurantCommandRepository commandRepository,
            IMapper mapper,
            ILogger<RestaurantController> logger)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody]RestaurantInsertModelDto restaurant) =>
            _commandRepository.Insert(_mapper.Map<RestaurantInsertModel>(restaurant))
                              .Map(x => Created(new { id = x }))
                              .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                              .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]RestaurantUpdateModelDto restaurant) =>
            _commandRepository.Update(id, _mapper.Map<RestaurantUpdateModel>(restaurant))
                              .Map(x => AllOk(new { updated = x }))
                              .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.LogError(x.ToString()))
                              .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepository.Delete(id)
                              .Map(_ => (IActionResult)NoContent())
                              .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.LogError(x.ToString()))
                              .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("Query")]
        public IActionResult Query([FromQuery]RestaurantQueryDto query) =>
            _queryRepository.Query(_mapper.Map<RestaurantQueryModel>(query))
                            .Map(_mapper.Map<PagingResult<RestaurantDto>>)
                            .Map(AllOk)
                            .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                            .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
