using System;
using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Option;

namespace Exebite.Business
{
    public class MenuService : IMenuService
    {
        private readonly IRestaurantQueryRepository _restaurantRepository;
        private readonly IRecipeQueryRepository _recipeQueryRepository;
        private readonly IFoodQueryRepository _foodQueryRepository;

        public MenuService(IRestaurantQueryRepository restaurantRepository, IFoodQueryRepository foodQueryRepository, IRecipeQueryRepository recipeQueryRepository)
        {
            _restaurantRepository = restaurantRepository;
            _foodQueryRepository = foodQueryRepository;
            _recipeQueryRepository = recipeQueryRepository;
        }

        public Either<Error, PagingResult<Restaurant>> GetRestorantsWithMenus()
        {
            return _restaurantRepository.Query(new RestaurantQueryModel());
        }

        public decimal CheckPrice(Meal meal)
        {
            // TODO: when special offers are done implement check
            throw new NotImplementedException();
        }

        public IList<Food> CheckAvailableSideDishes(int foodId)
        {
            var recepies = _foodQueryRepository.Query(new FoodQueryModel() { Id = foodId })
                .Map(x => x.Items.FirstOrNone())
                .Map(z =>
                    z.Map(x => _recipeQueryRepository.Query(new RecipeQueryModel() { MainCourseId = x.Id }))
                     .Reduce(PagingResult<Recipe>.Empty()))
                .Map(x => x.Items)
                .Reduce(_ => new List<Recipe>());

            return recepies.SelectMany(x => x.SideDish).ToList();
        }
    }
}
