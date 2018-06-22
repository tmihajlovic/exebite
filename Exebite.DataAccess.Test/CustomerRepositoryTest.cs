using System;
using System.Linq;
using Exebite.Common.Reflecsion;
using Exebite.DataAccess.Repositories;
using Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class CustomerRepositoryTest
    {
        // removed due InMemmory limitation of not reseting id
        //[InlineData(1, 1)]
        //[InlineData(2, 2)]
        //[InlineData(3, 2)]
        //[InlineData(50, 2)]
        //public void GetById_ValidId_ValidResult(int count, int id)
        //{
        //    var customerRepository = FillCustomerDataForTesing(Guid.NewGuid().ToString(), CreateCustomerEntities(0, count));

        //    var res = customerRepository.GetByID(id);

        //    Assert.NotNull(res);
        //    Assert.Equal(id, res.Id);
        //}

        [Theory]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 4)]
        public void GetById_InValidId_ValidResult(int startId, int count)
        {
            var customerRepository = FillCustomerDataForTesing(Methods.GetCurrentMethod() + startId.ToString() + count.ToString(), CreateCustomerEntities(0, count));

            var res = customerRepository.GetByID(count + 1);

            Assert.Null(res);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        public void Query_MultipleElements(int count)
        {
            CustomerRepository customerRepository = FillCustomerDataForTesing(Methods.GetCurrentMethod() + count, CreateCustomerEntities(1, count));

            var res = customerRepository.Query(new CustomerQueryModel());

            Assert.Equal(count, res.Count);
        }


        // removed due InMemmory limitation of not reseting id
        //[Fact]
        //public void Query_QueryByIDId_ValidId()
        //{
        //    CustomerRepository customerRepository = FillCustomerDataForTesing(Methods.GetCurrentMethod(), CreateCustomerEntities(0, 1));

        //    var res = customerRepository.Query(new CustomerQueryModel() { Id = 1 });

        //    Assert.Equal(1, res.Count);
        //}

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void Query_QueryByIDId_NonExistingID(int id)
        {
            CustomerRepository customerRepository = FillCustomerDataForTesing(Methods.GetCurrentMethod() + id, CreateCustomerEntities(id + 1, 1));

            var res = customerRepository.Query(new CustomerQueryModel() { Id = id });

            Assert.Equal(0, res.Count);
        }


        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 5)]
        [InlineData(2, 5)]
        public void Insert_InsertValid_CheckInsert(int idToSearchFor, int number)
        {
            var customers = CreateCustomerEntities(idToSearchFor, number);

            var customerRepsitory = FillCustomerDataForTesing(Guid.NewGuid().ToString(), customers);

            var customer = CreateCustomers(0, 1).FirstOrDefault();

            var resultingCustomer = customerRepsitory.Insert(customer);

           // Assert.Equal(number + 1, resultingCustomer.Id);
            Assert.Equal(customer.Balance, resultingCustomer.Balance);
            Assert.Equal(customer.LocationId, resultingCustomer.LocationId);
            Assert.Equal(customer.Name, resultingCustomer.Name);
        }
    }
}
