using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}