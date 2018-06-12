using System.Collections.Generic;
using Exebite.Model;

namespace Exebite.Business
{
    public interface IMealService
    {
        IList<Meal> Get(int page, int size);

        Meal GetById(int mealId);

        Meal Update(Meal meal);

        Meal Create(Meal meal);

        void Delete(int mealId);
    }
}
