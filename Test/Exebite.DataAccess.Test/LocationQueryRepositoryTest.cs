using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.BaseTests;
using Exebite.DomainModel;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class LocationQueryRepositoryTest : QueryRepositoryTests<LocationQueryRepositoryTest.Data, Location, LocationQueryModel>
    {
        protected override IEnumerable<Data> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(content => new Data
            {
                Id = content,
                Name = $"Name {content}",
                Address = $"Address {content}"
            });

        protected override LocationQueryModel ConvertEmptyToQuery()
        {
            return new LocationQueryModel();
        }

        protected override LocationQueryModel ConvertNullToQuery()
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override LocationQueryModel ConvertToQuery(Data data)
        {
            return new LocationQueryModel { Id = data.Id };
        }

        protected override LocationQueryModel ConvertToQuery(int id)
        {
            return new LocationQueryModel { Id = id };
        }

        protected override LocationQueryModel ConvertWithPageAndSize(int page, int size)
        {
            return new LocationQueryModel(page, size);
        }

        protected override IDatabaseQueryRepository<Location, LocationQueryModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateOnlyLocationQueryRepositoryInstanceNoData(factory);
        }

        protected override int GetId(Location result)
        {
            return result.Id;
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var locations = Enumerable.Range(1, count).Select(x => new LocationEntity()
                {
                    Id = x,
                    Name = $"Name {x}",
                    Address = $"Address {x}"
                });

                context.Location.AddRange(locations);
                context.SaveChanges();
            }
        }

        public sealed class Data
        {
            public int? Id { get; set; }

            public string Name { get; set; }

            public string Address { get; set; }
        }
    }
}
