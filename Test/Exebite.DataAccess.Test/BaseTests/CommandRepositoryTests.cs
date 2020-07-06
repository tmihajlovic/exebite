using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.Mocks;
using Microsoft.Data.Sqlite;
using Optional.Xunit;
using Xunit;

namespace Exebite.DataAccess.Test.BaseTests
{
    public abstract class CommandRepositoryTests<TModel, TId, TInput, TUpdate>
    {
        private readonly IMealOrderingContextFactory _factory;
        private readonly SqliteConnection _connection;

        protected CommandRepositoryTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            _factory = new InMemoryDBFactory(_connection);
        }

        protected abstract IEnumerable<TModel> SampleData { get; }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(17)]
        public void Insert_ObjectAdded_IdBecomesPositiveAfterSave(int count)
        {
            // Arrange
            IEnumerable<TModel> data = this.SampleData.Take(count + 1).ToList();
            this.InitializeStorage(_factory, count);
            TModel newObj = data.ElementAt(count);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(_factory);

            // Act
            var insertedId = repo.Insert(this.ConvertToInput(newObj));

            // Assert
            EAssert.IsRight(insertedId);
            Assert.True(this.GetId(insertedId) as long? > 0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void Delete_InsertThenDelete_DeleteWasSuccessfulExecutedReturnedTrue(int initialCount)
        {
            // Arrange
            IEnumerable<TModel> data = this.SampleData.Take(initialCount + 1).ToList();
            this.InitializeStorage(_factory, initialCount);
            TModel newObj = data.ElementAt(initialCount);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(_factory);
            var insertedRecordId = repo.Insert(this.ConvertToInput(newObj));

            // Act
            var result = repo.Delete(insertedRecordId.RightContent());

            // Assert
            EAssert.IsRight(result);
            Assert.True(result.RightContent());
        }

        [Fact]
        public void Insert_UnexpectedErrorOccur_ErrorReturned()
        {
            // Arrange
            IEnumerable<TModel> data = this.SampleData.Take(2).ToList();
            this.InitializeStorage(_factory, 1);
            TModel newObj = data.ElementAt(0);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(_factory);

            // Act
            var res = repo.Insert(this.ConvertToInvalidInput(newObj));

            // Assert
            EAssert.IsLeft(res);
        }

        [Fact]
        public void Delete_DeleteAlreadyDeletedRecord_RecordIsNotDeletedFalseReturned()
        {
            // Arrange
            IEnumerable<TModel> data = this.SampleData.Take(1).ToList();
            this.InitializeStorage(_factory, 0);
            TModel newObj = data.ElementAt(0);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(_factory);
            var insertedRecordId = repo.Insert(this.ConvertToInput(newObj));
            repo.Delete(insertedRecordId.RightContent());

            // Act
            var result = repo.Delete(insertedRecordId.RightContent());

            // Assert
            EAssert.IsLeft(result);
            Assert.Equal(typeof(RecordNotFound), result.LeftContent().GetType());
        }

        [Fact]
        public void Delete_UnExpectedErrorOccurs_ErrorReturned()
        {
            // Arrange
            IEnumerable<TModel> data = this.SampleData.Take(2).ToList();
            this.InitializeStorage(_factory, 0);
            TModel insertObject = data.ElementAt(1);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(_factory);
            var insertedRecordId = repo.Insert(this.ConvertToInput(insertObject));

            // this should make update to throw unexpected error
            _connection.Close();

            // Act
            var result = repo.Delete(insertedRecordId.RightContent());

            // Assert
            EAssert.IsLeft(result);
            Assert.Equal(typeof(UnknownError), result.LeftContent().GetType());
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 16)]
        public void Update_ExistingObjectModified_UpdateDoneSuccessfullTrueReturned(int storageCount, int targetIndex)
        {
            // Arrange
            IEnumerable<TModel> data = this.SampleData.Take(storageCount + 1).ToList();
            this.InitializeStorage(_factory, storageCount);
            TModel insertObject = data.ElementAt(storageCount);
            TModel updateObject = data.ElementAt(targetIndex + 1);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(_factory);
            var insertedRecordId = repo.Insert(this.ConvertToInput(insertObject));

            // Act
            var result = repo.Update(insertedRecordId.RightContent(), this.ConvertToUpdate(updateObject));

            // Assert
            EAssert.IsRight(result);
            Assert.True(result.RightContent());
        }

        [Fact]
        public void Update_InvalidUpdateObject_ArgumentNotSetErrorReturned()
        {
            // Arrange
            IEnumerable<TModel> data = this.SampleData.Take(2).ToList();
            this.InitializeStorage(_factory, 0);
            TModel insertObject = data.ElementAt(1);
            TModel updateObject = data.ElementAt(1);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(_factory);
            var insertedRecordId = repo.Insert(this.ConvertToInput(insertObject));

            // Act
            var result = repo.Update(insertedRecordId.RightContent(), this.ConvertToInvalidUpdate(updateObject));

            // Assert
            EAssert.IsLeft(result);
            Assert.Equal(typeof(ArgumentNotSet), result.LeftContent().GetType());
        }

        [Fact]
        public void Update_UnExistingObjectUpdate_ErrorReturned()
        {
            // Arrange
            IEnumerable<TModel> data = this.SampleData.Take(4).ToList();
            this.InitializeStorage(_factory, 0);
            TModel updateObject = data.ElementAt(3);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(_factory);

            // Act
            var result = repo.Update(this.GetUnExistingId(), this.ConvertToUpdate(updateObject));

            // Assert
            EAssert.IsLeft(result);
            Assert.Equal(typeof(RecordNotFound), result.LeftContent().GetType());
        }

        [Fact]
        public void Update_UnExpectedErrorOccurs_ErrorReturned()
        {
            // Arrange
            IEnumerable<TModel> data = this.SampleData.Take(2).ToList();
            this.InitializeStorage(_factory, 0);
            TModel insertObject = data.ElementAt(1);
            TModel updateObject = data.ElementAt(1);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(_factory);
            var insertedRecordId = repo.Insert(this.ConvertToInput(insertObject));

            // this should make update to throw unexpected error
            _connection.Close();

            // Act
            var result = repo.Update(insertedRecordId.RightContent(), this.ConvertToUpdate(updateObject));

            // Assert
            EAssert.IsLeft(result);
            Assert.Equal(typeof(UnknownError), result.LeftContent().GetType());
        }

        protected abstract IDatabaseCommandRepository<TId, TInput, TUpdate> CreateSut(IMealOrderingContextFactory factory);

        protected abstract void InitializeStorage(IMealOrderingContextFactory factory, int count);

        protected abstract TInput ConvertToInput(TModel data);

        protected abstract TInput ConvertToInvalidInput(TModel data);

        protected abstract TUpdate ConvertToInvalidUpdate(TModel data);

        protected abstract TUpdate ConvertToUpdate(TModel data);

        protected abstract TId GetId(Either<Error, TId> newObj);

        protected abstract TId GetUnExistingId();
    }
}