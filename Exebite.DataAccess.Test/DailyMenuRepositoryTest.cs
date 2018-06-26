using System;
using System.Collections.Generic;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
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
            var sut = CreateOnlyDailyMenuRepositoryInstanceNoData(Guid.NewGuid());

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => sut.Query(null));
        }

        [Theory]
        [InlineData(100)]
        public void Query_MultipleElements(int count)
        {
            // Arrange
            var sut = DailyMenuDataForTesing(Guid.NewGuid(), count);

            // Act
            var res = sut.Query(new DailyMenuQueryModel());

            // Assert
            Assert.Equal(count, res.Count);
        }

        [Fact]
        public void Query_QueryByIDId_ValidId()
        {
            // Arrange
            var sut = DailyMenuDataForTesing(Guid.NewGuid(), 1);

            // Act
            var res = sut.Query(new DailyMenuQueryModel() { Id = 1 });

            Assert.Equal(1, res.Count);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void Query_QueryByIDId_NonExistingID(int id)
        {
            // Arrange
            var sut = DailyMenuDataForTesing(Guid.NewGuid(), 1);

            // Act
            var res = sut.Query(new DailyMenuQueryModel() { Id = id });

            // Assert
            Assert.Equal(0, res.Count);
        }

        [Fact]
        public void Insert_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var sut = CreateOnlyDailyMenuRepositoryInstanceNoData(Guid.NewGuid());

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => sut.Insert(null));
        }

        [Fact]
        public void Insert_ValidObjectPassed_ObjectSavedInDatabase()
        {
            // Arrange
            var sut = DailyMenuDataForTesing(Guid.NewGuid(), 1);

            var dailyMenu = new DailyMenu
            {
                Id = 2,
                RestaurantId = 1
            };

            // Act
            var res = sut.Insert(dailyMenu);

            // Assert
            Assert.Equal(dailyMenu.Id, res.Id);
            Assert.Equal(dailyMenu.RestaurantId, res.RestaurantId);
        }

        [Fact]
        public void Update_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var sut = CreateOnlyDailyMenuRepositoryInstanceNoData(Guid.NewGuid());

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => sut.Update(null));
        }

        [Fact]
        public void Update_ValidObjectPassed_ObjectUpdatedInDatabase()
        {
            // Arrange
            var sut = DailyMenuDataForTesing(Guid.NewGuid(), 2);

            var updatedLocation = new DailyMenu
            {
                Id = 1,
                RestaurantId = 2,
                Foods = new List<Food> { new Food { Id = 1 }, new Food { Id = 2 } }
            };

            // Act
            var res = sut.Update(updatedLocation);

            // Assert
            Assert.Equal(updatedLocation.Id, res.Id);
            Assert.Equal(updatedLocation.RestaurantId, res.RestaurantId);
            Assert.Equal(updatedLocation.Foods.Count, res.Foods.Count);
        }

        [Fact]
        public void Update_SomeFoodReferenceAlreadyExists_ObjectUpdatedInDatabase()
        {
            // Arrange
            var sut = DailyMenuDataForTesing(Guid.NewGuid(), 2);

            var updatedLocation = new DailyMenu
            {
                Id = 1,
                RestaurantId = 2,
                Foods = new List<Food> { new Food { Id = 1 } }
            };

            sut.Update(updatedLocation);

            updatedLocation = new DailyMenu
            {
                Id = 1,
                RestaurantId = 2,
                Foods = new List<Food> { new Food { Id = 1 }, new Food { Id = 2 } }
            };

            // Act
            var res = sut.Update(updatedLocation);

            // Assert
            Assert.Equal(updatedLocation.Id, res.Id);
            Assert.Equal(updatedLocation.RestaurantId, res.RestaurantId);
            Assert.Equal(updatedLocation.Foods.Count, res.Foods.Count);
        }
    }
}
