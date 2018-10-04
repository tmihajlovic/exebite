using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Either;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.Sheets.API;
using Exebite.Sheets.Common;

namespace Exebite.Business.GoogleApiImportExport
{
    public class GoogleApiImport : IGoogleDataImporter
    {
        private readonly IRestaurantQueryRepository _restaurantQueryRepository;
        private readonly IRestaurantCommandRepository _restaurantCommandRepository;
        private readonly IFoodQueryRepository _foodQueryRepository;
        private readonly IFoodCommandRepository _foodCommandRepository;

        private readonly IDailyMenuQueryRepository _dailyMenuQueryRepository;
        private readonly IMapper _mapper;

        private readonly ISheetsAPI _sheetsApi;

        public GoogleApiImport(
            IRestaurantQueryRepository restaurantQueryRepository,
            IRestaurantCommandRepository restaurantCommandRepository,
            ISheetsAPI sheetsApi,
            IDailyMenuQueryRepository dailyMenuQueryRepository,
            IMapper mapper,
            IFoodQueryRepository foodQueryRepository,
            IFoodCommandRepository foodCommandRepository)
        {
            _restaurantQueryRepository = restaurantQueryRepository;
            _restaurantCommandRepository = restaurantCommandRepository;
            _mapper = mapper;

            // connectors to a new sheets
            _sheetsApi = sheetsApi;
      
            _dailyMenuQueryRepository = dailyMenuQueryRepository;
            _foodQueryRepository = foodQueryRepository;
            _foodCommandRepository = foodCommandRepository;
        }

        /// <summary>
        /// Update daily menu for restaurants
        /// </summary>
        public void UpdateRestorauntsMenu()
        {
            Restaurant lipaRestaurant = _restaurantQueryRepository.Query(new RestaurantQueryModel { Name = Constants.POD_LIPOM_NAME })
                                                                  .Map(x => x.Items.First())
                                                                  .Reduce(_ => throw new System.Exception());
            Restaurant hedoneRestaurant = _restaurantQueryRepository.Query(new RestaurantQueryModel { Name = Constants.HEDONE_NAME })
                                                                    .Map(x => x.Items.First())
                                                                    .Reduce(_ => throw new System.Exception());
            Restaurant teglasRestaurant = _restaurantQueryRepository.Query(new RestaurantQueryModel { Name = Constants.TEGLAS_NAME })
                                                                    .Map(x => x.Items.First())
                                                                    .Reduce(_ => throw new System.Exception());
            Restaurant indexRestaurant = _restaurantQueryRepository.Query(new RestaurantQueryModel { Name = Constants.INDEX_NAME })
                                                                    .Map(x => x.Items.First())
                                                                    .Reduce(_ => throw new System.Exception());

            DailyMenu lipaDailyMenu = _dailyMenuQueryRepository.Query(new DailyMenuQueryModel { RestaurantId = lipaRestaurant.Id })
                                                               .Map(x => x.Items.FirstOrDefault())
                                                               .Reduce(_ => throw new System.Exception());
            DailyMenu hedoneDailyMenu = _dailyMenuQueryRepository.Query(new DailyMenuQueryModel { RestaurantId = hedoneRestaurant.Id })
                                                                 .Map(x => x.Items.FirstOrDefault())
                                                                 .Reduce(_ => throw new System.Exception());
            DailyMenu teglasDailyMenu = _dailyMenuQueryRepository.Query(new DailyMenuQueryModel { RestaurantId = teglasRestaurant.Id })
                                                                 .Map(x => x.Items.FirstOrDefault())
                                                                 .Reduce(_ => throw new System.Exception());
            DailyMenu indexDailyMenu = _dailyMenuQueryRepository.Query(new DailyMenuQueryModel { RestaurantId = indexRestaurant.Id })
                                                                 .Map(x => x.Items.FirstOrDefault())
                                                                 .Reduce(_ => throw new System.Exception());

            // Get food from sheet, update database for new and changed and update daily menu
            var restaurantOffers = _sheetsApi.GetOffers(DateTime.Today).ToList();
            var updateDate = DateTime.Today;

            UpdateRestaurant(restaurantOffers, lipaRestaurant, lipaDailyMenu, updateDate);
            UpdateRestaurant(restaurantOffers, hedoneRestaurant, hedoneDailyMenu, updateDate);
            UpdateRestaurant(restaurantOffers, teglasRestaurant, teglasDailyMenu, updateDate);
            UpdateRestaurant(restaurantOffers, lipaRestaurant, lipaDailyMenu, updateDate);

        }

        /// <summary>
        /// Updates <see cref="Food"/> info in database, if food doesn't exist creates new one
        /// </summary>
        /// <param name="restaurant"></param>
        /// <param name="sheetFoods"></param>
        private void AddAndUpdateFood(Restaurant restaurant, List<Food> sheetFoods)
        {
            foreach (var food in sheetFoods)
            {
                var dbFood = restaurant.Foods.SingleOrDefault(f => f.Name == food.Name);
                if (dbFood != null)
                {
                    var d = _mapper.Map<FoodUpdateModel>(dbFood);
                    d.Price = food.Price;
                    d.Description = food.Description;
                    _foodCommandRepository.Update(dbFood.Id, d);
                }
                else
                {
                    var d = _mapper.Map<FoodInsertModel>(food);
                    d.RestaurantId = restaurant.Id;
                    _foodCommandRepository.Insert(d);
                }
            }

            foreach (var food in restaurant.Foods)
            {
                var sheetFood = sheetFoods.SingleOrDefault(f => f.Name == food.Name);
                if (sheetFood == null)
                {
                    var udpateModel = _mapper.Map<FoodUpdateModel>(food);
                    udpateModel.IsInactive = true;
                    _foodCommandRepository.Update(food.Id, udpateModel);
                }
            }
        }

        /// <summary>
        /// Finds <see cref="Food"/> in database based on sheet data
        /// </summary>
        /// <param name="restaurant">Restaurant to get food for</param>
        /// <param name="dailyMenufoods">Food list from sheet</param>
        /// <returns>Food list from database</returns>
        private List<Food> FoodsFromDB(Restaurant restaurant, List<Food> dailyMenufoods)
        {
            var results = _foodQueryRepository.Query(new FoodQueryModel() { RestaurantId = restaurant.Id })
                .Map(x =>
                {
                    List<Food> result = new List<Food>();
                    foreach (var food in dailyMenufoods)
                    {
                        result.Add(x.Items.First(f => f.Name == food.Name));
                    }

                    return result;
                })
                .Reduce(_ => new List<Food>());

            return results;
        }

        private void UpdateRestaurant(List<RestaurantOffer> offers, Restaurant restaurant, DailyMenu dailyMenu, DateTime date)
        {
            var (Found, StandardOffer, DailyOffer) = offers.GetRestaurantOffersForDate(restaurant, date);
            if (Found)
            {
                AddAndUpdateFood(restaurant, StandardOffer);
                dailyMenu.Foods = FoodsFromDB(restaurant, DailyOffer);
                _restaurantCommandRepository.Update(restaurant.Id, _mapper.Map<RestaurantUpdateModel>(restaurant));
            }
        }
    }
}
