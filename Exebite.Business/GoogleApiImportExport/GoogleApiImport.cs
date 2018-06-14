﻿using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Repositories;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.Model;

namespace Exebite.Business.GoogleApiImportExport
{
    public class GoogleApiImport : IGoogleDataImporter
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IFoodRepository _foodRepository;

        // connectors
        private readonly ILipaConector _lipaConector;
        private readonly IHedoneConector _hedoneConector;
        private readonly ITeglasConector _teglasConector;

        public GoogleApiImport(IRestaurantRepository restaurantRepository, IFoodRepository foodRepository, ILipaConector lipaConector, ITeglasConector teglasConector, IHedoneConector hedoneConector)
        {
            _restaurantRepository = restaurantRepository;
            _foodRepository = foodRepository;

            // connectors to a new sheets
            _lipaConector = lipaConector;
            _hedoneConector = hedoneConector;
            _teglasConector = teglasConector;
        }

        /// <summary>
        /// Update daily menu for restaurants
        /// </summary>
        public void UpdateRestorauntsMenu()
        {
            Restaurant lipaRestaurant = _restaurantRepository.Query(new RestaurantQueryModel { Name = "Restoran pod Lipom" }).First();
            Restaurant hedoneRestaurant = _restaurantRepository.Query(new RestaurantQueryModel { Name = "Hedone" }).First();
            Restaurant teglasRestaurant = _restaurantRepository.Query(new RestaurantQueryModel { Name = "Teglas" }).First();

            // Get food from sheet, update database for new and changed and update daily menu

            // Lipa
            // Check if all food exist in DB
            AddAndUpdateFood(lipaRestaurant, _lipaConector);

            // Update daily menu
            lipaRestaurant.DailyMenu = FoodsFromDB(lipaRestaurant, _lipaConector.GetDailyMenu());
            _restaurantRepository.Update(lipaRestaurant);

            // Teglas
            // Check if all food exist in DB
            AddAndUpdateFood(teglasRestaurant, _teglasConector);

            // Update daily menu
            teglasRestaurant.DailyMenu = FoodsFromDB(teglasRestaurant, _teglasConector.GetDailyMenu());
            _restaurantRepository.Update(teglasRestaurant);

            // Hedone
            // Check if all food exist in DB
            AddAndUpdateFood(hedoneRestaurant, _hedoneConector);

            // Update daily menu
            hedoneRestaurant.DailyMenu = FoodsFromDB(hedoneRestaurant, _hedoneConector.GetDailyMenu());
            _restaurantRepository.Update(hedoneRestaurant);
        }

        /// <summary>
        /// Updates <see cref="Food"/> info in database, if food doesn't exist creates new one
        /// </summary>
        /// <param name="restaurant">Restaurant food is loaded from</param>
        /// <param name="connector">Restaurant connector</param>
        private void AddAndUpdateFood(Restaurant restaurant, IRestaurantConector connector)
        {
            var sheetFoods = connector.LoadAllFoods();
            foreach (var food in sheetFoods)
            {
                var dbFood = restaurant.Foods.SingleOrDefault(f => f.Name == food.Name);
                if (dbFood != null)
                {
                    dbFood.Price = food.Price;
                    dbFood.Description = food.Description;
                    _foodRepository.Update(dbFood);
                }
                else
                {
                    food.Restaurant = restaurant;
                    _foodRepository.Insert(food);
                }
            }

            foreach (var food in restaurant.Foods)
            {
                var sheetFood = sheetFoods.SingleOrDefault(f => f.Name == food.Name);
                if (sheetFood == null)
                {
                    food.IsInactive = true;
                    _foodRepository.Update(food);
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
            List<Food> dbFood = _foodRepository.Get(0, int.MaxValue).Where(f => f.Restaurant.Id == restaurant.Id).ToList();
            foreach (var food in foods)
            {
                result.Add(dbFood.First(f => f.Name == food.Name));
            }

            return result;
        }
    }
}
