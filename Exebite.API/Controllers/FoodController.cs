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
    [Route("api/food")]
    public class FoodController : ControllerBase
    {
        private readonly IFoodQueryRepository _foodQueryRepository;
        private readonly IFoodCommandRepository _foodCommandRepository;
        private readonly IEitherMapper _mapper;
        private readonly ILogger<FoodController> _logger;

        public FoodController(
            IFoodCommandRepository foodCommandRepository,
            IFoodQueryRepository foodRepository,
            IEitherMapper mapper,
            ILogger<FoodController> logger)
        {
            _foodQueryRepository = foodRepository;
            _foodCommandRepository = foodCommandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // [Authorize(Policy = nameof(AccessPolicy.CreateFoodAccessPolicy))]
        [HttpPost]
        public IActionResult Post([FromBody]CreateFoodDto model) =>
            _mapper.Map<FoodInsertModel>(model)
                   .Map(_foodCommandRepository.Insert)
                   .Map(x => AllOk(new { id = x }))
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        // [Authorize(Policy = nameof(AccessPolicy.UpdateFoodAccessPolicy))]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateFoodDto model) =>
            _mapper.Map<FoodUpdateModel>(model)
                   .Map(x => _foodCommandRepository.Update(id, x))
                   .Map(x => AllOk(new { result = x }))
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        // [Authorize(Policy = nameof(AccessPolicy.DeleteFoodAccessPolicy))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _foodCommandRepository.Delete(id)
                                  .Map(x => AllOk(new { removed = x }))
                                  .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                                  .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        // [Authorize(Policy = nameof(AccessPolicy.ReadFoodAccessPolicy))]
        [HttpGet("Query")]
        public IActionResult Query(FoodQueryModelDto query) =>
            _mapper.Map<FoodQueryModel>(query)
                   .Map(_foodQueryRepository.Query)
                   .Map(_mapper.Map<PagingResult<FoodDto>>)
                   .Map(AllOk)
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
