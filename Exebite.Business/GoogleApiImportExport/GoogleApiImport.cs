using System.Collections.Generic;
using System.Linq;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.Model;

namespace Exebite.Business.GoogleApiImportExport
{
    public class GoogleApiImport : IGoogleDataImporter
    {
        private IRestaurantService _restarauntService;
        private IFoodService _foodService;

        // conectors
        private ILipaConector _lipaConector;
        private IHedoneConector _hedoneConector;
        private ITeglasConector _teglasConector;

        public GoogleApiImport(IRestaurantService restarauntService, IFoodService foodService, ILipaConector lipaConector, ITeglasConector teglasConector, IHedoneConector hedoneConector)
        {
            _restarauntService = restarauntService;
            _foodService = foodService;

            // conectors to a new sheets
            _lipaConector = lipaConector;
            _hedoneConector = hedoneConector;
            _teglasConector = teglasConector;
        }

        /// <summary>
        /// Update daily menu for restorants
        /// </summary>
        public void UpdateRestorauntsMenu()
        {
            Restaurant lipaRestoraunt = _restarauntService.GetRestaurantByName("Restoran pod Lipom");
            Restaurant hedoneRestoraunt = _restarauntService.GetRestaurantByName("Hedone");
            Restaurant teglasRestoraunt = _restarauntService.GetRestaurantByName("Teglas");

            // Get food from sheet, update database for new and changed and update daily menu

            // Lipa
            // Check if all food exist in DB
            AddAndUpdateFood(lipaRestoraunt, _lipaConector);

            // Update daily menu
            lipaRestoraunt.DailyMenu = FoodsFromDB(lipaRestoraunt, _lipaConector.GetDailyMenu());
            _restarauntService.UpdateRestourant(lipaRestoraunt);

            // Teglas
            // Check if all food exist in DB
            AddAndUpdateFood(teglasRestoraunt, _teglasConector);

            // Update daily menu
            teglasRestoraunt.DailyMenu = FoodsFromDB(teglasRestoraunt, _teglasConector.GetDailyMenu());
            _restarauntService.UpdateRestourant(teglasRestoraunt);

            // Hedone
            // Check if all food exist in DB
            AddAndUpdateFood(hedoneRestoraunt, _hedoneConector);

            // Update daily menu
            hedoneRestoraunt.DailyMenu = FoodsFromDB(hedoneRestoraunt, _hedoneConector.GetDailyMenu());
            _restarauntService.UpdateRestourant(hedoneRestoraunt);
        }

        /// <summary>
        /// Updates <see cref="Food"/> info in database, if food doesn't exist creates new one
        /// </summary>
        /// <param name="restaurant">Restaurant food is loaded from</param>
        /// <param name="conector">Restaurant conector</param>
        private void AddAndUpdateFood(Restaurant restaurant, IRestaurantConector conector)
        {
            var sheetFoods = conector.LoadAllFoods();
            foreach (var food in sheetFoods)
            {
                var dbFood = restaurant.Foods.SingleOrDefault(f => f.Name == food.Name);
                if (dbFood != null)
                {
                    dbFood.Price = food.Price;
                    dbFood.Description = food.Description;
                    _foodService.UpdateFood(dbFood);
                }
                else
                {
                    food.Restaurant = restaurant;
                    _foodService.CreateNewFood(food);
                }
            }

            foreach (var food in restaurant.Foods)
            {
                var sheetFood = sheetFoods.SingleOrDefault(f => f.Name == food.Name);
                if (sheetFood == null)
                {
                    food.IsInactive = true;
                    _foodService.UpdateFood(food);
                }
            }
        }

        /// <summary>
        /// Finds <see cref="Food"/> in database based on sheet data
        /// </summary>
        /// <param name="restaurant">Restaurant to get food for</param>
        /// <param name="foods">Food list from sheet</param>
        /// <returns>Food list from database</returns>
        private List<Food> FoodsFromDB(Restaurant restaurant, List<Food> foods)
        {
            List<Food> result = new List<Food>();
            List<Food> dbFood = _foodService.GetAllFoods().Where(f => f.Restaurant.Id == restaurant.Id).ToList();
            foreach (var food in foods)
            {
                result.Add(dbFood.First(f => f.Name == food.Name));
            }

            return result;
        }
    }
}
