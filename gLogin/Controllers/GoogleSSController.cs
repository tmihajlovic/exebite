using Exebite.Business;
using Exebite.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace gLogin.Controllers
{
    public class GoogleSSController : Controller
    {
        // IGoogleSpreadsheetServiceFactory service;
        IMenuService _menuService;
        public GoogleSSController(IMenuService menuService)
        {
            _menuService = menuService;
        }


        // GET: GoogleSS
        public ActionResult GoogleSS()
        {
            List<Restaurant> restaurantList = new List<Restaurant>();
            restaurantList = _menuService.GetRestorantsWithMenus();


            return View(restaurantList);
        }

        ////GET: Place order
        //public ActionResult PlaceOrder(string cell)
        //{
        //    var currentLoggedUser = User.Identity.Name;

        //    var users = service.GetUsers();

        //    var user = users.FirstOrDefault(u => u.Value == currentLoggedUser);

        //    service.PlaceOrder(user.Key.ToString(), cell);

        //    return RedirectToAction("GoogleSS");
        //}
        ////GET: Cancel order
        //public ActionResult CancelOrder(string cell)
        //{
        //    var currentLoggedUser = User.Identity.Name;
        //    var users = service.GetUsers();

        //    var user = users.FirstOrDefault(u => u.Value == currentLoggedUser);
        //    service.CancelOrder(user.Key.ToString(), cell);

        //    return RedirectToAction("GoogleSS");
        //}
    }

}