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
    [Route("api/location")]
    [Authorize]
    public class LocationController : ControllerBase
    {
        private readonly ILocationCommandRepository _commandRepository;
        private readonly ILocationQueryRepository _queryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LocationController> _logger;

        public LocationController(
            ILocationCommandRepository commandRepository,
            ILocationQueryRepository queryRepository,
            IMapper mapper,
            ILogger<LocationController> logger)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get() =>
            _queryRepository.Query(new LocationQueryModel())
                            .Map(x => AllOk(_mapper.Map<PagingResult<LocationDto>>(x.Items)))
                            .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
             _queryRepository.Query(new LocationQueryModel { Id = id })
                             .Map(x => AllOk(_mapper.Map<PagingResult<RestaurantDto>>(x.Items)))
                             .Reduce(_ => NotFound(), error => error is RecordNotFound)
                             .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPost]
        public IActionResult Post([FromBody]CreateLocationDto model) =>
            _commandRepository.Insert(_mapper.Map<LocationInsertModel>(model))
                              .Map(x => AllOk(new { id = x }))
                              .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                              .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateLocationDto model) =>
            _commandRepository.Update(id, _mapper.Map<LocationUpdateModel>(model))
                              .Map(x => AllOk(new { updated = x }))
                              .Reduce(_ => NotFound(), error => error is RecordNotFound)
                              .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                              .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepository.Delete(id)
                              .Map(_ => OkNoContent())
                              .Reduce(_ => NotFound(), error => error is RecordNotFound)
                              .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("Query")]
        public IActionResult Query(LocationQueryDto query) =>
            _queryRepository.Query(_mapper.Map<LocationQueryModel>(query))
                            .Map(x => AllOk(_mapper.Map<PagingResult<LocationDto>>(x)))
                            .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                            .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}