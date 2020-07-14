using Exebite.DomainModel;
using Microsoft.AspNetCore.Identity;

namespace Exebite.API.Authorization
{
    public class User : IdentityUser
    {
        public RoleType Role { get; set; }
    }
}
