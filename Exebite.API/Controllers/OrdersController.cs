﻿using System;
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
        public IActionResult GetOrders()
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

        [HttpGet("id")]
        public IActionResult GetOrder(int id)
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

            var order = _orderService.GetOrderById(id);
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

        [HttpGet("OrdersHistory")]
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

            model.Customer.Orders = model.Customer.Orders.OrderByDescending(o => o.Date).ToList();

            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderModel model)
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
        public IActionResult UpdateOrder([FromBody] UpdateOrderModel model)
        {
            if (model.FoodIds == null || model.FoodIds.Count() == 0)
            {
                return BadRequest();
            }

            Order currentOrder = _orderService.GetOrderById(model.Id);
            currentOrder.Meal.Foods.Clear();
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
            var updatedOrder = _orderService.UpdateOrder(currentOrder);

            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderService.DeleteOrder(id);
            return NoContent();
        }

    }
}