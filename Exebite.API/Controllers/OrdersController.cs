using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.API.Models;
using Exebite.Business;
using Exebite.Model;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMenuService _menuService;
        private readonly IOrderService _orderService;
        private readonly IFoodService _foodService;

        public OrdersController(ICustomerService customerService, IMenuService menuService, IOrderService orderService, IFoodService foodService)
        {
            _customerService = customerService;
            _menuService = menuService;
            _orderService = orderService;
            _foodService = foodService;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var model = new OrdersViewModel
            {
                Customer = _customerService.GetCustomerByIdentityId(User.Identity.Name),
                ListOfRestaurants = _menuService.GetRestorantsWithMenus(),
                TodayFoods = new List<Food>()
            };

            if (model.Customer == null)
            {
                return NotFound();
            }

            model.ListOfOrders = _orderService.GetAllOrdersForCustomer(model.Customer.Id).Where(o => o.Date == DateTime.Today).ToList();
            model.CurentOrder = new Order { Customer = model.Customer, Meal = new Meal { Foods = new List<Food>() } };
            foreach (var restaurant in model.ListOfRestaurants)
            {
                model.TodayFoods.AddRange(restaurant.DailyMenu);
            }

            return Ok(model);
        }

        public IActionResult OrdersHistory()
        {
            OrdersHistoryViewModel model = new OrdersHistoryViewModel
            {
                Customer = _customerService.GetCustomerByIdentityId(User.Identity.Name)
            };

            if (model.Customer == null)
            {
                return NotFound();
            }

            model.Customer.Orders = model.Customer.Orders.OrderBy(o => o.Date).Reverse().ToList();

            return Ok(model);
        }


        public IActionResult PlaceOrder(string[] inputId, string note)
        {
            if (inputId.Count() == 0)
            {
                return BadRequest();
            }

            Order newOrder = new Order
            {
                Customer = _customerService.GetCustomerByIdentityId(User.Identity.Name),
                Date = DateTime.Today,
                Note = note,
                Meal = new Meal { Foods = new List<Food>() }
            };

            if (newOrder.Customer == null)
            {
                return NotFound();
            }

            foreach (var id in inputId)
            {
                newOrder.Meal.Foods.Add(_foodService.GetFoodById(int.Parse(id)));
            }

            newOrder.Meal.Price = newOrder.Meal.Foods.Sum(f => f.Price);
            newOrder.Price = newOrder.Meal.Price;
            _orderService.PlaceOreder(newOrder);

            return NoContent();
        }

        public IActionResult CancelOrder(int orderId)
        {
            // int i = int.Parse(orderId);
            _orderService.CancelOrder(orderId);
            return Ok();
        }
    }
}