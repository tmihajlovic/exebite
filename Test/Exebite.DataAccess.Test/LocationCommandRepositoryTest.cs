using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.BaseTests;
using Optional.Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class LocationCommandRepositoryTest : CommandRepositoryTests<LocationCommandRepositoryTest.Data, long, LocationInsertModel, LocationUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(content => new Data
            {
                Id = content,
                Name = $"Name {content}",
                Address = $"Address {content}"
            });

        protected override LocationInsertModel ConvertToInput(Data data)
        {
            return new LocationInsertModel { Name = data.Name, Address = data.Address };
        }

        protected override LocationInsertModel ConvertToInvalidInput(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override LocationUpdateModel ConvertToInvalidUpdate(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override LocationUpdateModel ConvertToUpdate(Data data)
        {
            return new LocationUpdateModel { Name = data.Name, Address = data.Address };
        }

        protected override IDatabaseCommandRepository<long, LocationInsertModel, LocationUpdateModel> CreateSut(IMealOrderingContextFactory factory)
        {
            return CreateOnlyLocationCommandRepositoryInstanceNoData(factory);
        }

        protected override long GetId(Either<Error, long> newObj)
        {
            return newObj.RightContent();
        }

        protected override long GetUnExistingId()
        {
            return 99999;
        }

        protected override void InitializeStorage(IMealOrderingContextFactory factory, int count)
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
            public long? Id { get; set; }

            public string Name { get; set; }

            public string Address { get; set; }
        }
    }
}
