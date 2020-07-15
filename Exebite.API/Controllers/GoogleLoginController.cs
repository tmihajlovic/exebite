using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Either;
using Exebite.API.Authorization;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.DtoModels.GoogleLogin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/googlelogin")]
    public class GoogleLoginController : ControllerBase
    {
        private readonly ILogger<GoogleLoginController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICustomerCommandRepository _customerCommandRepo;
        private readonly ICustomerQueryRepository _customerQueryRepo;

        public GoogleLoginController(
            ILogger<GoogleLoginController> logger,
            IConfiguration configuration,
            ICustomerCommandRepository customerCommandRepo,
            ICustomerQueryRepository customerQueryRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _customerCommandRepo = customerCommandRepo;
            _customerQueryRepo = customerQueryRepo;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromBody] GoogleLoginDto request) =>
            (await ValidateGoogleUserAsync(request.IdToken).ConfigureAwait(false))
                .Map(payload => CreateCustomer(payload))
                .Map(customer => CreateUser(customer))
                .Map(user => CreateUserToken(user))
                .Map(userToken => AllOk(userToken))
                .Reduce(_ => InternalServerError(), error => _logger.LogError(error.ToString()));

        private async Task<Either<Error, Payload>> ValidateGoogleUserAsync(string idToken)
        {
            var validationSettings = new ValidationSettings
            {
                Audience = new[] { _configuration["Authentication:Exebite.ClientApp:ClientId"] }
            };
            Payload payload = await ValidateAsync(idToken, validationSettings).ConfigureAwait(false);
            return new Right<Error, Payload>(payload);
        }

        private Either<Error, Customer> CreateCustomer(Payload payload) =>
            _customerQueryRepo.Query(new CustomerQueryModel { GoogleUserId = payload.Email })
                .Map(customers => customers.Items.FirstOrDefault())
                .Map(customer =>
                {
                    if (customer == null)
                    {
                        var name = payload.GivenName + " " + payload.FamilyName;
                        var role = RoleType.User;
                        _customerCommandRepo.Insert(
                            new CustomerInsertModel
                            {
                                Balance = 0,
                                GoogleUserId = payload.Email,
                                Name = name,
                                Role = (int)role,
                                DefaultLocationId = 1,
                                IsActive = true
                            });
                        return new Customer
                        {
                            GoogleUserId = payload.Email,
                            Role = (int)role
                        };
                    }
                    return customer;
                });

        private User CreateUser(Customer customer) =>
            new User
            {
                Id = customer.GoogleUserId,
                Role = (RoleType)customer.Role
            };

        private UserToken CreateUserToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Authentication:Exebite.API:ClientSecret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
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

            return new UserToken { AccessToken = token };
        }
    }
}
