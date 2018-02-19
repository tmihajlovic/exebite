using Exebite.DataAccess.Context;
using Exebite.DataAccess.Customers;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessTest
{
    [TestClass]
    public class CustomerHandlerTest
    {
        IFoodOrderingContextFactory _ctxFactory = new FoodOrderingContextFactory();
        [TestMethod]
        public void Get_Pass_NotNull()
        {
            ICustomerHandler customerHandler = new CustomerHandler(_ctxFactory);
            var result = customerHandler.Get();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Insert_Pass_DoesntThrowException()
        {
            Customer cust = new Customer
            {
                 Name = "Test",
                 Balance = 0,
                 Location = new Location
                 {
                     Name = "Test1",
                     Address = "Test Test 14",
                     Id = 1

                 }
            };
            ICustomerHandler customerHandler = new CustomerHandler(_ctxFactory);
            customerHandler.Insert(cust);
        }


        [TestMethod]
        public void GetById_Pass_DoesntThrowException()
        {
            Customer cust = new Customer
            {
                Name = "Test",
                Balance = 0,
                Location = new Location
                {
                    Name = "Test1",
                    Address = "Test Test 14",
                    Id = 1

                }
            };
            ICustomerHandler customerHandler = new CustomerHandler(_ctxFactory);
            customerHandler.Delete(1);
        }
    }
}
