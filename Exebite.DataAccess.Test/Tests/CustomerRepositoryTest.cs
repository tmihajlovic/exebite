using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.InMemoryDB;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _container = new UnityContainer();
            Unity.UnityConfig.RegisterTypes(_container);
            _customerRepository = _container.Resolve<ICustomerRepository>(new ParameterOverride("factory", _factory));
            InMemorySeed.Seed(_factory);
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
            customer.Aliases.Add(new CustomerAliases { Alias = "Test Alisas", Customer = customer, Restaurant = new Restaurant { Id = 1 } });
            var result = _customerRepository.Update(customer);
            Assert.AreEqual(result.Aliases.First().Alias, "Test Alisas");
        }

        [TestMethod]
        public void UpdateLocation()
        {
            var customer = _customerRepository.GetByID(1);
            customer.Location = new Location { Id = 2 };
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
            _customerRepository.Delete(1);
            var result = _customerRepository.GetByID(1);
        }
    }
}
