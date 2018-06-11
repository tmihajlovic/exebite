using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Migrations;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class OrderServiceTest
    {
        private static IOrderService _orderService;
        private static IFoodOrderingContextFactory _factory;
        private static IMapper _mapper;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            var container = ServiceProviderWrapper.GetContainer();
            _factory = container.Resolve<IFoodOrderingContextFactory>();
            InMemoryDBSeed.Seed(_factory);
            _mapper = container.Resolve<IMapper>();
            _orderService = container.Resolve<IOrderService>();
        }

        [TestMethod]
        public void GetAllOrders_ObjectIsNotNull()
        {
            var result = _orderService.GetAllOrders();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetOrderById_IdValid_ObjectReturned()
        {
            var id = 1;
            var customerId = 1;
            var result = _orderService.GetOrderByIdForCustomer(id, customerId);
            Assert.AreEqual(result.Id, id);
        }

        [TestMethod]
        public void GetOrderById_IdNonExisting_NullIsReturned()
        {
            var id = 0;
            var customerId = 1;
            var result = _orderService.GetOrderByIdForCustomer(id, customerId);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllOrdersForCustomer_ExistingCustomer_ConutOfOrdersAreNotZero()
        {
            var customerId = 1;
            var orders = _orderService.GetAllOrdersForCustomer(customerId);
            Assert.AreNotSame(orders.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAllOrdersForCustomer_NonExistingCustomer_ArgumentExceptionThrown()
        {
            _orderService.GetAllOrdersForCustomer(0);
        }

        [TestMethod]
        public void GetAllOrdersForRestoraunt_RestorauntExists_OrdersCountIsNotZero()
        {
            var restaurantId = 1;
            var orders = _orderService.GetAllOrdersForRestoraunt(restaurantId);
            Assert.AreNotEqual(orders.Count, 0);
        }

        [TestMethod]
        public void GetAllOrdersForRestoraunt_NonExistingRestaurant_OrdersIsEmpty()
        {
            var restaurantId = 0;
            var orders = _orderService.GetAllOrdersForRestoraunt(restaurantId);
            Assert.AreEqual(orders.Count, 0);
        }

        [TestMethod]
        public void GetOrdersForDate_DateIsToday_ReturnedOnlyOrdersForPassedDate()
        {
            var date = DateTime.Today;
            var orders = _orderService.GetOrdersForDate(date);
            var result = orders.Where(o => o.Date != date).ToList();
            Assert.AreNotEqual(orders.Count, 0);
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetOrdersForDate_DateIsTomorow_ArgumentExceptionThrown()
        {
            var date = DateTime.Today.AddDays(1);
            _orderService.GetOrdersForDate(date);
        }

        [TestMethod]
        public void CreateOreder_ValidOrderPassed_OrderCreated()
        {
            using (var context = _factory.Create())
            {
                var food1 = _mapper.Map<Food>(context.Foods.First());
                var customer = _mapper.Map<Customer>(context.Customers.First());
                var order = new Order
                {
                    Meal = new Meal
                    {
                        Foods = new List<Food> { food1 }
                    },
                    Note = "New note",
                    Date = DateTime.Now,
                    Customer = customer
                };
                var result = _orderService.CreateOrder(order);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOrder_IsNull_ArgumentNullExceptionThrown()
        {
            _orderService.CreateOrder(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateOrder_IsNull_ArgumentNullExceptionThrown()
        {
            _orderService.UpdateOrder(null);
        }

        [TestMethod]
        public void UpdateOrder_ValidFoodAdded_OrderUpdated()
        {
            using (var context = _factory.Create())
            {
                var customerId = 1;
                var id = 1;
                var foodToAdd = _mapper.Map<Food>(context.Foods.Where(f => f.Type == FoodType.SIDE_DISH).First());
                var order = _orderService.GetOrderByIdForCustomer(id, customerId);
                order.Meal.Foods.Add(foodToAdd);
                var result = _orderService.UpdateOrder(order);
                Assert.AreEqual(result.Meal.Foods.Count, 2);
            }
        }

        [TestMethod]
        public void DeleteOrder_ExistingOrderPassed_OrderDeleted()
        {
            var customerId = 1;
            var orderNote = "For delete";
            var order = _orderService.GetAllOrders().FirstOrDefault(o => o.Note == orderNote);
            _orderService.DeleteOrder(order.Id);
            var result = _orderService.GetOrderByIdForCustomer(order.Id, customerId);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteOrder_NonExistingOrderPassed_NoException()
        {
            _orderService.DeleteOrder(0);
        }
    }
}
