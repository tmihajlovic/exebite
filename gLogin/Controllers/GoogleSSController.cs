using Exebite.Business;
using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

namespace gLogin.Controllers
{
    public class GoogleSSController : Controller
    {
        // IGoogleSpreadsheetServiceFactory service;
        IMenuService _menuService;
        IOrderService _orderService;
        ICustomerService _customerService;
        IRestarauntService _restarauntService;
        IGoogleDataExporter _googleDataExporter;
        public GoogleSSController(IMenuService menuService, IOrderService orderService, ICustomerService customerService, IRestarauntService restarauntService, IGoogleDataExporter googleDataExporter)
        {
            _menuService = menuService;
            _orderService = orderService;
            _customerService = customerService;
            _restarauntService = restarauntService;
            _googleDataExporter = googleDataExporter;
        }


        // GET: GoogleSS
        public ActionResult GoogleSS()
        {
            List<Restaurant> restaurantList = new List<Restaurant>();
            //restaurantList = _menuService.GetRestorantsWithMenus();
            restaurantList.Add(_restarauntService.GetRestaurantById(1));
            restaurantList.Add(_restarauntService.GetRestaurantById(2));
            restaurantList.Add(_restarauntService.GetRestaurantById(4));

            return View(restaurantList);
        }

        //GET: Place order
        //TODO get logged user, dont use random
        public ActionResult PlaceOrder(Food food)
        {
            Order newOrder = new Order();
            Customer customer = new Customer();
            Random rnd = new Random();
            customer = _customerService.GetCustomerById(rnd.Next(1, 200));
            newOrder.Customer = customer;
            newOrder.Date = DateTime.Today;
            newOrder.Meal = new Meal {
                 Foods = new List<Food> { food },
                 Price = food.Price
            };
            newOrder.Price = food.Price;

            _orderService.PlaceOreder(newOrder);
            return RedirectToAction("GoogleSS");
        }
        ////GET: Cancel order
        //public ActionResult CancelOrder(string cell)
        //{
        //    var currentLoggedUser = User.Identity.Name;
        //    var users = service.GetUsers();

        //    var user = users.FirstOrDefault(u => u.Value == currentLoggedUser);
        //    service.CancelOrder(user.Key.ToString(), cell);

        //    return RedirectToAction("GoogleSS");
        //}
        public ActionResult WriteToSheets()
        {
            var todayOrders = _orderService.GettOrdersForDate(DateTime.Today);

            _googleDataExporter.PlaceOrders(todayOrders);

            return RedirectToAction("GoogleSS");
        }
    }

}