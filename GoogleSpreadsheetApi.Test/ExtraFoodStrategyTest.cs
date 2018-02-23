using Exebite.GoogleSpreadsheetApi;
using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.GoogleSpreadsheetApi.Strategies;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace GoogleSpreadsheetApi.Test
{
    [TestClass]
    public class ExtraFoodStrategyTest
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
            _strategy = new ExtraFoodStrategy(_ssFactory, _idFactory);
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
            Assert.IsTrue(_dailyMenu.Any(f => f.Name == "Svezi Kupus" && f.Type == Exebite.Model.FoodType.SALAD));
        }

        [TestMethod]
        public void GetDailyMenu_Food2_Exists()
        {
            Assert.IsTrue(_dailyMenu.Any(f => f.Name == "Pekarski krompir" && f.Type == Exebite.Model.FoodType.SIDE_DISH));
        }
        

        [TestMethod]
        public void GetHistoricalData_Order_Valid()
        {
            DateTime expectedDate = new DateTime(2017, 10, 09);
            string userName = "Dragana Srećkov";
            string foodName = "Dorucak salata - pileca";
            
            Assert.IsTrue(_orders.Where(o => o.Date == expectedDate && o.Customer.Name == userName && o.Meal.Foods.Any(f => f.Name == foodName)).ToList().Count > 0);
        }

        [TestMethod]
        public void GetHistoricalData_FoodByOrder_Exists()
        {
            DateTime expectedDate = new DateTime(2017, 10, 16);
            string userName = "Bojan Đurić";
            string foodName = "Grilovano povrce (sa pomfritom i rizom)";
            
            Assert.IsTrue(_orders.Where(o => o.Date == expectedDate && o.Customer.Name == userName && o.Meal.Foods.Any(f => f.Name == foodName)).ToList().Count > 0);
        }



        [TestMethod]
        public void GetHistoricalData_FoodByOrder2_Exists()
        {
            DateTime expectedDate = new DateTime(2017, 12, 13);
            string userName = "Anđelko Višekruna";
            string foodName = "Burger chesse (burger original+sir) 400 gr";
            
            Assert.IsTrue(_orders.Where(o => o.Date == expectedDate && o.Customer.Name == userName && o.Meal.Foods.Any(f => f.Name == foodName)).ToList().Count > 0);
        }

        [TestMethod]
        public void GetHistoricalData_FoodByOrder3_Exists()
        {
            DateTime expectedDate = new DateTime(2018, 1, 5);
            string userName = "Saša Jugurdžija";
            string foodName = "Przena slanina i jaja     ";
            
            Assert.IsTrue(_orders.Where(o => o.Date == expectedDate && o.Customer.Name == userName && o.Meal.Foods.Any(f => f.Name == foodName)).ToList().Count > 0);
        }
        
        [TestMethod]
        public void GetHistoricalData_DailyOrder2_Exists()
        {
            DateTime expectedDate = new DateTime(2018, 1, 25);
            string userName = "Aleksandar Kahriman";

            // Food names which are not matching the name of current
            string foodName = "Becka snicla (pilece meso 350.00din.sa prilogom)";
            string foodName2 = "1/2 Spanac + 1/2 pekarski krompir";
            string foodName3 = "Lenja pita sa jabukom";
            
            Assert.IsTrue(_orders.Where(o => o.Date == expectedDate && o.Customer.Name == userName && o.Meal.Foods.Any(f=>f.Name == foodName) && o.Meal.Foods.Any(f => f.Name == foodName2) && o.Meal.Foods.Any(f => f.Name == foodName3)).ToList().Count > 0);
        }

        [TestMethod]
        public void GetHistoricalData_DailyOrder3_Exists()
        {
            DateTime expectedDate = new DateTime(2018, 1, 19);
            string userName = "Viktor Šujić";

            // Food names which are not matching the name of current
            string foodName = "Kordon bleu (cena 380.00 din.sa prilogom po želji)";
            string foodName2 = "1/2 Spanac + 1/2 riza sa povrcem";
            string foodName3 = "Svezi Kupus";
            
            Assert.IsTrue(_orders.Where(o => o.Date == expectedDate && o.Customer.Name == userName && o.Meal.Foods.Any(f => f.Name == foodName) && o.Meal.Foods.Any(f => f.Name == foodName2) && o.Meal.Foods.Any(f => f.Name == foodName3)).ToList().Count > 0);
        }
    }
}
