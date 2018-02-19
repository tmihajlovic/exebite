using Exebite.Business;
using GoogleSpreadsheetApi.GoogleSSFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Resolution;

namespace Business.Test.GoogleApiImportExport
{
    [TestClass]
    public class GoogleApiImportTest
    {
        IGoogleSheetServiceFactory _googleSheetServiceFactory;
        IGoogleSpreadsheetIdFactory _googleSpreadsheetIdFactory;
        IGoogleDataImporter _googleDataImporter;
        IUnityContainer _container;
        
        [TestInitialize]
        public void Setup()
        {
            _container = new UnityContainer();
            UnityConfig.RegisterTypes(_container);

            _googleSheetServiceFactory = _container.Resolve<IGoogleSheetServiceFactory>();
            _googleSpreadsheetIdFactory = _container.Resolve<IGoogleSpreadsheetIdFactory>();

            _googleDataImporter = _container.Resolve<IGoogleDataImporter>(new ParameterOverrides
            {
                { "GoogleSSFactory", _googleSheetServiceFactory },
                { "GoogleSSIdFactory", _googleSpreadsheetIdFactory }
            }.OnType<GoogleApiImport>());
        }

        [TestMethod]
        public void GetHistoricalData()
        {
            var restult = _googleDataImporter.GetHistoricalData();
            Assert.AreNotEqual(restult.Count, 0);
        }
    }
}
