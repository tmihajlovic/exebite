using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class MealRepositoryTest
    {
        [Fact]
        public void Query_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var sut = CreateOnlyMealRepositoryInstanceNoData(Guid.NewGuid().ToString());

            // Act and Assert
            Exception res = Assert.Throws<ArgumentNullException>(() => sut.Query(null));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        public void Query_MultipleElements(int count)
        {
            // Arrange
            var sut = MealDataForTesing(Guid.NewGuid().ToString(), count);

            // Act
            var res = sut.Query(new MealQueryModel());

            // Assert
            Assert.Equal(count, res.Count);
        }

        [Fact]
        public void Query_QueryByIDId_ValidId()
        {
            // Arrange
            var sut = MealDataForTesing(Guid.NewGuid().ToString(), 1);

            // Act
            var res = sut.Query(new MealQueryModel() { Id = 1 });

            Assert.Equal(1, res.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void Query_QueryByIDId_NonExistingID(int id)
        {
            // Arrange
            var sut = MealDataForTesing(Guid.NewGuid().ToString(), 1);

            // Act
            var res = sut.Query(new MealQueryModel() { Id = id });

            // Assert
            Assert.Equal(0, res.Count);
        }

        [Fact]
        public void Insert_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var sut = CreateOnlyMealRepositoryInstanceNoData(Guid.NewGuid().ToString());

            // Act and Assert
            Exception res = Assert.Throws<ArgumentNullException>(() => sut.Insert(null));
        }

        [Fact]
        public void Insert_ValidObjectPassed_ObjectSavedInDatabase()
        {
            // Arrange
            var sut = MealDataForTesing(Guid.NewGuid().ToString(), 1);

            var meal = new Meal
            {
                Id = 2,
                Price = 23.4M,
                Foods = new List<Food> { new Food { Id = 1 } }
            };

            // Act
            var res = sut.Insert(meal);

            // Assert
            Assert.Equal(meal.Id, res.Id);
            Assert.Equal(meal.Price, res.Price);
            Assert.Equal(meal.Foods.Count, res.Foods.Count);
        }

        [Fact]
        public void Update_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var sut = CreateOnlyMealRepositoryInstanceNoData(Guid.NewGuid().ToString());

            // Act and Assert
            Exception res = Assert.Throws<ArgumentNullException>(() => sut.Update(null));
        }

        [Fact]
        public void Update_ValidObjectPassed_ObjectUpdatedInDatabase()
        {
            // Arrange
            var sut = MealDataForTesing(Guid.NewGuid().ToString(), 2);

            var updatedMeal = new Meal
            {
                Id = 1,
                Price = 333,
                Foods = new List<Food> { new Food { Id = 2 } }
            };

            // Act
            var res = sut.Update(updatedMeal);

            // Assert
            Assert.Equal(updatedMeal.Id, res.Id);
            Assert.Equal(updatedMeal.Price, res.Price);
            Assert.Equal(updatedMeal.Foods.Count, res.Foods.Count);
        }
    }
}
