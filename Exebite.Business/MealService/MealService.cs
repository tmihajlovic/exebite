using System;
using System.Collections.Generic;
using Exebite.DataAccess.Repositories;
using Exebite.Model;

namespace Exebite.Business
{
    public class MealService : IMealService
    {
        private IMealRepository _mealRepository;

        public MealService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public IList<Meal> Get(int page, int size)
        {
            return _mealRepository.Get(page, size);
        }

        public Meal GetById(int mealId)
        {
            return _mealRepository.GetByID(mealId);
        }

        public Meal Update(Meal meal)
        {
            return _mealRepository.Update(meal);
        }

        public Meal Create(Meal meal)
        {
            if (meal == null)
            {
                throw new ArgumentNullException(nameof(meal));
            }

            return _mealRepository.Insert(meal);
        }

        public void Delete(int mealId)
        {
            var recipe = _mealRepository.GetByID(mealId);
            if (recipe != null)
            {
                _mealRepository.Delete(mealId);
            }
        }
    }
}
