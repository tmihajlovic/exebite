using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.Mocks;
using Microsoft.Extensions.Logging;
using Moq;

namespace Exebite.DataAccess.Test
{
    internal static class RepositoryTestHelpers
    {
        private static readonly IMapper _mapper;

        static RepositoryTestHelpers()
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

            return new CustomerRepository(factory, _mapper, new Mock<ILogger<CustomerRepository>>().Object);
        }

        internal static LocationRepository LocationDataForTesing(string name, int numberOfLocations)
        {
            var factory = new InMemoryDBFactory(name);

            using (var context = factory.Create())
            {
                var locations = Enumerable.Range(1, numberOfLocations).Select(x => new LocationEntity()
                {
                    Id = x,
                    Address = $"Address {x}",
                    Name = $"Name {x}"
                });

                context.Locations.AddRange(locations);
                context.SaveChanges();
            }

            return new LocationRepository(factory, _mapper, new Mock<ILogger<LocationRepository>>().Object);
        }

        internal static LocationRepository CreateOnlyLocationRepositoryInstanceNoData(string name)
        {
            return new LocationRepository(new InMemoryDBFactory(name), _mapper, new Mock<ILogger<LocationRepository>>().Object);
        }
    }
}
