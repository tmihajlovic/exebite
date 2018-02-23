using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.GoogleSpreadsheetApi;
using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.GoogleSpreadsheetApi.Strategies;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace GoogleSpreadsheetApi.Test
{
    [TestClass]
    public class IndexHouseStrategyTest
    {
        static IRestaurantStrategy _strategy;
        static IGoogleSheetServiceFactory _ssFactory;
        static IGoogleSpreadsheetIdFactory _idFactory;
        static IUnityContainer _container;

        static List<Food> _menuData = new List<Food>();
        static List<Order> _orderData = new List<Order>();

        List<Food> _dailyMenu = new List<Food>();
        List<Order> _orders = new List<Order>();

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _container = new UnityContainer();
            UnityConfig.RegisterTypes(_container);

            _ssFactory = _container.Resolve<IGoogleSheetServiceFactory>();
            _idFactory = _container.Resolve<IGoogleSpreadsheetIdFactory>();
            _strategy = new IndexHouseStrategy(_ssFactory, _idFactory);
            _menuData = _strategy.GetDailyMenu();
            _orderData = _strategy.GetHistoricalData();
        }


        [TestInitialize]
        public void Setup()
        {
            _dailyMenu.Clear();
            _dailyMenu.AddRange(_menuData);

            _orders.Clear();
            _orders.AddRange(_orderData);
        }

        [TestMethod]
        public void GetDailyMenu_Food_Exists()
        {
            Assert.IsTrue(_dailyMenu.Any(f => f.Name == "velika čili piletina" && f.Price == 300 && f.Type == Exebite.Model.FoodType.MAIN_COURSE));
        }

        [TestMethod]
        public void GetDailyMenu_Food2_Exists()
        {
            Assert.IsTrue(_dailyMenu.Any(f => f.Name == "šunka" && f.Price == 160 && f.Type == Exebite.Model.FoodType.MAIN_COURSE));
        }

        [TestMethod]
        public void GetDailyMenu_Food3_Exists()
        {
            Assert.IsTrue(_dailyMenu.Any(f => f.Name == "pomfrit salata" && f.Price == 100 && f.Type == Exebite.Model.FoodType.MAIN_COURSE));
        }

        [TestMethod]
        public void GetDailyMenu_Food4_Exists()
        {
            Assert.IsTrue(_dailyMenu.Any(f => f.Name == "tartar" && f.Price == 0 && f.Type == Exebite.Model.FoodType.CONDIMENTS));
        }

        [TestMethod]
        public void GetHistoricalData_Order_Exists()
        {
            DateTime expectedDate = new DateTime(2017, 11, 23);

            string userName = "Dunja Vuić ";
            string food = "mali vegeterijanski sendvič";
            string note = "pavlaka, urnebes, zelena salata, pardajz, kukuruz, MALO senfa, bosiljak";
            
            Assert.IsTrue(_orders.Where(o => o.Date == expectedDate && o.Customer.Name == userName && o.Meal.Foods.Any(f => f.Name == food) && o.Note == note).ToList().Count > 0);
        }

        [TestMethod]
        public void GetHistoricalData_Order2_Exists()
        {
            DateTime expectedDate = new DateTime(2017, 12, 14);

            string userName = "Vesna Radulović JD";
            string food = "salata sa piletinom";
            //maslinovo ulje, balsamiko, so i biber
            Assert.IsTrue(_orders.Where(o => o.Date == expectedDate && o.Customer.Name == userName
                            && o.Meal.Foods.Any(f => f.Name == food) 
                            && o.Meal.Foods.Any(f=>f.Name == "maslinovo ulje")
                            && o.Meal.Foods.Any(f => f.Name == "aceto balsamiko")
                            && o.Meal.Foods.Any(f => f.Name == "so")).ToList().Count > 0);
        }

        [TestMethod]
        public void GetHistoricalData_Order3_Exists()
        {
            DateTime expectedDate = new DateTime(2018, 1, 8);

            string userName = "Stefan Ugrinov";
            string food = "velika dimljena piletina";
            //maslinovo ulje, balsamiko, so i biber
            Assert.IsTrue(_orders.Where(o => o.Date == expectedDate && o.Customer.Name == userName
                            && o.Meal.Foods.Any(f => f.Name == food)
                            && o.Meal.Foods.Any(f => f.Name == "kečap")
                            && o.Meal.Foods.Any(f => f.Name == "pavlaka")
                            && o.Meal.Foods.Any(f => f.Name == "tartar")
                            && o.Meal.Foods.Any(f => f.Name == "urnebes")).ToList().Count > 0);
        }


        [TestMethod]
        public void GetHistoricalData_Order4_Exists()
        {
            DateTime expectedDate = new DateTime(2018, 1, 10);

            string userName = "Zlatko Radojčić ";
            string food = "chicken nuggets velika porcija";
            
            Assert.IsTrue(_orders.Where(o => o.Date == expectedDate && o.Customer.Name == userName && o.Meal.Foods.Any(f=>f.Name == food)).ToList().Count > 0);
        }

    }
}
