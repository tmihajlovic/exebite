using Exebite.Business;
using Exebite.Business.GoogleApiImportExport;
using Exebite.Model;
using System;
using System.Collections.Generic;
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
        IGoogleDataImporter _googleDataImporter;

        public GoogleSSController(IMenuService menuService, IOrderService orderService, ICustomerService customerService, IRestarauntService restarauntService,
            IGoogleDataExporter googleDataExporter, IGoogleDataImporter googleDataImporter)
        {
            _menuService = menuService;
            _orderService = orderService;
            _customerService = customerService;
            _restarauntService = restarauntService;
            _googleDataExporter = googleDataExporter;
            _googleDataImporter = googleDataImporter;
        }


        // GET: GoogleSS
        public ActionResult GoogleSS()
        {
            //TODO: Switch to menuService call afer all restaurans are implemented
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
            customer = _customerService.GetCustomerById(rnd.Next(1, 261));
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
        /// <summary>
        /// Loads food and daily menu to database
        /// </summary>
        /// <returns>Redirect to googleSS</returns>
        public ActionResult LoadFoodToDB()
        {
            _googleDataImporter.UpdateRestorauntsMenu();
            return RedirectToAction("GoogleSS");
        }

        /// <summary>
        /// Sets daily menu tab so today is in first column, and fix dates
        /// </summary>
        /// <returns></returns>
        public ActionResult SetupDailyMenuDayOrder()
        {
            _googleDataExporter.SetupDailyMenuDayOrder();
            return RedirectToAction("GoogleSS");
        }

        /// <summary>
        /// Update kasa tab with new info
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateKasa()
        {
            _googleDataExporter.UpdateKasaTab();

            return RedirectToAction("GoogleSS");
        }

        public ActionResult ImportUsers()
        {

            _googleDataImporter.ImportUsersFromKasa();
            return RedirectToAction("GoogleSS");
        }
    }

}