using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Optional.Xunit;
using Xunit;

namespace Exebite.DataAccess.Test
{
    public abstract class CommandRepositoryTests<TModel, TId, TInput, TUpdate>
    {
        protected abstract IEnumerable<TModel> SampleData { get; }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(17)]
        public void Insert_ObjectAdded_IdBecomesPositiveAfterSave(int count)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            IEnumerable<TModel> data = this.SampleData.Take(count + 1).ToList();
            this.InitializeStorage(connection, count);
            TModel newObj = data.ElementAt(count);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(connection);
            var res = repo.Insert(this.ConvertToInput(newObj));

            connection.Close();
            EAssert.IsRight(res);
            Assert.True(this.GetId(res) > 0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void Delete_InsertThenDelete_DeleteWasSuccessfulExecutedReturnedTrue(int initialCount)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            IEnumerable<TModel> data = this.SampleData.Take(initialCount + 1).ToList();
            this.InitializeStorage(connection, initialCount);
            TModel newObj = data.ElementAt(initialCount);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(connection);
            var insertedRecordId = repo.Insert(this.ConvertToInput(newObj));
            var result = repo.Delete(insertedRecordId.RightContent());

            connection.Close();
            EAssert.IsRight(result);
            Assert.True(result.RightContent());
        }

        [Fact]
        public void Delete_DeleteAlreadyDeletedRecord_RecordIsNotDeletedFalseReturned()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            IEnumerable<TModel> data = this.SampleData.Take(1).ToList();
            this.InitializeStorage(connection, 0);
            TModel newObj = data.ElementAt(0);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(connection);
            var insertedRecordId = repo.Insert(this.ConvertToInput(newObj));
            repo.Delete(insertedRecordId.RightContent());
            var result = repo.Delete(insertedRecordId.RightContent());

            connection.Close();
            EAssert.IsRight(result);
            Assert.False(result.RightContent());
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 3)]
        [InlineData(17, 16)]
        public void Update_ExistingObjectModified_UpdateDoneSuccessfullTrueReturned(int storageCount, int targetIndex)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            IEnumerable<TModel> data = this.SampleData.Take(storageCount + 1).ToList();
            this.InitializeStorage(connection, storageCount);
            TModel insertObject = data.ElementAt(storageCount);
            TModel updateObject = data.ElementAt(targetIndex + 1);

            IDatabaseCommandRepository<TId, TInput, TUpdate> repo = this.CreateSut(connection);
            var insertedRecordId = repo.Insert(this.ConvertToInput(insertObject));

            var result = repo.Update(insertedRecordId.RightContent(), this.ConvertToUpdate(updateObject));

            connection.Close();
            EAssert.IsRight(result);
            Assert.True(result.RightContent());
        }

        protected abstract IDatabaseCommandRepository<TId, TInput, TUpdate> CreateSut(SqliteConnection connection);

        protected abstract void InitializeStorage(SqliteConnection connection, int count);

        protected abstract TInput ConvertToInput(TModel data);

        protected abstract TUpdate ConvertToUpdate(TModel data);

        protected abstract int GetId(Either<Error, TId> newObj);
    }
}