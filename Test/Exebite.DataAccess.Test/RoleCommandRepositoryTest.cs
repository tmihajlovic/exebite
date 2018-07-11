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
    public sealed class RoleCommandRepositoryTest : CommandRepositoryTests<RoleCommandRepositoryTest.Data, int, RoleInsertModel, RoleUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(content => new Data
            {
                Id = content,
                Name = $"Name {content}"
            });

        protected override RoleInsertModel ConvertToInput(Data data)
        {
            return new RoleInsertModel { Name = data.Name };
        }

        protected override RoleInsertModel ConvertToInvalidInput(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override RoleUpdateModel ConvertToInvalidUpdate(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override RoleUpdateModel ConvertToUpdate(Data data)
        {
            return new RoleUpdateModel { Name = data.Name };
        }

        protected override IDatabaseCommandRepository<int, RoleInsertModel, RoleUpdateModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateroleCommandRepositoryInstance(factory);
        }

        protected override int GetId(Either<Error, int> newObj)
        {
            return newObj.RightContent();
        }

        protected override int GetUnExistingId()
        {
            return 99999;
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var roles = Enumerable.Range(1, count).Select(x => new RoleEntity()
                {
                    Id = x,
                    Name = $"Name {x}"
                });

                context.Roles.AddRange(roles);
                context.SaveChanges();
            }
        }

        public sealed class Data
        {
            public int? Id { get; set; }

            public string Name { get; set; }
        }
    }
}
