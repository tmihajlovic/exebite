using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Route("[controller]")]
    [Route("health")]
    [ApiController]
    public class HealthcheckController : ControllerBase
    {
        // GET: api/Heartbeat
        [HttpGet]
        public string Get()
        {
            return "Server Working";
        }
    }
}
