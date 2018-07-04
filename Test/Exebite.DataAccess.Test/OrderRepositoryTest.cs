using System;
using Exebite.Common;
using Exebite.DataAccess.Test.Mocks;
using Exebite.DomainModel;
using Microsoft.Data.Sqlite;
using Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class OrderRepositoryTest
    {
        private readonly IGetDateTime _dateTime;

        public OrderRepositoryTest()
        {
            _dateTime = new GetDateTimeStub();
        }

        [Fact]
        public void Query_NullPassed_EmptyListReturned()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyOrderRepositoryInstanceNoData(connection);

            // Act and Assert
            var result = sut.Query(null);
            connection.Close();

            Assert.Equal(0, result.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        public void Query_MultipleElements(int count)
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = OrderDataForTesting(connection, count);

            // Act
            var res = sut.Query(new OrderQueryModel());
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
            var sut = OrderDataForTesting(connection, 1);

            // Act
            var res = sut.Query(new OrderQueryModel() { Id = 1 });
            connection.Close();

            Assert.Equal(1, res.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void Query_QueryByIDId_NonExistingID(int id)
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = OrderDataForTesting(connection, 1);

            // Act
            var res = sut.Query(new OrderQueryModel() { Id = id });
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
            var sut = CreateOnlyOrderRepositoryInstanceNoData(connection);

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
            var sut = OrderDataForTesting(connection, 1);

            var meal = new Order
            {
                Id = 2,
                Price = 23.4M,
                MealId = 1,
                CustomerId = 1,
                Date = _dateTime.Now(),
                Note = "Note"
            };

            // Act
            var res = sut.Insert(meal);
            connection.Close();

            // Assert
            Assert.Equal(meal.Id, res.Id);
            Assert.Equal(meal.Price, res.Price);
            Assert.Equal(meal.CustomerId, res.CustomerId);
            Assert.Equal(meal.Date, res.Date);
            Assert.Equal(meal.Note, res.Note);
        }

        [Fact]
        public void Update_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyOrderRepositoryInstanceNoData(connection);

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
            var sut = OrderDataForTesting(connection, 2);

            var updatedMeal = new Order
            {
                Id = 1,
                Price = 333.33m,
                CustomerId = 1,
                MealId = 1,
                Date = _dateTime.Now(),
                Note = "note"
            };

            // Act
            var res = sut.Update(updatedMeal);
            connection.Close();

            // Assert
            Assert.Equal(updatedMeal.Id, res.Id);
            Assert.Equal(updatedMeal.Price, res.Price);
            Assert.Equal(updatedMeal.CustomerId, res.CustomerId);
            Assert.Equal(updatedMeal.MealId, res.MealId);
            Assert.Equal(updatedMeal.Date, res.Date);
            Assert.Equal(updatedMeal.Note, res.Note);
        }
    }
}
