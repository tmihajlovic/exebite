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
    [Route("api/meal")]
    public class MealController : ControllerBase
    {
        private readonly IMealQueryRepository _queryRepo;
        private readonly IMealCommandRepository _commandRepo;
        private readonly IEitherMapper _mapper;
        private readonly ILogger<MealController> _logger;

        public MealController(
            IMealQueryRepository queryRepo,
            IMealCommandRepository commandRepo,
            IEitherMapper mapper,
            ILogger<MealController> logger)
        {
            _mapper = mapper;
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _logger = logger;
        }

        // [Authorize(Policy = nameof(AccessPolicy.CreateMealAccessPolicy))]
        [HttpPost]
        public IActionResult Post([FromBody]CreateMealDto model) =>
            _mapper.Map<MealInsertModel>(model)
                   .Map(_commandRepo.Insert)
                   .Map(x => Created(new { id = x }))
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        // [Authorize(Policy = nameof(AccessPolicy.UpdateMealAccessPolicy))]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateMealDto model) =>
            _mapper.Map<MealUpdateModel>(model)
                   .Map(x => _commandRepo.Update(id, x))
                   .Map(x => AllOk(new { updated = x }))
                   .Reduce(_ => NotFound(), error => error is RecordNotFound)
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        // [Authorize(Policy = nameof(AccessPolicy.DeleteMealAccessPolicy))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepo.Delete(id)
                        .Map(_ => OkNoContent())
                        .Reduce(_ => NotFound(), error => error is RecordNotFound)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        // [Authorize(Policy = nameof(AccessPolicy.ReadMealAccessPolicy))]
        [HttpGet("Query")]
        public IActionResult Query([FromQuery]MealQueryDto query) =>
            _mapper.Map<MealQueryModel>(query)
                   .Map(_queryRepo.Query)
                   .Map(_mapper.Map<PagingResult<MealDto>>)
                   .Map(AllOk)
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
