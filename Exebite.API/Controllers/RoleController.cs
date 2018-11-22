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
    [Route("api/Role")]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleCommandRepository _commandRepository;
        private readonly IRoleQueryRepository _queryRepository;
        private readonly IEitherMapper _mapper;
        private readonly ILogger<RoleController> _logger;

        public RoleController(
            IRoleCommandRepository commandRepository,
            IRoleQueryRepository queryRepository,
            IEitherMapper mapper,
            ILogger<RoleController> logger)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Policy = nameof(AccessPolicy.CreateRoleAccessPolicy))]
        public IActionResult Post([FromBody]CreateRoleDto model) =>
            _mapper.Map<RoleInsertModel>(model)
                   .Map(_commandRepository.Insert)
                   .Map(x => AllOk(new { id = x }))
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpPut("{id}")]
        [Authorize(Policy = nameof(AccessPolicy.UpdateRoleAccessPolicy))]
        public IActionResult Put(int id, [FromBody]UpdateRoleDto model) =>
            _mapper.Map<RoleUpdateModel>(model)
                   .Map(x => _commandRepository.Update(id, x))
                   .Map(x => AllOk(new { updated = x }))
                   .Reduce(_ => NotFound(), error => error is RecordNotFound)
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpDelete("{id}")]
        [Authorize(Policy = nameof(AccessPolicy.DeleteRoleAccessPolicy))]
        public IActionResult Delete(int id) =>
            _commandRepository.Delete(id)
                              .Map(_ => OkNoContent())
                              .Reduce(_ => NotFound(), error => error is RecordNotFound)
                              .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("Query")]
        [Authorize(Policy = nameof(AccessPolicy.ReadRoleAccessPolicy))]
        public IActionResult Query(RoleQueryDto query) =>
            _mapper.Map<RoleQueryModel>(query)
                   .Map(_queryRepository.Query)
                   .Map(_mapper.Map<PagingResult<RoleDto>>)
                   .Map(AllOk)
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}