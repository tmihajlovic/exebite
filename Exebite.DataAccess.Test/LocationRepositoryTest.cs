using System;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class LocationRepositoryTest
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(50, 2)]
        public void GetById_ValidId_ValidResult(int count, int id)
        {
            // Arrange
            var sut = LocationDataForTesing(Guid.NewGuid().ToString(), count);

            // Act
            var res = sut.GetByID(id);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(id, res.Id);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_InValidId_ValidResult(int count)
        {
            // Arrange
            var sut = LocationDataForTesing(Guid.NewGuid().ToString(), count);

            // Act
            var res = sut.GetByID(count - 1);

            // Assert
            Assert.Null(res);
        }

        [Fact]
        public void Query_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var sut = CreateOnlyLocationRepositoryInstanceNoData(Guid.NewGuid().ToString());

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => sut.Query(null));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        public void Query_MultipleElements(int count)
        {
            // Arrange
            var sut = LocationDataForTesing(Guid.NewGuid().ToString(), count);

            // Act
            var res = sut.Query(new LocationQueryModel());

            // Assert
            Assert.Equal(count, res.Count);
        }

        [Fact]
        public void Query_QueryByIDId_ValidId()
        {
            // Arrange
            var sut = LocationDataForTesing(Guid.NewGuid().ToString(), 1);

            // Act
            var res = sut.Query(new LocationQueryModel() { Id = 1 });

            Assert.Equal(1, res.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void Query_QueryByIDId_NonExistingID(int id)
        {
            // Arrange
            var sut = LocationDataForTesing(Guid.NewGuid().ToString(), 1);

            // Act
            var res = sut.Query(new LocationQueryModel() { Id = id });

            // Assert
            Assert.Equal(0, res.Count);
        }

        [Fact]
        public void Insert_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var sut = CreateOnlyLocationRepositoryInstanceNoData(Guid.NewGuid().ToString());

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => sut.Insert(null));
        }

        [Fact]
        public void Insert_ValidObjectPassed_ObjectSavedInDatabase()
        {
            // Arrange
            var sut = CreateOnlyLocationRepositoryInstanceNoData(Guid.NewGuid().ToString());

            var location = new Location
            {
                Id = 1,
                Name = "Location name",
                Address = "Location address"
            };

            // Act
            var res = sut.Insert(location);

            // Assert
            Assert.Equal(location.Id, res.Id);
            Assert.Equal(location.Name, res.Name);
            Assert.Equal(location.Address, res.Address);
        }

        [Fact]
        public void Update_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var sut = CreateOnlyLocationRepositoryInstanceNoData(Guid.NewGuid().ToString());

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => sut.Update(null));
        }

        [Fact]
        public void Update_ValidObjectPassed_ObjectUpdatedInDatabase()
        {
            // Arrange
            var sut = LocationDataForTesing(Guid.NewGuid().ToString(), 1);

            var updatedLocation = new Location
            {
                Id = 1,
                Name = "Location name updated",
                Address = "Location address updated"
            };

            // Act
            var res = sut.Update(updatedLocation);

            // Assert
            Assert.Equal(updatedLocation.Id, res.Id);
            Assert.Equal(updatedLocation.Name, res.Name);
            Assert.Equal(updatedLocation.Address, res.Address);
        }

        [Fact]
        public void Delete_ExistingRecordIdPassed_ObjectDeletedFromDatabase()
        {
            // Arrange
            var sut = LocationDataForTesing(Guid.NewGuid().ToString(), 1);
            const int existingId = 1;

            Assert.NotNull(sut.GetByID(existingId));

            // Act
            sut.Delete(existingId);

            // Assert
            Assert.Null(sut.GetByID(existingId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(50)]
        public void Get_ValidId_ValidResult(int count)
        {
            // Arrange
            var sut = LocationDataForTesing(Guid.NewGuid().ToString(), count);

            // Act
            var res = sut.Get(0, int.MaxValue);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(count, res.Count);
        }
    }
}
