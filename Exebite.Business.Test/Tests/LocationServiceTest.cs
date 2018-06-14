using System;
using System.Linq;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class LocationServiceTest
    {
        private static ILocationRepository _locationRepository;
        private static IFoodOrderingContextFactory _factory;

        [TestInitialize]
        public void Init()
        {
            var container = ServiceProviderWrapper.GetContainer();
            _factory = container.Resolve<IFoodOrderingContextFactory>();
            InMemoryDBSeed.Seed(_factory);

            _locationRepository = container.Resolve<ILocationRepository>();
        }

        [TestMethod]
        public void GetAllLocations()
        {
            var result = _locationRepository.Get(0, int.MaxValue);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationById()
        {
            const int id = 1;
            var result = _locationRepository.GetByID(id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationById_NonExisting()
        {
            var result = _locationRepository.GetByID(0);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateNewLocation()
        {
            Location newLocation = new Location
            {
                Name = "New location",
                Address = "New location address"
            };
            var result = _locationRepository.Insert(newLocation);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNewLocaiotn_IsNull()
        {
            _locationRepository.Insert(null);
        }

        [TestMethod]
        public void UpdateLocation()
        {
            const int id = 1;
            const string newName = "New name";
            const string newAdress = "New address";
            var location = _locationRepository.GetByID(id);
            location.Name = newName;
            location.Address = newAdress;
            var result = _locationRepository.Update(location);
            Assert.AreEqual(result.Id, id);
            Assert.AreEqual(result.Name, newName);
            Assert.AreEqual(result.Address, newAdress);
        }

        [TestMethod]
        public void DeleteLocation()
        {
            const string name = "For delete";
            var location = _locationRepository.Get(0, int.MaxValue).FirstOrDefault(x => x.Name == name);
            _locationRepository.Delete(location.Id);
            var result = _locationRepository.GetByID(location.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteLocation_NonExisting()
        {
            _locationRepository.Delete(0);
        }
    }
}
