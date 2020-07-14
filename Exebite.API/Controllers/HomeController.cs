using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly ICustomerCommandRepository _commandRepo;
        private readonly ICustomerQueryRepository _queryRepo;
        private readonly IEitherMapper _mapper;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(
            ICustomerCommandRepository commandRepo,
            ICustomerQueryRepository queryRepo,
            IEitherMapper mapper,
            ILogger<HomeController> logger,
            IConfiguration configuration)
        {
            _commandRepo = commandRepo;
            _queryRepo = queryRepo;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("signin")]
        [Authorize]
        public IActionResult GoogleSignIn()
        {
            string googleId = User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
            return _queryRepo.Query(new CustomerQueryModel { GoogleUserId = googleId })
                   .Map(x => SignInUser(x, googleId))
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
        }

        [AllowAnonymous]
        [HttpPost("api/google-login")]
        public async Task<IActionResult> GoogleAuthenticateAsync([FromBody] GoogleUserRequest request) =>
            (await AuthenticateGoogleUserAsync(request.IdToken).ConfigureAwait(false))
                .Map(x => GenerateUserToken(x))
                .Map(x => AllOk(x))
                .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        private IActionResult SignInUser(PagingResult<DomainModel.Customer> customers, string googleId)
        {
            if (!customers.Items.Any())
            {
                return _commandRepo.Insert(
                      new CustomerInsertModel
                      {
                          Balance = 0,
                          GoogleUserId = googleId,
                          Name = User.Claims.FirstOrDefault(x => x.Type.EndsWith("name"))?.Value
                      })
                      .Map(x => AllOk(new { id = x }))
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
            }

            return AllOk(new { id = customers.Items.First().Id });
        }

        public class GoogleUserRequest
        {
            public string IdToken { get; set; }
        }

        private class AppUser : IdentityUser
        {
            public int Role { get; set; }
        }

        private class UserToken
        {
            public string Token { get; set; }
        }

        private async Task<Either<Error, AppUser>> AuthenticateGoogleUserAsync(string idToken)
        {
            Payload payload = await ValidateAsync(idToken, new ValidationSettings { Audience = new[] { _configuration["Authentication:Exebite.ClientApp:ClientId"] } }).ConfigureAwait(false);

            return GetOrCreateExternalLoginUser(payload.Email);
        }

        private Either<Error, AppUser> GetOrCreateExternalLoginUser(string email)
        {
            return _queryRepo.Query(new CustomerQueryModel { GoogleUserId = email })
                   .Map(customers =>
                   {
                       if (!customers.Items.Any())
                       {
                           var name = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value + User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
                           var role = (int)RoleType.User;
                           return _commandRepo.Insert(
                                 new CustomerInsertModel
                                 {
                                     Balance = 0,
                                     GoogleUserId = email,
                                     Name = name,
                                     Role = role
                                 })
                                 .Map(_ => new AppUser { Id = email, Role = role });
                       }
                       return new AppUser { Id = email, Role = customers.Items.First().Role };
                   });
        }

        private UserToken GenerateUserToken(AppUser user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Authentication:Exebite.API:ClientSecret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id), // sub claim
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Authentication:Exebite.API:ClientId"],
                Audience = _configuration["Authentication:Exebite.ClientApp:ClientId"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);

            return new UserToken { Token = token };
        }
    }
}