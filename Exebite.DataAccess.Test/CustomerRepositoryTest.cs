using System;
using System.Linq;
using Exebite.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class CustomerRepositoryTest
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(50, 2)]
        public void GetById_ValidId_ValidResult(int count, int id)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var customerRepository = FillCustomerDataForTesting(connection, CreateCustomerEntities(count));

            var res = customerRepository.GetByID(id);
            connection.Close();

            Assert.NotNull(res);
            Assert.Equal(id, res.Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void GetById_InValidId_ValidResult(int count)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var customerRepository = FillCustomerDataForTesting(connection, CreateCustomerEntities(count));

            var res = customerRepository.GetByID(count + 1);
            connection.Close();

            Assert.Null(res);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        public void Query_MultipleElements(int count)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            CustomerRepository customerRepository = FillCustomerDataForTesting(connection, CreateCustomerEntities(count));

            var res = customerRepository.Query(new CustomerQueryModel());
            connection.Close();

            Assert.Equal(count, res.Count);
        }

        [Fact]
        public void Query_QueryByIDId_ValidId()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            CustomerRepository customerRepository = FillCustomerDataForTesting(connection, CreateCustomerEntities(2));

            var res = customerRepository.Query(new CustomerQueryModel() { Id = 1 });
            connection.Close();

            Assert.Equal(1, res.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void Query_QueryByIDId_NonExistingID(int id)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            CustomerRepository customerRepository = FillCustomerDataForTesting(connection, CreateCustomerEntities(1));

            var res = customerRepository.Query(new CustomerQueryModel() { Id = id + 1 });
            connection.Close();

            Assert.Equal(0, res.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Insert_InsertValid_CheckInsert(int number)
        {
            var customers = CreateCustomerEntities(0);
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            LocationDataForTesing(connection, 6);
            var customerRepsitory = FillCustomerDataForTesting(connection, customers);

            var customer = CreateCustomers(number + 1, 1).FirstOrDefault();

            var resultingCustomer = customerRepsitory.Insert(customer);
            connection.Close();

            Assert.Equal(customer.Balance, resultingCustomer.Balance);
            Assert.Equal(customer.LocationId, resultingCustomer.LocationId);
            Assert.Equal(customer.Name, resultingCustomer.Name);
        }
    }
}
