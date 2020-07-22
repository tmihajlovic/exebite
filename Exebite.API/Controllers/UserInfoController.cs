using Either;
using Exebite.API.Authorization;
using Exebite.DataAccess.Repositories;
using Exebite.DtoModels.UserInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/userinfo")]
    public class UserInfoController : ControllerBase
    {
        private readonly ICustomerQueryRepository _customerQueryRepo;
        private readonly ILogger<UserInfoController> _logger;

        public UserInfoController(
            ICustomerQueryRepository customerQueryRepo,
            ILogger<UserInfoController> logger)
        {
            _customerQueryRepo = customerQueryRepo;
            _logger = logger;
        }

        [HttpGet("{googleId}")]
        [Authorize(Policy = nameof(AccessPolicy.ReadUserInfoAccessPolicy))]
        public IActionResult Get(string googleId)
        {
            return _customerQueryRepo.GetRole(googleId)
                .Map(role => new UserInfoDto { Role = role })
                .Map(AllOk)
                .Reduce(_ => NotFound(), error => error is RecordNotFound)
                .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
        }
    }
}
