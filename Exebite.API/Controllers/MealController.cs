using System.Collections.Generic;
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
    [Route("api/meal")]
    [Authorize]
    public class MealController : ControllerBase
    {
        private readonly IMealQueryRepository _queryRepo;
        private readonly IMealCommandRepository _commandRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<MealController> _logger;

        public MealController(
            IMealQueryRepository queryRepo,
            IMealCommandRepository commandRepo,
            IMapper mapper,
            ILogger<MealController> logger)
        {
            _mapper = mapper;
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(int page, int size) =>
            _queryRepo.Query(new MealQueryModel(page, size))
                      .Map(x => AllOk(_mapper.Map<PagingResult<MealDto>>(x)))
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
            _queryRepo.Query(new MealQueryModel { Id = id })
                      .Map(x => AllOk(_mapper.Map<PagingResult<MealDto>>(x)))
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPost]
        public IActionResult Post([FromBody]CreateMealDto model) =>
            _commandRepo.Insert(_mapper.Map<MealInsertModel>(model))
                        .Map(x => Created(new { id = x }))
                        .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateMealDto model) =>
            _commandRepo.Update(id, _mapper.Map<MealUpdateModel>(model))
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
        public IActionResult Query([FromQuery]MealQueryDto query) =>
            _queryRepo.Query(_mapper.Map<MealQueryModel>(query))
                      .Map(x => AllOk(_mapper.Map<PagingResult<MealDto>>(x)))
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
