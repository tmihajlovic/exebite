using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Test.InMemoryDB;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;
using Unity.Resolution;

namespace Exebite.DataAccess.Test.Tests
{
    [TestClass]
    public class LocationRepositoryTest
    {
        private static IFoodOrderingContextFactory _factory;
        private static ILocationRepository _locationRepository;
        private static IUnityContainer _container;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _container = new UnityContainer();
            Unity.UnityConfig.RegisterTypes(_container);
            _locationRepository = _container.Resolve<ILocationRepository>(new ParameterOverride("factory", _factory));
            InMemorySeed.Seed(_factory);
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
    }
}
