using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;

namespace Exebite.DataAccess.AutoMapper
{
    public class RecipeToRecipeEntityConverter : IRecipeToRecipeEntityConverter
    {
        private readonly IMapper _mapper;
        private readonly IFoodOrderingContextFactory _factory;

        public RecipeToRecipeEntityConverter(IMapper mapper, IFoodOrderingContextFactory factory)
        {
            _mapper = mapper;
            _factory = factory;
        }

        public RecipeEntity Convert(Recipe source, RecipeEntity destination, ResolutionContext context)
        {
            destination = new RecipeEntity
            {
                Id = source.Id,
                MainCourse = _mapper.Map<FoodEntity>(source.MainCourse),
                MainCourseId = source.MainCourse.Id,
                RestaurantId = source.Restaurant.Id,
                FoodEntityRecipeEntities = new List<FoodEntityRecipeEntity>()
            };
            foreach (var food in source.SideDish)
            {
                using (var dbcontext = _factory.Create())
                {
                    var fre = dbcontext.Recipes.FirstOrDefault(r => r.Id == source.Id).FoodEntityRecipeEntities.SingleOrDefault(i => i.FoodEntityId == food.Id);
                    if (fre == null)
                    {
                        destination.FoodEntityRecipeEntities.Add(new FoodEntityRecipeEntity
                        {
                            FoodEntity = _mapper.Map<FoodEntity>(food),
                            FoodEntityId = food.Id,
                            RecipeEntity = destination,
                            RecepieEntityId = source.Id
                        });
                    }
                    else
                    {
                        destination.FoodEntityRecipeEntities.Add(fre);
                    }
                }
            }

            return destination;
        }
    }
}
