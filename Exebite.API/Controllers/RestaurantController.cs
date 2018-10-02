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
    // [Authorize]
    [Produces("application/json")]
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantQueryRepository _queryRepository;
        private readonly IRestaurantCommandRepository _commandRepository;
        private readonly IEitherMapper _mapper;
        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(
            IRestaurantQueryRepository queryRepository,
            IRestaurantCommandRepository commandRepository,
            IEitherMapper mapper,
            ILogger<RestaurantController> logger)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // [Authorize(Policy = nameof(AccessPolicy.CreateRestaurantAccessPolicy))]
        [HttpPost]
        public IActionResult Post([FromBody]RestaurantInsertModelDto restaurant) =>
            _mapper.Map<RestaurantInsertModel>(restaurant)
                   .Map(_commandRepository.Insert)
                   .Map(x => Created(new { id = x }))
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        // [Authorize(Policy = nameof(AccessPolicy.UpdateRestaurantAccessPolicy))]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]RestaurantUpdateModelDto restaurant) =>
            _mapper.Map<RestaurantUpdateModel>(restaurant)
                   .Map(x => _commandRepository.Update(id, x))
                   .Map(x => AllOk(new { updated = x }))
                   .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.LogError(x.ToString()))
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        // [Authorize(Policy = nameof(AccessPolicy.DeleteRestaurantAccessPolicy))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepository.Delete(id)
                              .Map(_ => (IActionResult)NoContent())
                              .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.LogError(x.ToString()))
                              .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        // [Authorize(Policy = nameof(AccessPolicy.ReadRestaurantAccessPolicy))]
        [HttpGet("Query")]
        public IActionResult Query([FromQuery]RestaurantQueryDto query) =>
            _mapper.Map<RestaurantQueryModel>(query)
                   .Map(_queryRepository.Query)
                   .Map(_mapper.Map<PagingResult<RestaurantDto>>)
                   .Map(AllOk)
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
