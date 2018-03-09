using AutoMapper.QueryableExtensions;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Test.InMemoryDB;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Unity;
using Unity.Resolution;

namespace Exebite.DataAccess.Test.Tests
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        private static IFoodOrderingContextFactory _factory;
        private static ICustomerRepository _customerRepository;
        private static IUnityContainer _container;
        private static List<Location> _locations;
        private static List<Customer> _customers;
        private static List<Restaurant> _restaurants;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _container = new UnityContainer();
            Unity.UnityConfig.RegisterTypes(_container);
            _customerRepository = _container.Resolve<ICustomerRepository>(new ParameterOverride("factory", _factory));
            InMemorySeed.Seed(_factory);
            using (var context = _factory.Create())
            {
                _locations = context.Locations.Select(l => AutoMapperHelper.Instance.GetMappedValue<Location>(l)).ToList();
                _customers = context.Customers.Select(c => AutoMapperHelper.Instance.GetMappedValue<Customer>(c)).ToList();
                _restaurants = context.Restaurants.Select(r => AutoMapperHelper.Instance.GetMappedValue<Restaurant>(r)).ToList();
            }
        }

        [TestMethod]
        public void InsertNewCustomer()
        {
            var newCustomer = new Customer()
            {
                Name = "TestCustomer1",
                AppUserId = "TestAppUserString",
                Balance = 0,
                Location = new Location() { Id = 1 }
            };
            var result = _customerRepository.Insert(newCustomer);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllCustomers()
        {
            var result = _customerRepository.GetAll().ToList();
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetCustomerById()
        {
            var result = _customerRepository.GetByID(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByName()
        {
            var result = _customerRepository.GetByName("Test Customer");
            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public void UpdateAlias()
        {
            var customer = _customerRepository.GetByID(1);
            var restaurant = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(_restaurants.FirstOrDefault());
            var newAlisas = new CustomerAliases
            {
                Alias = "Test Alisas",
                Customer = customer,
                Restaurant = restaurant
            };
            customer.Aliases.Add(newAlisas);
            var result = _customerRepository.Update(customer);
            Assert.AreEqual(result.Aliases.First().Alias, "Test Alisas");
        }

        [TestMethod]
        public void UpdateLocation()
        {
            var customer = _customerRepository.GetByID(1);
            var newLocation = AutoMapperHelper.Instance.GetMappedValue<Location>(_locations.FirstOrDefault(l => l.Id == 2));
            customer.Location = newLocation;
            var result = _customerRepository.Update(customer);
            Assert.AreEqual(result.Location.Name, "JD");
        }

        [TestMethod]
        public void UpdateDetails()
        {
            var customer = _customerRepository.GetByID(1);
            customer.Name = "NewName";
            customer.AppUserId = "NewAppUserId";
            var result = _customerRepository.Update(customer);
            Assert.AreEqual(result.Name, "NewName");
            Assert.AreEqual(result.AppUserId, "NewAppUserId");
        }

        [TestMethod]
        public void RemoveCustomer()
        {
            var customerForDelete = _customerRepository.GetByName("Test Customer for delete");
            _customerRepository.Delete(customerForDelete.Id);
            var result = _customerRepository.GetByID(customerForDelete.Id);
            Assert.IsNull(result);
        }
    }
}
