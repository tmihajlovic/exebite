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
    public sealed class RoleQueryRepositoryTest : QueryRepositoryTests<RoleQueryRepositoryTest.Data, Role, RoleQueryModel>
    {
        protected override IEnumerable<Data> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(content => new Data
            {
                Id = content,
                Name = $"Name {content}"
            });

        protected override RoleQueryModel ConvertEmptyToQuery()
        {
            return new RoleQueryModel();
        }

        protected override RoleQueryModel ConvertNullToQuery()
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override RoleQueryModel ConvertToQuery(Data data)
        {
            return new RoleQueryModel { Id = data.Id };
        }

        protected override RoleQueryModel ConvertToQuery(int id)
        {
            return new RoleQueryModel { Id = id };
        }

        protected override RoleQueryModel ConvertWithPageAndSize(int page, int size)
        {
            return new RoleQueryModel(page, size);
        }

        protected override IDatabaseQueryRepository<Role, RoleQueryModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateRoleQueryRepositoryInstance(factory);
        }

        protected override int GetId(Role result)
        {
            return result.Id;
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
