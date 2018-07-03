using System;
using System.Collections.Generic;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.Data.Sqlite;
using Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class DailyMenuRepositoryTest
    {
        [Fact]
        public void Query_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyDailyMenuRepositoryInstanceNoData(connection);

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
            var sut = DailyMenuDataForTesing(connection, count);

            // Act
            var res = sut.Query(new DailyMenuQueryModel());
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
            var sut = DailyMenuDataForTesing(connection, 2);

            // Act
            var res = sut.Query(new DailyMenuQueryModel() { Id = 1 });
            connection.Close();

            Assert.Equal(1, res.Count);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void Query_QueryByIDNonExistingID_ReturnsEmpty(int id)
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = DailyMenuDataForTesing(connection, 1);

            // Act
            var res = sut.Query(new DailyMenuQueryModel() { Id = id });
            connection.Close();

            // Assert
            Assert.Empty(res);
        }

        [Fact]
        public void Insert_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyDailyMenuRepositoryInstanceNoData(connection);

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
            var sut = DailyMenuDataForTesing(connection, 2);

            var dailyMenu = new DailyMenu
            {
                Id = 2,
                RestaurantId = 2,
                Foods = new List<Food> { new Food { Id = 1 } }
            };

            // remove daily menu that will be added again. Just need ref to restaurant
            sut.Delete(2);

            // Act
            var res = sut.Insert(dailyMenu);
            connection.Close();

            // Assert
            Assert.Equal(dailyMenu.Id, res.Id);
            Assert.Equal(dailyMenu.RestaurantId, res.RestaurantId);
            Assert.Equal(dailyMenu.Foods.Count, res.Foods.Count);
        }

        [Fact]
        public void Update_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyDailyMenuRepositoryInstanceNoData(connection);

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
            var sut = DailyMenuDataForTesing(connection, 2);

            var updatedLocation = new DailyMenu
            {
                Id = 1,
                RestaurantId = 2,
                Foods = new List<Food> { new Food { Id = 1 }, new Food { Id = 2 } }
            };

            // remove reference to restaurant 2
            sut.Delete(2);

            // Act
            var res = sut.Update(updatedLocation);
            connection.Close();

            // Assert
            Assert.Equal(updatedLocation.Id, res.Id);
            Assert.Equal(updatedLocation.RestaurantId, res.RestaurantId);
            Assert.Equal(updatedLocation.Foods.Count, res.Foods.Count);
        }

        [Fact]
        public void Update_SomeFoodReferenceAlreadyExists_ObjectUpdatedInDatabase()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = DailyMenuDataForTesing(connection, 2);

            var updatedLocation = new DailyMenu
            {
                Id = 1,
                RestaurantId = 1,
                Foods = new List<Food> { new Food { Id = 1 } }
            };

            sut.Update(updatedLocation);

            // remove reference to restaurant 2
            sut.Delete(2);

            updatedLocation = new DailyMenu
            {
                Id = 1,
                RestaurantId = 2,
                Foods = new List<Food> { new Food { Id = 1 }, new Food { Id = 2 } }
            };

            // Act
            var res = sut.Update(updatedLocation);
            connection.Close();

            // Assert
            Assert.Equal(updatedLocation.Id, res.Id);
            Assert.Equal(updatedLocation.RestaurantId, res.RestaurantId);
            Assert.Equal(updatedLocation.Foods.Count, res.Foods.Count);
        }
    }
}
