using System;
using System.Collections.Generic;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.Data.Sqlite;
using Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class RecipeRepositoryTest
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
            var sut = RecipeDataForTesting(connection, count);

            // Act
            var res = sut.GetByID(id);
            connection.Close();

            // Assert
            Assert.NotNull(res);
            Assert.Equal(id, res.Id);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_InValidId_ValidResult(int count)
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = RecipeDataForTesting(connection, count);

            // Act
            var res = sut.GetByID(count - 1);
            connection.Close();

            // Assert
            Assert.Null(res);
        }

        [Fact]
        public void Query_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyRecipeRepositoryInstanceNoData(connection);

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
            var sut = RecipeDataForTesting(connection, count);

            // Act
            var res = sut.Query(new RecipeQueryModel());
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
            var sut = RecipeDataForTesting(connection, 1);

            // Act
            var res = sut.Query(new RecipeQueryModel() { Id = 1 });
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
            var sut = RecipeDataForTesting(connection, 1);

            // Act
            var res = sut.Query(new RecipeQueryModel() { Id = id });
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
            var sut = CreateOnlyRecipeRepositoryInstanceNoData(connection);

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
            var sut = RecipeDataForTesting(connection, 1);

            var recipe = new Recipe()
            {
                Id = 2,
                MainCourseId = 1,
                RestaurantId = 1,
                SideDish = new List<Food>() { new Food() { Id = 1 } }
            };

            // Act
            var res = sut.Insert(recipe);
            connection.Close();

            // Assert
            Assert.Equal(recipe.Id, res.Id);
            Assert.Equal(recipe.RestaurantId, res.RestaurantId);
            Assert.Equal(recipe.MainCourseId, res.MainCourseId);
        }

        [Fact]
        public void Update_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = CreateOnlyRecipeRepositoryInstanceNoData(connection);

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
            var sut = RecipeDataForTesting(connection, 2);

            var updatedRecipe = new Recipe
            {
                Id = 1,
                MainCourseId = 2,
                RestaurantId = 2,
                SideDish = new List<Food> { new Food { Id = 1 }, new Food { Id = 2 } }
            };

            // Act
            var res = sut.Update(updatedRecipe);
            connection.Close();

            // Assert
            Assert.Equal(updatedRecipe.Id, res.Id);
            Assert.Equal(updatedRecipe.MainCourseId, res.MainCourseId);
            Assert.Equal(updatedRecipe.RestaurantId, res.RestaurantId);
            Assert.Equal(updatedRecipe.SideDish.Count, res.SideDish.Count);
        }

        [Fact]
        public void Delete_ExistingRecordIdPassed_ObjectDeletedFromDatabase()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = RecipeDataForTesting(connection, 1);
            const int existingId = 1;

            Assert.NotNull(sut.GetByID(existingId));

            // Act
            sut.Delete(existingId);

            // Assert
            Assert.Null(sut.GetByID(existingId));
            connection.Close();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(50)]
        public void Get_ValidId_ValidResult(int count)
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = RecipeDataForTesting(connection, count);

            // Act
            var res = sut.Get(0, int.MaxValue);
            connection.Close();

            // Assert
            Assert.NotNull(res);
            Assert.Equal(count, res.Count);
        }
    }
}
