using Exebite.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    public class ControllerBase : Controller
    {
        protected IActionResult InternalServerError(Error error) =>
                StatusCode(StatusCodes.Status500InternalServerError, error);
    }
}
