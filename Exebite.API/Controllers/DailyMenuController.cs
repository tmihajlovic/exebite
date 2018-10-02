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
    [Route("api/dailymenu")]
    public class DailyMenuController : ControllerBase
    {
        private readonly IDailyMenuQueryRepository _queryRepo;
        private readonly IDailyMenuCommandRepository _commandRepo;
        private readonly IEitherMapper _mapper;
        private readonly ILogger<DailyMenuController> _logger;

        public DailyMenuController(
            IDailyMenuQueryRepository queryRepo,
            IDailyMenuCommandRepository commandRepo,
            IEitherMapper mapper,
            ILogger<DailyMenuController> logger)
        {
            _mapper = mapper;
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _logger = logger;
        }

        // [Authorize(Policy = nameof(AccessPolicy.CreateDailyMenuAccessPolicy))]
        [HttpPost]
        public IActionResult Post([FromBody]CreateDailyMenuDto model) =>
            _mapper.Map<DailyMenuInsertModel>(model)
                        .Map(_commandRepo.Insert)
                        .Map(x => Created(new { id = x }))
                        .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        // [Authorize(Policy = nameof(AccessPolicy.UpdateDailyMenuAccessPolicy))]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateDailyMenuDto model) =>
            _mapper.Map<DailyMenuUpdateModel>(model)
                        .Map(x => _commandRepo.Update(id, x))
                        .Map(x => AllOk(new { updated = x }))
                        .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.LogError(x.ToString()))
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        // [Authorize(Policy = nameof(AccessPolicy.DeleteDailyMenuAccessPolicy))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepo.Delete(id)
                        .Map(_ => OkNoContent())
                        .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.LogError(x.ToString()))
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        // [Authorize(Policy = nameof(AccessPolicy.ReadDailyMenuAccessPolicy))]
        [HttpGet("Query")]
        public IActionResult Query([FromQuery]DailyMenuQueryDto query) =>
            _mapper.Map<DailyMenuQueryModel>(query)
                      .Map(_queryRepo.Query)
                      .Map(_mapper.Map<PagingResult<DailyMenuDto>>)
                      .Map(AllOk)
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
