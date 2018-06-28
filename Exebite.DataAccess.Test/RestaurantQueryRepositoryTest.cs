using System;
using System.Linq;
using Exebite.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Optional.Xunit;
using Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class RestaurantQueryRepositoryTest
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(50, 2)]
        public void GetById_ValidId_ValidResult(int count, int id)
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = RestaurantQueryDataForTesting(connection, count);

            // Act
            var res = sut.Query(new RestaurantQueryModel { Id = id });
            connection.Close();

            // Assert
            EAssert.IsRight(res);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_InValidId_ValidResult(int count)
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = RestaurantQueryDataForTesting(connection, count);

            // Act
            var res = sut.Query(new RestaurantQueryModel() { Id = 999 });
            connection.Close();

            // Assert
            EAssert.IsRight(res);
            var result = res.RightContent();
            Assert.Empty(result.Items);
        }

        [Fact]
        public void Query_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyRestaurantQueryRepositoryInstanceNoData(connection);

            // Act and Assert
            var res = sut.Query(null);
            connection.Close();

            EAssert.IsLeft(res);
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
            var sut = RestaurantQueryDataForTesting(connection, count);

            // Act
            var res = sut.Query(new RestaurantQueryModel());
            connection.Close();

            EAssert.IsRight(res);
            var result = res.RightContent();
            Assert.Equal(count, result.Items.Count());
        }

        [Fact]
        public void Query_QueryByIDId_ValidId()
        {
            // Arrange
            const int validId = 1;
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = RestaurantQueryDataForTesting(connection, 1);

            // Act
            var res = sut.Query(new RestaurantQueryModel() { Id = validId });
            connection.Close();

            EAssert.IsRight(res);

            var result = res.RightContent();

            Assert.Single(result.Items);

            var item = result.Items.FirstOrDefault();

            Assert.Equal(validId, item.Id);
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
            var sut = RestaurantQueryDataForTesting(connection, 1);

            // Act
            var res = sut.Query(new RestaurantQueryModel() { Id = id });
            connection.Close();

            // Assert
            EAssert.IsRight(res);
            var result = res.RightContent();
            Assert.Empty(result.Items);
        }

        [Fact]
        public void Insert_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyRestaurantRepositoryInstanceNoData(connection);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => sut.Insert(null));
            connection.Close();
        }

        [Fact]
        public void Update_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyFoodRepositoryInstanceNoData(connection);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => sut.Update(null));
            connection.Close();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(50)]
        public void Query_ValidId_ValidResult(int count)
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = RestaurantQueryDataForTesting(connection, count);

            // Act
            var res = sut.Query(new RestaurantQueryModel(1, QueryConstants.MaxElements));
            connection.Close();

            // Assert
            EAssert.IsRight(res);

            var result = res.RightContent();

            Assert.Equal(count, result.Total);
            Assert.Equal(count, result.Items.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(99)]
        public void Query_ValidId_ValidResultLimited(int count)
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = RestaurantQueryDataForTesting(connection, QueryConstants.MaxElements + count);

            // Act
            var res = sut.Query(new RestaurantQueryModel(1, QueryConstants.MaxElements + count));
            connection.Close();

            // Assert
            EAssert.IsRight(res);

            var result = res.RightContent();

            Assert.Equal(QueryConstants.MaxElements + count, result.Total);
            Assert.Equal(QueryConstants.MaxElements, result.Items.Count());
        }

        [Fact]
        public void Query_ErrorReturned()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = RestaurantQueryDataForTesting(connection, 20);

            // Act
            var res = sut.Query(null);
            connection.Close();

            // Assert
            EAssert.IsLeft(res);
        }
    }
}
