using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exebite.Business;
using Exebite.Model;
using BusinessTest;
using Exebite.DataAccess;

namespace Business.Test.Services
{
    [TestClass]
    public class CustomerServiceTest
    {
        static ICustomerService _customerService;
        static ICustomerHandler _customerHandler;
        
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            var _customerHandlerMock = new Moq.Mock<ICustomerHandler>();
            _customerHandlerMock.Setup(f => f.Get()).Returns(FakeData.cutumerList);
            _customerHandlerMock.Setup(f => f.GetByID(1)).Returns(FakeData.customer);
            _customerHandlerMock.Setup(f => f.GetByName("TestName")).Returns(FakeData.customer);
            _customerHandlerMock.Setup(f => f.GetByName("TestNameInvalid")).Returns((Customer)null);

            _customerHandler = _customerHandlerMock.Object;
            _customerService = new CustomerService(_customerHandler);

        }

        [TestMethod]
        public void GetAllCutomers()
        {
            var customers = _customerService.GetAllCustomers();
            Assert.IsNotNull(customers);
        }
        [TestMethod]
        public void GetCustomerById()
        {
            var custumer = _customerService.GetCustomerById(1);
            Assert.AreEqual(custumer, FakeData.customer);
        }
        [TestMethod]
        public void GetCustomerByName_Valid()
        {
            var customer =_customerService.GetCustomerByName("TestName");
            Assert.AreEqual(customer, FakeData.customer);
        }
        [TestMethod]
        public void GetCustomerByName_Invalid()
        {
            var customer = _customerService.GetCustomerByName("TestNameInvalid");
            Assert.AreEqual(customer, null);
        }
    }
}
