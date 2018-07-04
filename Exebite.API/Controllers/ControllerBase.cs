using System;
using Exebite.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    public class ControllerBase : Controller
    {
        [Obsolete("Security concern, can return data which can be misused by malicious user")]
        protected IActionResult InternalServerError(Error error) =>
                StatusCode(StatusCodes.Status500InternalServerError, error);

        protected IActionResult InternalServerError(string error) =>
                StatusCode(StatusCodes.Status500InternalServerError, error);

        protected IActionResult InternalServerError() =>
                StatusCode(StatusCodes.Status500InternalServerError);

        protected IActionResult Created<T>(T content) =>
          StatusCode(StatusCodes.Status201Created, content);

        protected IActionResult AllOk<T>(T content) =>
            StatusCode(StatusCodes.Status200OK, content);
    }
}
