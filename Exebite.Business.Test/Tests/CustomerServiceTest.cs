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
    public class CustomerServiceTest
    {
        private static ICustomerService _customerService;
        private static IFoodOrderingContextFactory _factory;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _customerService = new CustomerService(new CustomerRepository(_factory));
            InMemoryDBSeed.Seed(_factory);
        }

        [TestMethod]
        public void GetAllCustomer()
        {
            var result = _customerService.GetAllCustomers();
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetCustomerById()
        {
            var result = _customerService.GetCustomerById(1);
            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public void GetCustomerByIdentityId()
        {
            var appId = "TestAppUserId";
            var result = _customerService.GetCustomerByIdentityId(appId);
            Assert.AreEqual(result.AppUserId, appId);
        }

        [TestMethod]
        public void GetCustomerByName()
        {
            var name = "Test Customer";
            var result = _customerService.GetCustomerByName(name);
            Assert.AreEqual(result.Name, name);
        }

        [TestMethod]
        public void CreateCustomer()
        {
            using (var context = _factory.Create())
            {
                Customer newCustomer = new Customer()
                {
                    AppUserId = "New App ID",
                    Balance = 0,
                    Name = "New Customer",
                    Location = AutoMapperHelper.Instance.GetMappedValue<Location>(context.Locations.First(), context)
                };
                var result = _customerService.CreateCustomer(newCustomer);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void UpdateCustomerInfo()
        {
            using (var context = _factory.Create())
            {
                var newName = "New name";
                var newLocationId = 2;
                var customer = _customerService.GetAllCustomers().First();
                customer.Name = newName;
                customer.Location = AutoMapperHelper.Instance.GetMappedValue<Location>(context.Locations.Find(newLocationId), context);
                var result = _customerService.UpdateCustomer(customer);
                Assert.AreEqual(result.Name, newName);
                Assert.AreEqual(result.Location.Id, newLocationId);
            }
        }

        [TestMethod]
        public void DeleteCustomer()
        {
            var customerName = "Test Customer for delete";
            var customer = _customerService.GetCustomerByName(customerName);
            _customerService.DeleteCustomer(customer.Id);
            var result = _customerService.GetCustomerByName(customerName);
            Assert.IsNull(result);
        }
    }
}
