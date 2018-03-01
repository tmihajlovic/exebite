using Exebite.Business;
using Exebite.Model;
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
        IFoodService _foodService;

        public HomeController(ICustomerService customerService, IMenuService menuService, IOrderService orderService, IFoodService foodService)
        {
            _customerService = customerService;
            _menuService = menuService;
            _orderService = orderService;
            _foodService = foodService;
        }

        public ActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.Customer =  _customerService.GetCustomerByIdentityId(User.Identity.GetUserId());
            model.ListOfRestaurants = _menuService.GetRestorantsWithMenus();
            model.ListOfOrders = _orderService.GetAllOrdersForCustomer(model.Customer.Id).Where(o => o.Date == DateTime.Today).ToList();
            model.TodayFoods = new List<Food>();
            model.CurentOrder = new Order {  Customer =model.Customer, Meal = new Meal { Foods= new List<Food>() } };
            foreach(var restaurant in model.ListOfRestaurants)
            {
                model.TodayFoods.AddRange(restaurant.DailyMenu);
            }
            return View(model);
        }

        public ActionResult IstorijaNarudzbina()
        {
            HomeIstorijaNarudzbinaViewModel model = new HomeIstorijaNarudzbinaViewModel();
            model.Customer = _customerService.GetCustomerByIdentityId(User.Identity.GetUserId());
            model.Customer.Orders = model.Customer.Orders.OrderBy(o => o.Date).Reverse().ToList();
            return View(model);
        }
        

        public ActionResult PlaceOrder(string[] inputId, string note)
        {

            if (inputId.Count() > 0)
            {
                Order newOrder = new Order();
                newOrder.Customer = _customerService.GetCustomerByIdentityId(User.Identity.GetUserId());
                newOrder.Date = DateTime.Today;
                newOrder.Note = note;
                newOrder.Meal = new Meal();
                newOrder.Meal.Foods = new List<Food>();
                foreach (var id in inputId)
                {
                    newOrder.Meal.Foods.Add(_foodService.GetFoodById(int.Parse(id)));
                }
                newOrder.Meal.Price = newOrder.Meal.Foods.Sum(f => f.Price);
                newOrder.Price = newOrder.Meal.Price;
                _orderService.PlaceOreder(newOrder);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CancelOrder(int orderId)
        {
           // int i = int.Parse(orderId);
             _orderService.CancelOrder(orderId);
            return RedirectToAction("Index", "Home");
        }
    }
}