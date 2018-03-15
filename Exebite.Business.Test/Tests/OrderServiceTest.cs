using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class OrderServiceTest
    {
        private static IOrderService _orderService;
        private static IFoodOrderingContextFactory _factory;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _orderService = new OrderService(new OrderRepository(_factory));
            InMemoryDBSeed.Seed(_factory);
        }

        [TestMethod]
        public void GetAllOrders()
        {
            var result = _orderService.GetAllOrders();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetOrderById()
        {
            var id = 1;
            var result = _orderService.GetOrderById(id);
            Assert.AreEqual(result.Id, id);
        }

        [TestMethod]
        public void GetOrderById_NonExisting()
        {
            var id = 0;
            var result = _orderService.GetOrderById(id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllOrdersForCustomer()
        {
            var customerId = 1;
            var orders = _orderService.GetAllOrdersForCustomer(customerId);
            var result = orders.Where(o => o.Customer.Id != customerId).ToList();
            Assert.AreNotSame(orders.Count, 0);
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAllOrdersForCustomer_NonExistingCustomer()
        {
            _orderService.GetAllOrdersForCustomer(0);
        }

        [TestMethod]
        public void GetAllOrdersForRestoraunt()
        {
            var restaurantId = 1;
            var orders = _orderService.GetAllOrdersForRestoraunt(restaurantId);
            var result = orders.Where(o => o.Meal.Foods.First().Restaurant.Id != restaurantId).ToList();
            Assert.AreNotEqual(orders.Count, 0);
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetAllOrdersForRestoraunt_NonExistingRestaurant()
        {
            var restaurantId = 0;
            var orders = _orderService.GetAllOrdersForRestoraunt(restaurantId);
            var result = orders.Where(o => o.Meal.Foods.First().Restaurant.Id != restaurantId).ToList();
            Assert.AreEqual(orders.Count, 0);
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetOrdersForDate()
        {
            var date = DateTime.Today;
            var orders = _orderService.GetOrdersForDate(date);
            var result = orders.Where(o => o.Date != date).ToList();
            Assert.AreNotEqual(orders.Count, 0);
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetOrdersForDate_DateIsTomorow()
        {
            var date = DateTime.Today.AddDays(1);
            _orderService.GetOrdersForDate(date);
        }

        [TestMethod]
        public void PlaceOreder()
        {
            using (var context = _factory.Create())
            {
                var food1 = AutoMapperHelper.Instance.GetMappedValue<Food>(context.Foods.First(), context);
                var customer = AutoMapperHelper.Instance.GetMappedValue<Customer>(context.Customers.First(), context);
                var order = new Order();
                order.Meal = new Meal();
                order.Meal.Foods = new List<Food>();
                order.Note = "New note";
                order.Meal.Foods.Add(food1);
                order.Date = DateTime.Now;
                order.Customer = customer;
                var result = _orderService.PlaceOreder(order);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlaceOrder_IsNull()
        {
            _orderService.PlaceOreder(null);
        }

        [TestMethod]
        public void EditOrder()
        {
            using (var context = _factory.Create())
            {
                var id = 1;
                var foodToAdd = AutoMapperHelper.Instance.GetMappedValue<Food>(context.Foods.Where(f => f.Type == FoodType.SIDE_DISH).First(), context);
                var order = _orderService.GetOrderById(id);
                order.Meal.Foods.Add(foodToAdd);
                var result = _orderService.EditOrder(order);
                Assert.AreEqual(result.Meal.Foods.Count, 2);
            }
        }

        [TestMethod]
        public void CancelOrder()
        {
            var orderNote = "For delete";
            var order = _orderService.GetAllOrders().FirstOrDefault(o => o.Note == orderNote);
            _orderService.CancelOrder(order.Id);
            var result = _orderService.GetOrderById(order.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CancelOrder_NonExisting()
        {
            _orderService.CancelOrder(0);
        }
    }
}
