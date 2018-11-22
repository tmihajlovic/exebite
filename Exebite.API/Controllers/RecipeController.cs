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
    [Route("api/recipe")]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeQueryRepository _queryRepository;
        private readonly IRecipeCommandRepository _commandRepository;
        private readonly IEitherMapper _mapper;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(
            IRecipeQueryRepository queryRepository,
            IRecipeCommandRepository commandRepository,
            IEitherMapper mapper,
            ILogger<RecipeController> logger)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Policy = nameof(AccessPolicy.CreateRecipeAccessPolicy))]
        public IActionResult Post([FromBody]RecipeInsertModelDto recipe) =>
            _mapper.Map<RecipeInsertModel>(recipe)
                   .Map(_commandRepository.Insert)
                   .Map(x => Created(new { id = x }))
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        [Authorize(Policy = nameof(AccessPolicy.UpdateRecipeAccessPolicy))]
        public IActionResult Put(int id, [FromBody]UpdateRecipeDto recipe) =>
            _mapper.Map<RecipeUpdateModel>(recipe)
                   .Map(x => _commandRepository.Update(id, x))
                   .Map(x => AllOk(new { updated = x }))
                   .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.LogError(x.ToString()))
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpDelete("{id}")]
        [Authorize(Policy = nameof(AccessPolicy.DeleteRecipeAccessPolicy))]
        public IActionResult Delete(int id) =>
            _commandRepository.Delete(id)
                              .Map(_ => (IActionResult)NoContent())
                              .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.LogError(x.ToString()))
                              .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("Query")]
        [Authorize(Policy = nameof(AccessPolicy.ReadRecipeAccessPolicy))]
        public IActionResult Query([FromQuery]RecipeQueryDto query) =>
            _mapper.Map<RecipeQueryModel>(query)
                   .Map(_queryRepository.Query)
                   .Map(_mapper.Map<PagingResult<RecipeDto>>)
                   .Map(AllOk)
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
