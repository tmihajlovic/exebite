using System;
using Exebite.Common.Reflecsion;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
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
            var sut = CreateOnlyCustomerAliasRepositoryInstanceNoData(Methods.GetCurrentMethod());

            // Act and Assert
            Exception res = Assert.Throws<ArgumentNullException>(() => sut.Query(null));
        }

        [Theory]
        [InlineData(100)]
        public void Query_MultipleElements(int count)
        {
            // Arrange
            var sut = CustomerAliasesDataForTesing(Methods.GetCurrentMethod() + count, count);

            // Act
            var res = sut.Query(new CustomerAliasQueryModel());

            // Assert
            Assert.Equal(count, res.Count);
        }

        [Fact]
        public void Query_QueryByIDId_ValidId()
        {
            // Arrange
            var sut = CustomerAliasesDataForTesing(Methods.GetCurrentMethod(), 1);

            // Act
            var res = sut.Query(new CustomerAliasQueryModel() { Id = 1 });

            Assert.Equal(1, res.Count);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void Query_QueryByIDId_NonExistingID(int id)
        {
            // Arrange
            var sut = CustomerAliasesDataForTesing(Methods.GetCurrentMethod() + id, 1);

            // Act
            var res = sut.Query(new CustomerAliasQueryModel() { Id = id });

            // Assert
            Assert.Equal(0, res.Count);
        }

        [Fact]
        public void Insert_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var sut = CreateOnlyCustomerAliasRepositoryInstanceNoData(Methods.GetCurrentMethod());

            // Act and Assert
            Exception res = Assert.Throws<ArgumentNullException>(() => sut.Insert(null));
        }

        [Fact]
        public void Insert_ValidObjectPassed_ObjectSavedInDatabase()
        {
            // Arrange
            var sut = CustomerAliasesDataForTesing(Methods.GetCurrentMethod(), 1);

            var customerAlias = new CustomerAliases
            {
                Id = 2,
                Alias = "Alias",
                Customer = new Customer { Id = 1 },
                Restaurant = new Restaurant { Id = 1 }
            };

            // Act
            var res = sut.Insert(customerAlias);

            // Assert
            Assert.Equal(customerAlias.Id, res.Id);
            Assert.Equal(customerAlias.Alias, res.Alias);
            Assert.Equal(customerAlias.Customer.Id, res.Customer.Id);
            Assert.Equal(customerAlias.Restaurant.Id, res.Customer.Id);
        }

        [Fact]
        public void Update_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var sut = CreateOnlyCustomerAliasRepositoryInstanceNoData(Methods.GetCurrentMethod());

            // Act and Assert
            Exception res = Assert.Throws<ArgumentNullException>(() => sut.Update(null));
        }

        [Fact]
        public void Update_ValidObjectPassed_ObjectUpdatedInDatabase()
        {
            // Arrange
            var sut = CustomerAliasesDataForTesing(Methods.GetCurrentMethod(), 2);

            var updatedLocation = new CustomerAliases
            {
                Id = 1,
                Alias = "Alias updated",
                Restaurant = new Restaurant { Id = 2 },
                Customer = new Customer { Id = 2 }
            };

            // Act
            var res = sut.Update(updatedLocation);

            // Assert
            Assert.Equal(updatedLocation.Id, res.Id);
            Assert.Equal(updatedLocation.Alias, res.Alias);
            Assert.Equal(updatedLocation.Customer.Id, res.Customer.Id);
            Assert.Equal(updatedLocation.Restaurant.Id, res.Restaurant.Id);
        }
    }
}
