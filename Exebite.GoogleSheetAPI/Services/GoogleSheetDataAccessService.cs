﻿using System;
using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;

namespace Exebite.GoogleSheetAPI.Services
{
    /// <inheritdoc/>
    public sealed class GoogleSheetDataAccessService : IGoogleSheetDataAccessService
    {
        private readonly IEitherMapper _mapper;

        private readonly ICustomerCommandRepository _customerCommandRepository;
        private readonly ICustomerQueryRepository _customerQueryRepository;
        private readonly IFoodQueryRepository _foodQueryRepository;
        private readonly IFoodCommandRepository _foodCommandRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleSheetDataAccessService"/> class.
        /// </summary>
        /// <param name="mapper">Mapper instance.</param>
        /// <param name="customerCommandRepository">Customer command repository</param>
        /// <param name="customerQueryRepository">Customer query repository.</param>
        /// <param name="foodCommandRepository">Food command repository</param>
        /// <param name="foodQueryRepository">Food query repository.</param>
        public GoogleSheetDataAccessService(
            IEitherMapper mapper,
            ICustomerCommandRepository customerCommandRepository,
            ICustomerQueryRepository customerQueryRepository,
            IFoodCommandRepository foodCommandRepository,
            IFoodQueryRepository foodQueryRepository)
        {
            _mapper = mapper;

            _customerCommandRepository = customerCommandRepository;
            _customerQueryRepository = customerQueryRepository;
            _foodCommandRepository = foodCommandRepository;
            _foodQueryRepository = foodQueryRepository;
        }

        /// <inheritdoc/>
        public Either<Error, (int Added, int Updated)> UpdateCustomers(IEnumerable<Customer> customers)
        {
            var added = 0;
            var updated = 0;

            if (!customers.Any())
            {
                return new Left<Error, (int, int)>(new ArgumentNotSet(nameof(customers)));
            }

            foreach (var customer in customers)
            {
                var exists = _customerQueryRepository
                     .ExistsByGoogleId(customer.GoogleUserId)
                     .Reduce(r => false, ex => Console.WriteLine(ex.ToString()));

                if (!exists)
                {
                    added += _mapper
                        .Map<CustomerInsertModel>(customer)
                        .Map(_customerCommandRepository.Insert)
                        .Reduce(r => 0, ex => Console.WriteLine(ex.ToString())) > 0 ? 1 : 0;
                }
                else
                {
                    updated += _mapper
                        .Map<CustomerUpdateModel>(customer)
                        .Map(_customerCommandRepository.UpdateByGoogleId)
                        .Reduce(r => false, ex => Console.WriteLine(ex.ToString())) ? 1 : 0;
                }
            }

            return new Right<Error, (int, int)>((added, updated));
        }

        /// <inheritdoc/>
        public Either<Error, (int Added, int Updated)> UpdateFoods(IEnumerable<Food> foods)
        {
            var added = 0;
            var updated = 0;

            if (!foods.Any())
            {
                return new Left<Error, (int, int)>(new ArgumentNotSet(nameof(foods)));
            }

            foreach (var food in foods)
            {
                var exists = _foodQueryRepository
                            .FindByNameAndRestaurantId(new FoodQueryModel { Name = food.Name, RestaurantId = food.RestaurantId })
                            .Reduce(r => 0, ex => Console.WriteLine(ex.ToString())) > 0;

                if (!exists)
                {
                    var insertFood = new FoodInsertModel
                    {
                        Description = food.Description,
                        IsInactive = food.IsInactive,
                        Name = food.Name,
                        Price = food.Price,
                        RestaurantId = food.RestaurantId,
                        Type = food.Type
                    };

                    added += _mapper
                        .Map<FoodInsertModel>(insertFood)
                        .Map(_foodCommandRepository.Insert)
                        .Reduce(r => 0, ex => Console.WriteLine(ex.ToString())) > 0 ? 1 : 0;
                }
                else
                {
                    updated += _mapper
                        .Map<FoodUpdateModel>(food)
                        .Map(_foodCommandRepository.UpdateByNameAndRestaurantId)
                        .Reduce(r => false, ex => Console.WriteLine(ex.ToString())) ? 1 : 0;
                }
            }

            return new Right<Error, (int, int)>((added, updated));
        }
    }
}