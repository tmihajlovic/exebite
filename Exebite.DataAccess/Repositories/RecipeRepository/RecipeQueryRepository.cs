using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public class RecipeQueryRepository : IRecipeQueryRepository
    {
        private readonly IMapper _mapper;
        private readonly IFoodOrderingContextFactory _factory;

        public RecipeQueryRepository(IFoodOrderingContextFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public Either<Error, PagingResult<Recipe>> Query(RecipeQueryModel queryModel)
        {
            try
            {
                if (queryModel == null)
                {
                    return new Left<Error, PagingResult<Recipe>>(new ArgumentNotSet(nameof(queryModel)));
                }

                using (var context = _factory.Create())
                {
                    var query = context.Recipe.AsQueryable();

                    if (queryModel.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == queryModel.Id);
                    }

                    if (queryModel.MainCourseId.HasValue)
                    {
                        query = query.Where(x => x.MainCourseId == queryModel.MainCourseId);
                    }

                    var total = query.Count();
                    query = query
                        .Skip((queryModel.Page - 1) * queryModel.Size)
                        .Take(queryModel.Size);

                    var recepieEntities = query.ToList();
                    var recepies = _mapper.Map<IList<Recipe>>(recepieEntities).ToList();
                    return new Right<Error, PagingResult<Recipe>>(new PagingResult<Recipe>(recepies, total));
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, PagingResult<Recipe>>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, IList<Recipe>> GetRecipesForFood(int foodId)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var recepieEntities = context.Food.Where(fe => fe.Id == foodId).SelectMany(r => r.FoodEntityRecipeEntities.Select(x => x.RecipeEntity)).ToList();

                    var recepies = _mapper.Map<IList<Recipe>>(recepieEntities);

                    return new Right<Error, IList<Recipe>>(recepies);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, IList<Recipe>>(new UnknownError(ex.ToString()));
            }
        }
    }
}
