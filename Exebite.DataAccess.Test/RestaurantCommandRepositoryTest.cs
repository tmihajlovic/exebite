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
        //[Fact]
        //public void Delete_ExistingRecordIdPassed_ObjectDeletedFromDatabase()
        //{
        //    // Arrange
        //    var sut = RestaurantCommandDataForTesting(Guid.NewGuid().ToString(), 1);
        //    const int existingId = 1;

        //    Assert.NotNull(sut.GetByID(existingId));

        //    // Act
        //    sut.Delete(existingId);

        //    // Assert
        //    Assert.Null(sut.GetByID(existingId));
        //}


        //[Fact]
        //public void Update_ValidObjectPassed_ObjectUpdatedInDatabase()
        //{
        //    // Arrange
        //    var sut = RestaurantCommandDataForTesting(Guid.NewGuid().ToString(), 1);

        //    var updatedRestaurant = new Restaurant
        //    {
        //        Id = 1,
        //        Name = "Restaurant name updated",
        //        DailyMenuId = 1
        //    };

        //    // Act
        //    var res = sut.Update(updatedRestaurant);

        //    // Assert
        //    Assert.Equal(updatedRestaurant.Id, res.Id);
        //    Assert.Equal(updatedRestaurant.Name, res.Name);
        //    Assert.Equal(updatedRestaurant.DailyMenuId, res.DailyMenu.Id);
        //}

        [Fact]
        public void Insert_ValidObjectPassed_ObjectSavedInDatabase()
        {
            // Arrange
            var sut = RestaurantCommandDataForTesting(Guid.NewGuid().ToString(), 0);

            var restaurant = new RestourantInsertModel
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
