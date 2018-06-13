using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.InMemoryDB;
using Exebite.Model;
using Exebite.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.DataAccess.Test.Tests
{
    [TestClass]
    public class OrderRepositoryTest
    {
        private static IFoodOrderingContextFactory _factory;
        private static IOrderRepository _orderRepository;
        private static IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            var cointeiner = ServiceProviderWrapper.GetContainer();
            _orderRepository = cointeiner.Resolve<IOrderRepository>();
            _factory = cointeiner.Resolve<IFoodOrderingContextFactory>();
            _mapper = cointeiner.Resolve<IMapper>();
            InMemorySeed.Seed(_factory);
        }

        [TestMethod]
        public void GetAllOrders()
        {
            var result = _orderRepository.Get(0, int.MaxValue);
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
                var customer = _mapper.Map<Customer>(context.Customers.Find(1));
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
                var customer = _mapper.Map<Customer>(context.Customers.Find(1));
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
                var customer = _mapper.Map<Customer>(context.Customers.Find(1));
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
                var order = _mapper.Map<Order>(context.Orders.Find(1));
                var newFood = _mapper.Map<Food>(context.Foods.Find(1));
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
            Order order = null;
            using (var context = _factory.Create())
            {
                var newFood = _mapper.Map<Food>(context.Foods.First());
                var customer = _mapper.Map<Customer>(context.Customers.First());
                order = new Order
                {
                    Meal = new Meal { Foods = new List<Food> { newFood } },
                    Note = "New note",
                    Date = DateTime.Now,
                    Customer = customer
                };
            }

            var result = _orderRepository.Insert(order);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddOrder_IsNull()
        {
            _orderRepository.Insert(null);
        }

        [TestMethod]
        public void QueryById()
        {
            var id = 1;
            var res = _orderRepository.Query(new OrderQueryModel()
            {
                Id = id
            });

            Assert.AreEqual(res.FirstOrDefault().Id, id);
        }
    }
}
