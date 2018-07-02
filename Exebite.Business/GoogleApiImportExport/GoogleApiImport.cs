using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Either;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;

namespace Exebite.Business.GoogleApiImportExport
{
    public class GoogleApiImport : IGoogleDataImporter
    {
        private readonly IRestaurantQueryRepository _restaurantQueryRepository;
        private readonly IRestaurantCommandRepository _restaurantCommandRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IDailyMenuRepository _dailyMenuRepository;
        private readonly IMapper _mapper;

        // connectors
        private readonly ILipaConector _lipaConector;
        private readonly IHedoneConector _hedoneConector;
        private readonly ITeglasConector _teglasConector;

        public GoogleApiImport(
            IRestaurantQueryRepository restaurantQueryRepository,
            IRestaurantCommandRepository restaurantCommandRepository,
            IFoodRepository foodRepository,
            ILipaConector lipaConector,
            ITeglasConector teglasConector,
            IHedoneConector hedoneConector,
            IDailyMenuRepository dailyMenuRepository,
            IMapper mapper)
        {
            _restaurantQueryRepository = restaurantQueryRepository;
            _restaurantCommandRepository = restaurantCommandRepository;
            _foodRepository = foodRepository;
            _mapper = mapper;

            // connectors to a new sheets
            _lipaConector = lipaConector;
            _hedoneConector = hedoneConector;
            _teglasConector = teglasConector;
            _dailyMenuRepository = dailyMenuRepository;
        }

        /// <summary>
        /// Update daily menu for restaurants
        /// </summary>
        public void UpdateRestorauntsMenu()
        {
            Restaurant lipaRestaurant = _restaurantQueryRepository.Query(new RestaurantQueryModel { Name = "Restoran pod Lipom" })
                                                                  .Map(x => x.Items.First())
                                                                  .Reduce(_ => throw new System.Exception());
            Restaurant hedoneRestaurant = _restaurantQueryRepository.Query(new RestaurantQueryModel { Name = "Hedone" })
                                                                    .Map(x => x.Items.First())
                                                                    .Reduce(_ => throw new System.Exception());
            Restaurant teglasRestaurant = _restaurantQueryRepository.Query(new RestaurantQueryModel { Name = "Teglas" })
                                                                    .Map(x => x.Items.First())
                                                                    .Reduce(_ => throw new System.Exception());

            DailyMenu lipaDailyMenu = _dailyMenuRepository.Query(new DailyMenuQueryModel { RestaurantId = lipaRestaurant.Id }).FirstOrDefault();
            DailyMenu hedoneDailyMenu = _dailyMenuRepository.Query(new DailyMenuQueryModel { RestaurantId = hedoneRestaurant.Id }).FirstOrDefault();
            DailyMenu teglasDailyMenu = _dailyMenuRepository.Query(new DailyMenuQueryModel { RestaurantId = teglasRestaurant.Id }).FirstOrDefault();

            // Get food from sheet, update database for new and changed and update daily menu

            // Lipa
            // Check if all food exist in DB
            AddAndUpdateFood(lipaRestaurant, _lipaConector);

            // Update daily menu
            lipaDailyMenu.Foods = FoodsFromDB(lipaRestaurant, _lipaConector.GetDailyMenu());
            _restaurantCommandRepository.Update(lipaRestaurant.Id, _mapper.Map<RestaurantUpdateModel>(lipaRestaurant));

            // Teglas
            // Check if all food exist in DB
            AddAndUpdateFood(teglasRestaurant, _teglasConector);

            // Update daily menu
            teglasDailyMenu.Foods = FoodsFromDB(teglasRestaurant, _teglasConector.GetDailyMenu());
            _restaurantCommandRepository.Update(teglasRestaurant.Id, _mapper.Map<RestaurantUpdateModel>(teglasRestaurant));

            // Hedone
            // Check if all food exist in DB
            AddAndUpdateFood(hedoneRestaurant, _hedoneConector);

            // Update daily menu
            hedoneDailyMenu.Foods = FoodsFromDB(hedoneRestaurant, _hedoneConector.GetDailyMenu());
            _restaurantCommandRepository.Update(hedoneRestaurant.Id, _mapper.Map<RestaurantUpdateModel>(hedoneRestaurant));
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
