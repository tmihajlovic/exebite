using System;
using System.Collections.Generic;
using Exebite.DomainModel;
using Microsoft.Data.Sqlite;
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
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyMealRepositoryInstanceNoData(connection);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => sut.Query(null));
            connection.Close();
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
            var sut = MealDataForTesing(connection, count);

            // Act
            var res = sut.Query(new MealQueryModel());
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
            var sut = MealDataForTesing(connection, 1);

            // Act
            var res = sut.Query(new MealQueryModel() { Id = 1 });
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
            var sut = MealDataForTesing(connection, 1);

            // Act
            var res = sut.Query(new MealQueryModel() { Id = id });
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
            var sut = CreateOnlyMealRepositoryInstanceNoData(connection);

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
            var sut = MealDataForTesing(connection, 1);

            var meal = new Meal
            {
                Id = 2,
                Price = 23.4M,
                Foods = new List<Food> { new Food { Id = 1 } }
            };

            // Act
            var res = sut.Insert(meal);
            connection.Close();

            // Assert
            Assert.Equal(meal.Id, res.Id);
            Assert.Equal(meal.Price, res.Price);
            Assert.Equal(meal.Foods.Count, res.Foods.Count);
        }

        [Fact]
        public void Update_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyMealRepositoryInstanceNoData(connection);

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
            var sut = MealDataForTesing(connection, 2);

            var updatedMeal = new Meal
            {
                Id = 1,
                Price = 333,
                Foods = new List<Food> { new Food { Id = 2 } }
            };

            // Act
            var res = sut.Update(updatedMeal);
            connection.Close();

            // Assert
            Assert.Equal(updatedMeal.Id, res.Id);
            Assert.Equal(updatedMeal.Price, res.Price);
            Assert.Equal(updatedMeal.Foods.Count, res.Foods.Count);
        }
    }
}
