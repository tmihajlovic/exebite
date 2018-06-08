using System;
using System.Linq;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.InMemoryDB;
using Exebite.Model;
using Exebite.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.DataAccess.Test.Tests
{
    [TestClass]
    public class LocationRepositoryTest
    {
        private static IFoodOrderingContextFactory _factory;
        private static ILocationRepository _locationRepository;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            var container = ServiceProviderWrapper.GetContainer();
            _factory = container.Resolve<IFoodOrderingContextFactory>();
            InMemorySeed.Seed(_factory);

            _locationRepository = container.Resolve<ILocationRepository>();
        }

        [TestMethod]
        public void GetAllLocations()
        {
            var result = _locationRepository.GetAll().ToList();
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetLocationById()
        {
            var result = _locationRepository.GetByID(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationById_NonExisting()
        {
            var result = _locationRepository.GetByID(0);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void InsertLocation()
        {
            var newLocation = new Location()
            {
                Name = "NewLocation",
                Address = "New Adress"
            };
            var result = _locationRepository.Insert(newLocation);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertLocation_IsNull()
        {
            _locationRepository.Insert(null);
        }

        [TestMethod]
        public void UpdateLocation()
        {
            using (var context = _factory.Create())
            {
                var location = AutoMapperHelper.Instance.GetMappedValue<Location>(context.Locations.Find(1), context);
                location.Name = "UpdatedName";
                location.Address = "UpdatedAddress";
                var result = _locationRepository.Update(location);
                Assert.AreEqual(result.Name, location.Name);
                Assert.AreEqual(result.Address, location.Address);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateLocation_IsNull()
        {
            _locationRepository.Update(null);
        }

        [TestMethod]
        public void DeleteLocation()
        {
            using (var context = _factory.Create())
            {
                var location = AutoMapperHelper.Instance.GetMappedValue<Location>(context.Locations.First(l => l.Name == "For delete"), context);
                _locationRepository.Delete(location.Id);
                var result = _locationRepository.GetByID(location.Id);
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void DeleteLocation_NonExisting()
        {
            _locationRepository.Delete(0);
        }
    }
}
