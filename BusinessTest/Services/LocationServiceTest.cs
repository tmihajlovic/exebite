using Exebite.Business;
using Exebite.DataAccess;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTest.Services
{
    [TestClass]
    public class LocationServiceTest
    {
        static ILocationService _locationService;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            var _locationHandlerMock = new Moq.Mock<ILocationHandler>();
            _locationHandlerMock.Setup(f => f.Get()).Returns(FakeData.locationList);
            _locationHandlerMock.Setup(f => f.GetByID(1)).Returns(FakeData.location);
            _locationHandlerMock.Setup(f => f.GetByID(11)).Returns((Location)null);

            _locationService = new LocationService(_locationHandlerMock.Object);
        }

        [TestMethod]
        public void GetAllLocations()
        {
            var result = _locationService.GetAllLocations();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationById_Valid()
        {
            var result = _locationService.GetLocationById(1);
            Assert.AreEqual(result, FakeData.location);
        }

        [TestMethod]
        public void GetLocationById_Invalid()
        {
            var result = _locationService.GetLocationById(11);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetLocationByName_Valid()
        {
            var result = _locationService.GetLocationByName("TestLocation1");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationByName_Invalid()
        {
            var result = _locationService.GetLocationByName("InvalidName");
            Assert.IsNull(result);
        }
    }
}
