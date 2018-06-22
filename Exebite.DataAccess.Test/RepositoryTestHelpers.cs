using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.Mocks;
using Exebite.DomainModel;
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

        internal static CustomerRepository FillCustomerDataForTesing(string name, IEnumerable<CustomerEntity> customers)
        {
            var factory = new InMemoryDBFactory(name);

            using (var context = factory.Create())
            {
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            return new CustomerRepository(factory, _mapper, new Mock<ILogger<CustomerRepository>>().Object);
        }

        internal static IEnumerable<CustomerEntity> CreateCustomerEntities(int startId, int numberOfCustomers)
        {
            return Enumerable.Range(startId, numberOfCustomers)
                .Select(x => new CustomerEntity()
                {
                    //Id = x,
                    Balance = x,
                    AppUserId = (1000 + x).ToString(),
                    LocationId = x,
                    Name = $"Name {x}"
                });
        }

        internal static IEnumerable<Customer> CreateCustomers(int startId, int numberOfCustomers)
        {
            return Enumerable.Range(startId, numberOfCustomers)
                            .Select(x => new Customer()
                            {
                                Id = x,
                                Balance = x,
                                AppUserId = (1000 + x).ToString(),
                                LocationId = x,
                                Name = $"Name {x}"
                            });
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

        internal static CustomerAliasRepository CustomerAliasesDataForTesing(string name, int numberOfLocations)
        {
            var factory = new InMemoryDBFactory(name);

            using (var context = factory.Create())
            {
                var customerAlias = Enumerable.Range(1, numberOfLocations).Select(x => new CustomerAliasesEntities
                {
                    Id = x,
                    Alias = $"Alias {x}",
                    CustomerId = x,
                    RestaurantId = x
                });
                context.CustomerAliases.AddRange(customerAlias);

                var restaurant = Enumerable.Range(1, numberOfLocations).Select(x => new RestaurantEntity
                {
                    Id = x,
                    Name = $"Name {x}"
                });
                context.Restaurants.AddRange(restaurant);

                var customers = Enumerable.Range(1, numberOfLocations).Select(x => new CustomerEntity
                {
                    Id = x,
                    Name = $"Name {x}"
                });
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            return new CustomerAliasRepository(factory, _mapper, new Mock<ILogger<CustomerAliasRepository>>().Object);
        }

        internal static CustomerAliasRepository CreateOnlyCustomerAliasRepositoryInstanceNoData(string name)
        {
            return new CustomerAliasRepository(new InMemoryDBFactory(name), _mapper, new Mock<ILogger<CustomerAliasRepository>>().Object);
        }
    }
}
