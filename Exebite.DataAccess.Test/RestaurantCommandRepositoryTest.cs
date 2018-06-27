using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Optional.Xunit;
using System;
using Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public class RestaurantCommandRepositoryTest
    {
        [Fact]
        public void Delete_ExistingRecordIdPassed_ObjectDeletedFromDatabase()
        {
            // Arrange
            var sut = RestaurantCommandDataForTesting(Guid.NewGuid().ToString(), 1);
            const int existingId = 1;

            // Act
            var res = sut.Delete(existingId);

            // Assert
            EAssert.IsRight(res);
            var result = res.RightContent();
            Assert.True(result);

        }


        [Fact]
        public void Update_ValidObjectPassed_ObjectUpdatedInDatabase()
        {
            // Arrange
            var sut = RestaurantCommandDataForTesting(Guid.NewGuid().ToString(), 1);

            var updatedRestaurant = new RestaurantUpdateModel
            {
                Name = "Restaurant name updated",
                DailyMenuId = 1
            };

            // Act
            var res = sut.Update(1, updatedRestaurant);

            // Assert
            EAssert.IsRight(res);
            var result = res.RightContent();
            Assert.True(result);

            ////Assert.Equal(updatedRestaurant.Id, result.Id);
            ////Assert.Equal(updatedRestaurant.Name, result.Name);
            ////Assert.Equal(updatedRestaurant.DailyMenuId, result.DailyMenuId);
        }

        [Fact]
        public void Insert_ValidObjectPassed_ObjectSavedInDatabase()
        {
            // Arrange
            var sut = RestaurantCommandDataForTesting(Guid.NewGuid().ToString(), 0);

            var restaurant = new RestaurantInsertModel
            {
                Name = "Restaurant name",
                DailyMenuId = 1
            };

            // Act
            var res = sut.Insert(restaurant);

            // Assert
            EAssert.IsRight(res);
            var result = res.RightContent();
            Assert.Equal(1, result);
        }


    }
}
