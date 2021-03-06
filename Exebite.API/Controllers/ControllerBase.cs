﻿using Exebite.API.Controllers.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [ValidateModel]
    public class ControllerBase : Controller
    {
        protected IActionResult InternalServerError(string error) =>
                StatusCode(StatusCodes.Status500InternalServerError, error);

        protected IActionResult InternalServerError() =>
                StatusCode(StatusCodes.Status500InternalServerError);

        protected IActionResult Created<T>(T content) =>
          StatusCode(StatusCodes.Status201Created, content);

        protected IActionResult AllOk<T>(T content) =>
            StatusCode(StatusCodes.Status200OK, content);

        protected IActionResult OkNoContent() =>
            StatusCode(StatusCodes.Status204NoContent);
    }
}
