using System;
using System.Linq;
using AutoMapper;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class CustomerServiceTest
    {
        private static ICustomerRepository _customerRepository;
        private static IFoodOrderingContextFactory _factory;

        private static IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            var container = ServiceProviderWrapper.GetContainer();
            _factory = container.Resolve<IFoodOrderingContextFactory>();
            _mapper = container.Resolve<IMapper>();
            InMemoryDBSeed.Seed(_factory);
            _customerRepository = container.Resolve<ICustomerRepository>();
        }

        [TestMethod]
        public void GetAllCustomer()
        {
            var result = _customerRepository.Get(0, int.MaxValue);
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetCustomerById()
        {
            var result = _customerRepository.GetByID(1);
            Assert.AreEqual(result.Id, 1);
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
            const string name = "Test Customer";
            var result = _customerRepository.GetByName(name);
            Assert.AreEqual(result.Name, name);
        }

        [TestMethod]
        public void GetCustomerByName_NonExisting()
        {
            const string name = "Non existing customer";
            var result = _customerRepository.GetByName(name);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCustomerByName_EmptyString()
        {
            _customerRepository.GetByName(string.Empty);
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
                    Location = _mapper.Map<Location>(context.Locations.First())
                };
                var result = _customerRepository.Insert(newCustomer);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateCustomer_Null()
        {
            _customerRepository.Insert(null);
        }

        [TestMethod]
        public void UpdateCustomerInfo()
        {
            using (var context = _factory.Create())
            {
                const string newName = "New name";
                const int newLocationId = 2;
                var customer = _customerRepository.Get(0, int.MaxValue).First();
                customer.Name = newName;
                customer.LocationId = newLocationId;
                var result = _customerRepository.Update(customer);
                Assert.AreEqual(result.Name, newName);
                Assert.AreEqual(result.LocationId, newLocationId);
            }
        }

        [TestMethod]
        public void DeleteCustomer()
        {
            const string customerName = "Test Customer for delete";
            var customer = _customerRepository.GetByName(customerName);
            _customerRepository.Delete(customer.Id);
            var result = _customerRepository.GetByName(customerName);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteCustomer_NonExisting()
        {
            _customerRepository.Delete(0);
        }
    }
}
