using System;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Context;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class LocationServiceTest
    {
        private static ILocationService _locationService;
        private static IFoodOrderingContextFactory _factory;

        [TestInitialize]
        public void Init()
        {
            var container = ServiceProviderWrapper.GetContainer();
            _factory = container.Resolve<IFoodOrderingContextFactory>();
            InMemoryDBSeed.Seed(_factory);

            _locationService = container.Resolve<ILocationService>();
        }

        [TestMethod]
        public void GetAllLocations()
        {
            var result = _locationService.GetLocations(0, int.MaxValue);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationById()
        {
            const int id = 1;
            var result = _locationService.GetLocationById(id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationById_NonExisting()
        {
            var result = _locationService.GetLocationById(0);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetLocationByName()
        {
            const string name = "Bulevar";
            var result = _locationService.GetLocationByName(name);
            Assert.AreEqual(result.Name, name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLocationByName_StringEmpty()
        {
            _locationService.GetLocationByName(string.Empty);
        }

        [TestMethod]
        public void GetLocationByName_NonExisting()
        {
            const string name = "NonExistingLocaiotn";
            var result = _locationService.GetLocationByName(name);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateNewLocation()
        {
            Location newLocation = new Location
            {
                Name = "New location",
                Address = "New location adress"
            };
            var result = _locationService.CreateNewLocation(newLocation);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNewLocaiotn_IsNull()
        {
            _locationService.CreateNewLocation(null);
        }

        [TestMethod]
        public void UpdateLocation()
        {
            const int id = 1;
            const string newName = "New name";
            const string newAdress = "New adress";
            var location = _locationService.GetLocationById(id);
            location.Name = newName;
            location.Address = newAdress;
            var result = _locationService.UpdateLocation(location);
            Assert.AreEqual(result.Id, id);
            Assert.AreEqual(result.Name, newName);
            Assert.AreEqual(result.Address, newAdress);
        }

        [TestMethod]
        public void DeleteLocation()
        {
            const string name = "For delete";
            var location = _locationService.GetLocationByName(name);
            _locationService.DeleteLocation(location.Id);
            var result = _locationService.GetLocationById(location.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteLocation_NonExisting()
        {
            _locationService.DeleteLocation(0);
        }
    }
}
