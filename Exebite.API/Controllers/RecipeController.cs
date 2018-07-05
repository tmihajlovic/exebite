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
    [Route("api/recipe")]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeQueryRepository _queryRepository;
        private readonly IRecipeCommandRepository _commandRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(
            IRecipeQueryRepository queryRepository,
            IRecipeCommandRepository commandRepository,
            IMapper mapper,
            ILogger<RecipeController> logger)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody]RecipeInsertModelDto Recipe) =>
            _commandRepository.Insert(_mapper.Map<RecipeInsertModel>(Recipe))
                              .Map(x => Created(new { id = x }))
                              .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                              .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateRecipeDto Recipe) =>
            _commandRepository.Update(id, _mapper.Map<RecipeUpdateModel>(Recipe))
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
        public IActionResult Query([FromQuery]RecipeQueryDto query) =>
            _queryRepository.Query(_mapper.Map<RecipeQueryModel>(query))
                            .Map(x => AllOk(_mapper.Map<PagingResult<RecipeDto>>(x)))
                            .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                            .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
