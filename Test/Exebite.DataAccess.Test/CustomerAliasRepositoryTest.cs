using System;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.Data.Sqlite;
using Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class CustomerAliasRepositoryTest
    {
        [Fact]
        public void Query_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyCustomerAliasRepositoryInstanceNoData(connection);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => sut.Query(null));
            connection.Close();
        }

        [Theory]
        [InlineData(100)]
        public void Query_MultipleElements(int count)
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CustomerAliasesDataForTesing(connection, count);

            // Act
            var res = sut.Query(new CustomerAliasQueryModel());
            connection.Close();

            // Assert
            Assert.Equal(count, res.Count);
        }

        [Fact]
        public void Query_QueryByIDId_ValidId()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CustomerAliasesDataForTesing(connection, 1);

            // Act
            var res = sut.Query(new CustomerAliasQueryModel() { Id = 1 });
            connection.Close();

            Assert.Equal(1, res.Count);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void Query_QueryByIDId_NonExistingID(int id)
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CustomerAliasesDataForTesing(connection, 1);

            // Act
            var res = sut.Query(new CustomerAliasQueryModel() { Id = id });
            connection.Close();

            // Assert
            Assert.Equal(0, res.Count);
        }

        [Fact]
        public void Insert_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyCustomerAliasRepositoryInstanceNoData(connection);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => sut.Insert(null));
            connection.Close();
        }

        [Fact]
        public void Insert_ValidObjectPassed_ObjectSavedInDatabase()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CustomerAliasesDataForTesing(connection, 1);

            var customerAlias = new CustomerAliases
            {
                Id = 2,
                Alias = "Alias",
                Customer = new Customer { Id = 1 },
                Restaurant = new Restaurant { Id = 1 }
            };

            // Act
            var res = sut.Insert(customerAlias);
            connection.Close();

            // Assert
            Assert.Equal(customerAlias.Id, res.Id);
            Assert.Equal(customerAlias.Alias, res.Alias);
            Assert.Equal(customerAlias.Customer.Id, res.Customer.Id);
            Assert.Equal(customerAlias.Restaurant.Id, res.Restaurant.Id);
        }

        [Fact]
        public void Update_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyCustomerAliasRepositoryInstanceNoData(connection);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => sut.Update(null));
            connection.Close();
        }

        [Fact]
        public void Update_ValidObjectPassed_ObjectUpdatedInDatabase()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CustomerAliasesDataForTesing(connection, 2);

            var updatedCustomerAlias = new CustomerAliases
            {
                Id = 1,
                Alias = "Alias updated",
                Restaurant = new Restaurant { Id = 2 },
                Customer = new Customer { Id = 2 }
            };

            // Act
            var res = sut.Update(updatedCustomerAlias);
            connection.Close();

            // Assert
            Assert.Equal(updatedCustomerAlias.Id, res.Id);
            Assert.Equal(updatedCustomerAlias.Alias, res.Alias);
            Assert.Equal(updatedCustomerAlias.Customer.Id, res.Customer.Id);
            Assert.Equal(updatedCustomerAlias.Restaurant.Id, res.Restaurant.Id);
        }
    }
}
