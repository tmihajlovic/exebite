using Exebite.DataAccess;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
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
            ICustomerRepository customerHandler = new CustomerRepository(_ctxFactory);
            var result = customerHandler.GetAll();
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
            ICustomerRepository customerHandler = new CustomerRepository(_ctxFactory);
            customerHandler.Insert(cust);
        }

        //live delete
        //[TestMethod]
        //public void GetById_Pass_DoesntThrowException()
        //{
        //    Customer cust = new Customer
        //    {
        //        Name = "Test",
        //        Balance = 0,
        //        Location = new Location
        //        {
        //            Name = "Test1",
        //            Address = "Test Test 14",
        //            Id = 1

        //        }
        //    };
        //    ICustomerRepository customerHandler = new CustomerRepository(_ctxFactory);
        //    customerHandler.Delete(1);
        //}
    }
}
