using Exebite.Business;
using Execom.ClientDemo.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Execom.ClientDemo.Controllers
{
    public class HomeController : Controller
    {
        ICustomerService _customerService;
        IMenuService _menuService;
        IOrderService _orderService;

        public HomeController(ICustomerService customerService, IMenuService menuService, IOrderService orderService)
        {
            _customerService = customerService;
            _menuService = menuService;
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.Customer =  _customerService.GetCustomerByIdentityId(User.Identity.GetUserId());
            model.ListOfRestaurants = _menuService.GetRestorantsWithMenus();
            model.ListOfOrders = _orderService.GetAllOrdersForCustomer(model.Customer.Id).Where(o => o.Date == DateTime.Today).ToList();
            model.TodayFoods = new List<Exebite.Model.Food>();
            foreach(var restaurant in model.ListOfRestaurants)
            {
                model.TodayFoods.AddRange(restaurant.DailyMenu);
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}