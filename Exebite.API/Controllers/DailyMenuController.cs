using System.Collections.Generic;
using AutoMapper;
using Either;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

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
        private readonly ILogger _logger;

        public DailyMenuController(
            IDailyMenuQueryRepository queryRepo,
            IDailyMenuCommandRepository commandRepo,
            IMapper mapper,
            ILogger logger)
        {
            _mapper = mapper;
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(int page, int size) =>
            _queryRepo.Query(new DailyMenuQueryModel(page, size))
                      .Map(x => (IActionResult)Ok(_mapper.Map<IEnumerable<DailyMenuDto>>(x.Items)))
                      .Reduce(_ => InternalServerError(), x => _logger.Error(x));

        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
            _queryRepo.Query(new DailyMenuQueryModel { Id = id })
                      .Map(x => AllOk(_mapper.Map<IEnumerable<DailyMenuDto>>(x.Items)))
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.Error(x))
                      .Reduce(_ => InternalServerError(), x => _logger.Error(x));

        [HttpPost]
        public IActionResult Post([FromBody]CreateDailyMenuDto model) =>
            _commandRepo.Insert(_mapper.Map<DailyMenuInsertModel>(model))
                        .Map(x => Created(new { id = x }))
                        .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                        .Reduce(_ => InternalServerError(), x => _logger.Error(x));

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateDailyMenuDto model) =>
            _commandRepo.Update(id, _mapper.Map<DailyMenuUpdateModel>(model))
                        .Map(x => AllOk(new { updated = x }))
                        .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.Error(x))
                        .Reduce(_ => InternalServerError(), x => _logger.Error(x));

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            _commandRepo.Delete(id)
                        .Map(_ => (IActionResult)NoContent())
                        .Reduce(_ => NotFound(), error => error is RecordNotFound, x => _logger.Error(x))
                        .Reduce(_ => InternalServerError(), x => _logger.Error(x));

        [HttpGet("Query")]
        public IActionResult Query([FromQuery]DailyMenuQueryDto query) =>
            _queryRepo.Query(_mapper.Map<DailyMenuQueryModel>(query))
                      .Map(_mapper.Map<PagingResult<DailyMenuDto>>)
                      .Map(AllOk)
                      .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.Error(x))
                      .Reduce(_ => InternalServerError(), x => _logger.Error(x));
    }
}
