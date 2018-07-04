using System;
using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Option;

namespace Exebite.Business
{
    public class MenuService : IMenuService
    {
        private readonly IRestaurantQueryRepository _restaurantRepository;
        private readonly IFoodQueryRepository _foodQueryRepository;
        private readonly IRecipeRepository _recipeRepository;

        public MenuService(IRestaurantQueryRepository restaurantRepository, IRecipeRepository recipeRepository, IFoodQueryRepository foodQueryRepository)
        {
            _restaurantRepository = restaurantRepository;
            _recipeRepository = recipeRepository;
            _foodQueryRepository = foodQueryRepository;
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
            // TODO move to railway style when refactoring menu service
            var recepies = _foodQueryRepository.Query(new FoodQueryModel() { Id = foodId })
                .Map(x => x.Items.FirstOrNone())
                .Map(z =>
                    z.Map(x => _recipeRepository.Query(new RecipeQueryModel() { MainCourseId = x.Id }))
                     .Reduce(new List<Recipe>())
                ).Reduce(_ => new List<Recipe>());

            return recepies.SelectMany(x => x.SideDish).ToList();
        }
    }
}
