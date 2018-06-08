using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.API.Models;
using Exebite.Business;
using Exebite.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/orders")]
    [Authorize]
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

        //todo: check if here should be get for all orders by date for all users


        [HttpGet]
        public IActionResult Get()
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

            // todo: where is not used good, in method GetAllOrdersForCustomer, we already do ToList()
            model.ListOfOrders = _orderService.GetAllOrdersForCustomer(model.Customer.Id).Where(o => o.Date == DateTime.Today).ToList();
            model.CurentOrder = new Order { Customer = model.Customer, Meal = new Meal { Foods = new List<Food>() } };
            foreach (var restaurant in model.ListOfRestaurants)
            {
                model.TodayFoods.AddRange(restaurant.DailyMenu);
            }

            return Ok(model);
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
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

            var order = _orderService.GetOrderByIdForCustomer(id, model.Customer.Id);
            if (order == null)
            {
                return NotFound();
            }

            model.ListOfOrders = new List<Order> { order };
            model.CurentOrder = new Order { Customer = model.Customer, Meal = new Meal { Foods = new List<Food>() } };
            foreach (var restaurant in model.ListOfRestaurants)
            {
                model.TodayFoods.AddRange(restaurant.DailyMenu);
            }

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderModel model)
        {
            if (model.FoodIds == null || model.FoodIds.Count() == 0)
            {
                return BadRequest();
            }

            Order newOrder = new Order
            {
                Customer = _customerService.GetCustomerByIdentityId(User.Identity.Name),
                Date = DateTime.Today,
                Note = model.Note,
                Meal = new Meal { Foods = new List<Food>() }
            };

            if (newOrder.Customer == null)
            {
                return NotFound();
            }

            foreach (var id in model.FoodIds)
            {
                newOrder.Meal.Foods.Add(_foodService.GetFoodById(id));
            }

            newOrder.Meal.Price = newOrder.Meal.Foods.Sum(f => f.Price);
            newOrder.Price = newOrder.Meal.Price;
            var createdOrder = _orderService.CreateOrder(newOrder);

            return Ok(createdOrder.Id);
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateOrderModel model) //todo: after Mladen check in, check if Id is in model or not and should it be there or not?
        {
            // todo: check if we should support note update, because we should support whole object update on http put
            if (model.FoodIds == null || model.FoodIds.Count() == 0)
            {
                return BadRequest();
            }

            var currentCustomer = _customerService.GetCustomerByIdentityId(User.Identity.Name);
            if (currentCustomer == null)
            {
                return NotFound();
            }

            Order currentOrder = _orderService.GetOrderByIdForCustomer(model.Id, currentCustomer.Id);
            if (currentOrder == null)
            {
                return NotFound();
            }

            currentOrder.Meal.Foods = new List<Food>();
            foreach (var id in model.FoodIds)
            {
                currentOrder.Meal.Foods.Add(_foodService.GetFoodById(id));
            }

            currentOrder.Meal.Price = currentOrder.Meal.Foods.Sum(f => f.Price);
            currentOrder.Price = currentOrder.Meal.Price;
            currentOrder.Note = model.Note;
            var updatedOrder = _orderService.UpdateOrder(currentOrder);

            return Ok(updatedOrder.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderService.DeleteOrder(id);
            return NoContent();
        }

    }
}