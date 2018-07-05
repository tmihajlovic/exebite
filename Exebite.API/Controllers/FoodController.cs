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
    [Route("api/food")]
    [Authorize]
    public class FoodController : ControllerBase
    {
        private readonly IFoodQueryRepository _foodQueryRepository;
        private readonly IFoodCommandRepository _foodCommandRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<FoodController> _logger;

        public FoodController(
            IFoodCommandRepository foodCommandRepository,
            IFoodQueryRepository foodRepository,
            IMapper mapper,
            ILogger<FoodController> logger)
        {
            _foodQueryRepository = foodRepository;
            _foodCommandRepository = foodCommandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
             _foodQueryRepository.Query(new FoodQueryModel() { Id = id })
                                 .Map(x => AllOk(_mapper.Map<PagingResult<FoodDto>>(x)))
                                 .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPost]
        public IActionResult Post([FromBody]CreateFoodDto model) =>
            _foodCommandRepository.Insert(_mapper.Map<FoodInsertModel>(model))
                                  .Map(x => AllOk(new { id = x }))
                                  .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                                  .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateFoodDto model) =>
            _foodCommandRepository.Update(id, _mapper.Map<FoodUpdateModel>(model))
                                  .Map(x => AllOk(new { result = x }))
                                  .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                                  .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _foodCommandRepository.Delete(id)
                                  .Map(x => AllOk(new { removed = x }))
                                  .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                                  .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("Query")]
        public IActionResult Query(FoodQueryModelDto query) =>
            _foodQueryRepository.Query(_mapper.Map<FoodQueryModel>(query))
                                .Map(x => AllOk(_mapper.Map<PagingResult<FoodDto>>(x)))
                                .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                                .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
