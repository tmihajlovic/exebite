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
        private readonly IFoodQueryRepository _foodQueryRepository;
        private readonly IFoodCommandRepository _foodCommandRepository;

        private readonly IDailyMenuQueryRepository _dailyMenuQueryRepository;
        private readonly IMapper _mapper;

        // connectors
        private readonly ILipaConector _lipaConector;
        private readonly IHedoneConector _hedoneConector;
        private readonly ITeglasConector _teglasConector;

        public GoogleApiImport(
            IRestaurantQueryRepository restaurantQueryRepository,
            IRestaurantCommandRepository restaurantCommandRepository,
            ILipaConector lipaConector,
            ITeglasConector teglasConector,
            IHedoneConector hedoneConector,
            IDailyMenuQueryRepository dailyMenuQueryRepository,
            IMapper mapper,
            IFoodQueryRepository foodQueryRepository,
            IFoodCommandRepository foodCommandRepository)
        {
            _restaurantQueryRepository = restaurantQueryRepository;
            _restaurantCommandRepository = restaurantCommandRepository;
            _mapper = mapper;

            // connectors to a new sheets
            _lipaConector = lipaConector;
            _hedoneConector = hedoneConector;
            _teglasConector = teglasConector;
            _dailyMenuQueryRepository = dailyMenuQueryRepository;
            _foodQueryRepository = foodQueryRepository;
            _foodCommandRepository = foodCommandRepository;
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

            DailyMenu lipaDailyMenu = _dailyMenuQueryRepository.Query(new DailyMenuQueryModel { RestaurantId = lipaRestaurant.Id })
                                                               .Map(x => x.Items.FirstOrDefault())
                                                               .Reduce(_ => throw new System.Exception());
            DailyMenu hedoneDailyMenu = _dailyMenuQueryRepository.Query(new DailyMenuQueryModel { RestaurantId = hedoneRestaurant.Id })
                                                                 .Map(x => x.Items.FirstOrDefault())
                                                                 .Reduce(_ => throw new System.Exception());
            DailyMenu teglasDailyMenu = _dailyMenuQueryRepository.Query(new DailyMenuQueryModel { RestaurantId = teglasRestaurant.Id })
                                                                 .Map(x => x.Items.FirstOrDefault())
                                                                 .Reduce(_ => throw new System.Exception());

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
                .Reduce(x => new List<Food>());

            return results;
        }
    }
}
