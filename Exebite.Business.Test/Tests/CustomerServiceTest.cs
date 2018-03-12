using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
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
    }
}
