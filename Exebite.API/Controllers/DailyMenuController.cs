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
    [Route("api/dailymenu")]
    //[Authorize]
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

        [HttpPost]
        //[Authorize(Policy = nameof(AccessPolicy.CreateDailyMenuAccessPolicy))]
        public IActionResult Post([FromBody]CreateDailyMenuDto model) =>
            _mapper.Map<DailyMenuInsertModel>(model)
                        .Map(_commandRepo.Insert)
                        .Map(x => Created(new { id = x }))
                        .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        //[Authorize(Policy = nameof(AccessPolicy.UpdateDailyMenuAccessPolicy))]
        public IActionResult Put(int id, [FromBody]UpdateDailyMenuDto model) =>
            _mapper.Map<DailyMenuUpdateModel>(model)
                        .Map(x => _commandRepo.Update(id, x))
                        .Map(x => AllOk(new { updated = x }))
                        .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.LogError(x.ToString()))
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpDelete("{id}")]
        //[Authorize(Policy = nameof(AccessPolicy.DeleteDailyMenuAccessPolicy))]
        public IActionResult Delete(int id) =>
            _commandRepo.Delete(id)
                        .Map(_ => OkNoContent())
                        .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.LogError(x.ToString()))
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("Query")]
        //[Authorize(Policy = nameof(AccessPolicy.ReadDailyMenuAccessPolicy))]
        public IActionResult Query([FromQuery]DailyMenuQueryDto query) =>
            _mapper.Map<DailyMenuQueryModel>(query)
                      .Map(_queryRepo.Query)
                      .Map(_mapper.Map<PagingResult<DailyMenuDto>>)
                      .Map(AllOk)
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
