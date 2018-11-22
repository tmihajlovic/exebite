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
    [Route("api/location")]
    [Authorize]
    public class LocationController : ControllerBase
    {
        private readonly ILocationCommandRepository _commandRepository;
        private readonly ILocationQueryRepository _queryRepository;
        private readonly IEitherMapper _mapper;
        private readonly ILogger<LocationController> _logger;

        public LocationController(
            ILocationCommandRepository commandRepository,
            ILocationQueryRepository queryRepository,
            IEitherMapper mapper,
            ILogger<LocationController> logger)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Policy = nameof(AccessPolicy.CreateLocationAccessPolicy))]
        public IActionResult Post([FromBody]CreateLocationDto model) =>
            _mapper.Map<LocationInsertModel>(model)
                   .Map(_commandRepository.Insert)
                   .Map(x => AllOk(new { id = x }))
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        [Authorize(Policy = nameof(AccessPolicy.UpdateLocationAccessPolicy))]
        public IActionResult Put(int id, [FromBody]UpdateLocationDto model) =>
            _mapper.Map<LocationUpdateModel>(model)
                   .Map(x => _commandRepository.Update(id, x))
                   .Map(x => AllOk(new { updated = x }))
                   .Reduce(_ => NotFound(), error => error is RecordNotFound)
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpDelete("{id}")]
        [Authorize(Policy = nameof(AccessPolicy.DeleteLocationAccessPolicy))]
        public IActionResult Delete(int id) =>
            _commandRepository.Delete(id)
                              .Map(_ => OkNoContent())
                              .Reduce(_ => NotFound(), error => error is RecordNotFound)
                              .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("Query")]
        [Authorize(Policy = nameof(AccessPolicy.ReadLocationAccessPolicy))]
        public IActionResult Query(LocationQueryDto query) =>
            _mapper.Map<LocationQueryModel>(query)
                   .Map(_queryRepository.Query)
                   .Map(_mapper.Map<PagingResult<LocationDto>>)
                   .Map(AllOk)
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}