using System;
using Exebite.DomainModel;
using Microsoft.Data.Sqlite;
using Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class FoodRepositoryTest
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
            var sut = FoodDataForTesting(connection, count);

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
            var sut = FoodDataForTesting(connection, count);

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
            var sut = CreateOnlyFoodRepositoryInstanceNoData(connection);

            // Act and Assert
            Exception res = Assert.Throws<ArgumentNullException>(() => sut.Query(null));
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
            var sut = FoodDataForTesting(connection, count);

            // Act
            var res = sut.Query(new FoodQueryModel());
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
            var sut = FoodDataForTesting(connection, 1);

            // Act
            var res = sut.Query(new FoodQueryModel() { Id = 1 });
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
            var sut = FoodDataForTesting(connection, 1);

            // Act
            var res = sut.Query(new FoodQueryModel() { Id = id });
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
            var sut = CreateOnlyFoodRepositoryInstanceNoData(connection);

            // Act and Assert
            Exception res = Assert.Throws<ArgumentNullException>(() => sut.Insert(null));
            connection.Close();
        }

        [Fact]
        public void Insert_ValidObjectPassed_ObjectSavedInDatabase()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = FoodDataForTesting(connection, 1);

            var food = new Food
            {
                Id = 2,
                Name = "Location name",
                Type = FoodType.MAIN_COURSE,
                Description = "Description",
                Price = 0.55m,
                RestaurantId = 1
            };

            // Act
            var res = sut.Insert(food);
            connection.Close();

            // Assert
            Assert.Equal(food.Id, res.Id);
            Assert.Equal(food.Name, res.Name);
            Assert.Equal(food.Type, res.Type);
            Assert.Equal(food.Description, res.Description);
            Assert.Equal(food.Price, res.Price);
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

        [Fact]
        public void Update_ValidObjectPassed_ObjectUpdatedInDatabase()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = FoodDataForTesting(connection, 2);

            var updatedFood = new Food
            {
                Id = 1,
                Name = "Location name updated",
                Price = 0.55m,
                Description = "Description updated",
                IsInactive = false,
                Type = FoodType.MAIN_COURSE,
                RestaurantId = 2
            };

            // Act
            var res = sut.Update(updatedFood);
            connection.Close();

            // Assert
            Assert.Equal(updatedFood.Id, res.Id);
            Assert.Equal(updatedFood.Name, res.Name);
            Assert.Equal(updatedFood.Price, res.Price);
            Assert.Equal(updatedFood.Description, res.Description);
            Assert.Equal(updatedFood.IsInactive, res.IsInactive);
            Assert.Equal(updatedFood.Type, res.Type);
            Assert.Equal(updatedFood.RestaurantId, res.RestaurantId);
        }

        [Fact]
        public void Delete_ExistingRecordIdPassed_ObjectDeletedFromDatabase()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var sut = FoodDataForTesting(connection, 1);
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
            var sut = FoodDataForTesting(connection, count);

            // Act
            var res = sut.Get(0, int.MaxValue);
            connection.Close();

            // Assert
            Assert.NotNull(res);
            Assert.Equal(count, res.Count);
        }
    }
}
