using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.Mocks;

namespace Exebite.DataAccess.Test
{
    internal static class CustomerRepositoryTestHelpers
    {
        private static readonly IMapper _mapper;

        static CustomerRepositoryTestHelpers()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;
            Mapper.Initialize(cfg => cfg.AddProfile<DataAccessMappingProfile>());

            _mapper = Mapper.Instance;
        }

        internal static CustomerRepository CustomerDataForTesing(string name, int numberOfCustomers)
        {
            var factory = new InMemoryDBFactory(name);

            using (var context = factory.Create())
            {
                var customers = Enumerable.Range(1, numberOfCustomers).Select(x => new CustomerEntity()
                {
                    Id = x,
                    Balance = x,
                    AppUserId = (1000 + x).ToString(),
                    LocationId = x,
                    Name = $"Name {x}"
                });

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            return new CustomerRepository(factory, _mapper);
        }
    }
}
