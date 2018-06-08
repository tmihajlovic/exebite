using System;
using System.Linq;
using Exebite.DataAccess.AutoMapper;
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
    public class CustomerRepositoryTest
    {
        private static IFoodOrderingContextFactory _factory;
        private static ICustomerRepository _customerRepository;
        private static IExebiteMapper _mapper;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            //_factory = new InMemoryDBFactory();
            //_mapper = new ExebiteMapper(new ServiceProvider());
            //_customerRepository = new CustomerRepository(_factory, _mapper);
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertNewCustomer_Invalid_IsNull()
        {
            _customerRepository.Insert(null);
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
        public void GetCustomerById_NonExisting()
        {
            var result = _customerRepository.GetByID(0);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetCustomerByName()
        {
            var result = _customerRepository.GetByName("Test Customer");
            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public void GetCustomerByName_NonExisting()
        {
            var result = _customerRepository.GetByName("Non Existing Customer");
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCustomerByName_StringEmpty()
        {
            _customerRepository.GetByName(string.Empty);
        }

        [TestMethod]
        public void UpdateAlias()
        {
            using (var context = _factory.Create())
            {
                var customer = _customerRepository.GetByID(1);
                var restaurant = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(context.Restaurants.FirstOrDefault(), context);
                var newAlisas = new CustomerAliases
                {
                    Alias = "Test Alisas",
                    Customer = customer,
                    Restaurant = restaurant
                };
                customer.Aliases.Add(newAlisas);
                var result = _customerRepository.Update(customer);
                Assert.IsNotNull(result.Aliases.Where(a => a.Alias == "Test Alisas"));
            }
        }

        [TestMethod]
        public void UpdateLocation()
        {
            using (var context = _factory.Create())
            {
                var customer = _customerRepository.GetByID(1);
                var newLocation = AutoMapperHelper.Instance.GetMappedValue<Location>(context.Locations.FirstOrDefault(l => l.Id == 2), context);
                customer.Location = newLocation;
                var result = _customerRepository.Update(customer);
                Assert.AreEqual(result.Location.Name, "JD");
            }
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCustomer_IsNull()
        {
            _customerRepository.Update(null);
        }

        [TestMethod]
        public void RemoveCustomer()
        {
            var customerForDelete = _customerRepository.GetByName("Test Customer for delete");
            _customerRepository.Delete(customerForDelete.Id);
            var result = _customerRepository.GetByID(customerForDelete.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RemoveCustomer_NonExisting()
        {
            _customerRepository.Delete(0);
        }
    }
}
