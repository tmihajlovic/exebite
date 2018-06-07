using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.InMemoryDB;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Resolution;

namespace Exebite.DataAccess.Test.Tests
{
    [TestClass]
    public class OrderRepositoryTest
    {
        private static IFoodOrderingContextFactory _factory;
        private static IOrderRepository _orderRepository;
        private static IUnityContainer _container;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _container = new UnityContainer();
           // Unity.UnityConfig.RegisterTypes(_container);
            _orderRepository = _container.Resolve<IOrderRepository>(new ParameterOverride("factory", _factory));
            InMemorySeed.Seed(_factory);
        }

        [TestMethod]
        public void GetAllOrders()
        {
            var result = _orderRepository.GetAll().ToList();
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetOrderById()
        {
            var result = _orderRepository.GetByID(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetOrderById_NonExisting()
        {
            var result = _orderRepository.GetByID(0);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetOrdersForCustomer()
        {
            using (var context = _factory.Create())
            {
                var customer = AutoMapperHelper.Instance.GetMappedValue<Customer>(context.Customers.Find(1), context);
                var result = _orderRepository.GetOrdersForCustomer(customer.Id).ToList();
                Assert.AreEqual(result.Count, customer.Orders.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetOrdersForCustomer_NonExistingCustomer()
        {
            _orderRepository.GetOrdersForCustomer(0);
        }

        [TestMethod]
        public void GetOrderForCustomer_CustomerAndOrderExists_OrderNotNull()
        {
            using (var context = _factory.Create())
            {
                int existingOrderId = 1;
                var customer = AutoMapperHelper.Instance.GetMappedValue<Customer>(context.Customers.Find(1), context);
                var result = _orderRepository.GetOrderForCustomer(existingOrderId, customer.Id);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetOrderForCustomer_CustomerDoseNotExistsOrderExists_ArgumentExceptionThrown()
        {
            using (var context = _factory.Create())
            {
                int existingOrderId = 1;
                int nonExistingCustomerId = 999;
                var result = _orderRepository.GetOrderForCustomer(existingOrderId, nonExistingCustomerId);
            }
        }

        [TestMethod]
        public void GetOrderForCustomer_CustomerExistsOrderDoesNotExists_OrderIsNull()
        {
            using (var context = _factory.Create())
            {
                int nonExistingOrderId = 999;
                var customer = AutoMapperHelper.Instance.GetMappedValue<Customer>(context.Customers.Find(1), context);
                var result = _orderRepository.GetOrderForCustomer(nonExistingOrderId, customer.Id);
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void GetOrdersForDate()
        {
            var result = _orderRepository.GetOrdersForDate(DateTime.Today).ToList();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateOrder()
        {
            using (var context = _factory.Create())
            {
                var order = AutoMapperHelper.Instance.GetMappedValue<Order>(context.Orders.Find(1), context);
                var newFood = AutoMapperHelper.Instance.GetMappedValue<Food>(context.Foods.Find(1), context);
                order.Note = "New note";
                order.Meal.Foods.Add(newFood);
                var result = _orderRepository.Update(order);
                Assert.AreEqual(result.Note, order.Note);
                Assert.AreEqual(result.Meal.Foods.Count, order.Meal.Foods.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateOrder_IsNull()
        {
            _orderRepository.Update(null);
        }

        [TestMethod]
        public void AddOrder()
        {
            using (var context = _factory.Create())
            {
                var newFood = AutoMapperHelper.Instance.GetMappedValue<Food>(context.Foods.First(), context);
                var customer = AutoMapperHelper.Instance.GetMappedValue<Customer>(context.Customers.First(), context);
                var order = new Order();
                order.Meal = new Meal();
                order.Meal.Foods = new List<Food>();
                order.Note = "New note";
                order.Meal.Foods.Add(newFood);
                order.Date = DateTime.Now;
                order.Customer = customer;
                var result = _orderRepository.Insert(order);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddOrder_IsNull()
        {
            _orderRepository.Insert(null);
        }
    }
}
