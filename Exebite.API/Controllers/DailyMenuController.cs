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
    [Route("api/dailymenu")]
    [Authorize]
    public class DailyMenuController : ControllerBase
    {
        private readonly IDailyMenuQueryRepository _queryRepo;
        private readonly IDailyMenuCommandRepository _commandRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<DailyMenuController> _logger;

        public DailyMenuController(
            IDailyMenuQueryRepository queryRepo,
            IDailyMenuCommandRepository commandRepo,
            IMapper mapper,
            ILogger<DailyMenuController> logger)
        {
            _mapper = mapper;
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateDailyMenuDto model) =>
            _commandRepo.Insert(_mapper.Map<DailyMenuInsertModel>(model))
                        .Map(x => Created(new { id = x }))
                        .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateDailyMenuDto model) =>
            _commandRepo.Update(id, _mapper.Map<DailyMenuUpdateModel>(model))
                        .Map(x => AllOk(new { updated = x }))
                        .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.LogError(x.ToString()))
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepo.Delete(id)
                        .Map(_ => OkNoContent())
                        .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.LogError(x.ToString()))
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("Query")]
        public IActionResult Query([FromQuery]DailyMenuQueryDto query) =>
            _queryRepo.Query(_mapper.Map<DailyMenuQueryModel>(query))
                      .Map(x => AllOk(_mapper.Map<PagingResult<DailyMenuDto>>(x)))
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}
