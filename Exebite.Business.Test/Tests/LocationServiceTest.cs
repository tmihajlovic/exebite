﻿using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class LocationServiceTest
    {
        private static ILocationService _locationService;
        private static IFoodOrderingContextFactory _factory;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _locationService = new LocationService(new LocationRepository(_factory));
            InMemoryDBSeed.Seed(_factory);
        }

        [TestMethod]
        public void GetAllLocations()
        {
            var result = _locationService.GetAllLocations();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationById()
        {
            var id = 1;
            var result = _locationService.GetLocationById(id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationByName()
        {
            var name = "Bulevar";
            var result = _locationService.GetLocationByName(name);
            Assert.AreEqual(result.Name, name);
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
        public void UpdateLocation()
        {
            var id = 1;
            var newName = "New name";
            var newAdress = "New adress";
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
            var name = "For delete";
            var location = _locationService.GetLocationByName(name);
            _locationService.DeleteLocation(location.Id);
            var result = _locationService.GetLocationById(location.Id);
            Assert.IsNull(result);
        }
    }
}
