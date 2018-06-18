using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;

namespace Exebite.DataAccess.AutoMapper
{
    public class MealToMealEntityConverter : IMealToMealEntityConverter
    {
        private readonly IFoodOrderingContextFactory _factory;
        private readonly IMapper _mapper;

        public MealToMealEntityConverter(IFoodOrderingContextFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public MealEntity Convert(Meal source, MealEntity destination, ResolutionContext context)
        {
            using (var dbContext = _factory.Create())
            {
                destination = new MealEntity
                {
                    Id = source.Id,
                    Price = source.Price
                };
                destination.Id = source.Id;
                destination.FoodEntityMealEntities = new List<FoodEntityMealEntities>();
                foreach (var food in source.Foods)
                {
                    var dbFoodMealEntity = dbContext.Meals.FirstOrDefault(m => m.Id == source.Id).FoodEntityMealEntities.SingleOrDefault(fm => fm.FoodEntityId == food.Id);
                    if (dbFoodMealEntity == null)
                    {
                        destination.FoodEntityMealEntities.Add(new FoodEntityMealEntities
                        {
                            FoodEntity = _mapper.Map<FoodEntity>(food),
                            FoodEntityId = food.Id,
                            MealEntityId = source.Id,
                            MealEntity = destination
                        });
                    }
                    else
                    {
                        destination.FoodEntityMealEntities.Add(dbFoodMealEntity);
                    }
                }

                return destination;
            }
        }
    }
}
