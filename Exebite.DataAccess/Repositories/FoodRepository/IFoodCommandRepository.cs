using System.Collections.Generic;
using Either;
using Exebite.Common;

namespace Exebite.DataAccess.Repositories
{
    public interface IFoodCommandRepository : IDatabaseCommandRepository<int, FoodInsertModel, FoodUpdateModel>
    {
        Either<Error, bool> DeactivatFoods(IList<int> foodIds);
    }
}
