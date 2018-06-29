using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.Mocks;
using Exebite.DomainModel;
using Microsoft.Data.Sqlite;
using Optional.Xunit;
using Xunit;

namespace Exebite.DataAccess.Test
{
    public abstract class QueryRepositoryTests<TModel, TResult, TQuery>
    {
        private readonly SqliteConnection _connection;
        private readonly IFoodOrderingContextFactory _factory;

        protected QueryRepositoryTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            _factory = new InMemoryDBFactory(_connection);
        }

        protected abstract IEnumerable<TModel> SampleData { get; }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(50, 2)]
        public void GetById_ValidId_ValidResult(int count, int id)
        {
            // Arrange
            IEnumerable<TModel> data = this.SampleData.Take(count + 1).ToList();
            this.InitializeStorage(_factory, count);
            TModel queryData = data.ElementAt(id);

            var sut = this.CreateSut(_factory);

            // Act
            var res = sut.Query(this.ConvertToQuery(queryData));

            // Assert
            EAssert.IsRight(res);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_InValidId_ValidResult(int count)
        {
            // Arrange
            IEnumerable<TModel> data = this.SampleData.Take(count + 2).ToList();
            this.InitializeStorage(_factory, count);
            TModel queryData = data.ElementAt(count + 1);

            var sut = this.CreateSut(_factory);

            // Act
            var res = sut.Query(this.ConvertToQuery(queryData));

            // Assert
            EAssert.IsRight(res);
            var result = res.RightContent();
            Assert.Empty(result.Items);
        }

        [Fact]
        public void Query_NullPassed_ArgumentNullExceptionThrown()
        {
            // Arrange
            var sut = this.CreateSut(_factory);

            // Act
            var res = sut.Query(this.ConvertNullToQuery());

            // Assert
            EAssert.IsLeft(res);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        public void Query_MultipleElements(int count)
        {
            // Arrange
            this.InitializeStorage(_factory, count);

            var sut = this.CreateSut(_factory);

            // Act
            var res = sut.Query(this.ConvertEmptyToQuery());

            // Assert
            EAssert.IsRight(res);
            var result = res.RightContent();
            Assert.Equal(count, result.Items.Count());
        }

        [Fact]
        public void Query_QueryByIDId_ValidId()
        {
            // Arrange
            const int validId = 1;
            IEnumerable<TModel> data = this.SampleData.Take(1).ToList();
            this.InitializeStorage(_factory, 1);
            TModel queryData = data.ElementAt(0);

            var sut = this.CreateSut(_factory);

            // Act
            var res = sut.Query(this.ConvertToQuery(queryData));

            // Assert
            EAssert.IsRight(res);
            var result = res.RightContent();
            Assert.Single(result.Items);
            var item = result.Items.FirstOrDefault();
            Assert.Equal(validId, this.GetId(item));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void Query_QueryByIDId_NonExistingID(int id)
        {
            // Arrange
            this.InitializeStorage(_factory, 1);

            var sut = this.CreateSut(_factory);

            // Act
            var res = sut.Query(this.ConvertToQuery(id));

            // Assert
            EAssert.IsRight(res);
            var result = res.RightContent();
            Assert.Empty(result.Items);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(50)]
        public void Query_ValidId_ValidResult(int count)
        {
            // Arrange
            IEnumerable<TModel> data = this.SampleData.Take(count + 1).ToList();
            this.InitializeStorage(_factory, count);
            TModel queryData = data.ElementAt(count);

            var sut = this.CreateSut(_factory);

            // Act
            var res = sut.Query(this.ConvertWithPageAndSize(1, QueryConstants.MaxElements));

            // Assert
            EAssert.IsRight(res);
            var result = res.RightContent();
            Assert.Equal(count, result.Total);
            Assert.Equal(count, result.Items.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(99)]
        public void Query_ValidId_ValidResultLimited(int count)
        {
            // Arrange
            this.InitializeStorage(_factory, QueryConstants.MaxElements + count);

            var sut = this.CreateSut(_factory);

            // Act
            var res = sut.Query(this.ConvertWithPageAndSize(1, QueryConstants.MaxElements + count));

            // Assert
            EAssert.IsRight(res);
            var result = res.RightContent();
            Assert.Equal(QueryConstants.MaxElements + count, result.Total);
            Assert.Equal(QueryConstants.MaxElements, result.Items.Count());
        }

        [Fact]
        public void Query_ErrorReturned()
        {
            // Arrange
            this.InitializeStorage(_factory, 20);

            var sut = this.CreateSut(_factory);

            // Act
            var res = sut.Query(this.ConvertNullToQuery());

            // Assert
            EAssert.IsLeft(res);
        }

        protected abstract IDatabaseQueryRepository<TResult, TQuery> CreateSut(IFoodOrderingContextFactory factory);

        protected abstract void InitializeStorage(IFoodOrderingContextFactory factory, int count);

        protected abstract int GetId(TResult result);

        protected abstract TQuery ConvertToQuery(TModel data);

        protected abstract TQuery ConvertToQuery(int id);

        protected abstract TQuery ConvertNullToQuery();

        protected abstract TQuery ConvertEmptyToQuery();

        protected abstract TQuery ConvertWithPageAndSize(int page, int size);
    }
}
