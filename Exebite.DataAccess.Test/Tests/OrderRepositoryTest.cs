using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Migrations;
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
            Unity.UnityConfig.RegisterTypes(_container);
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
    }
}
