﻿using System;
using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.Business.Model;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Services;

namespace Exebite.Business
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IMealQueryRepository _mealQuery;
        private readonly ICustomerQueryRepository _customerQuery;
        private readonly ILocationQueryRepository _locationQuery;
        private readonly IGoogleSheetAPIService _apiService;

        public RestaurantService(
            IMealQueryRepository mealQuery,
            ICustomerQueryRepository customerQuery,
            ILocationQueryRepository locationQuery,
            IGoogleSheetAPIService googleSheetAPIService)
        {
            _mealQuery = mealQuery;
            _customerQuery = customerQuery;
            _locationQuery = locationQuery;
            _apiService = googleSheetAPIService;
        }

        public Either<Error, RestaurantOrder> PlaceOrdersForRestaurant(RestaurantOrder order)
        {
            if (order == null)
            {
                return new Left<Error, RestaurantOrder>(new ArgumentNotSet(nameof(order)));
            }

            var customer = _customerQuery.Query(new CustomerQueryModel() { Id = order.CustomerId })
                .Map(c => c.Items.ToList().FirstOrDefault())
                .Reduce(_ => throw new Exception());

            var location = _locationQuery.Query(new LocationQueryModel() { Id = order.LocationId })
                .Map(l => l.Items.ToList().FirstOrDefault())
                .Reduce(_ => throw new Exception());

            List<Meal> meals = new List<Meal>();

            foreach (var mealOrder in order.Meals)
            {
                var meal = _mealQuery.Query(new MealQueryModel() { Id = mealOrder.Id })
                    .Map(c => c.Items.ToList().FirstOrDefault())
                    .Reduce(_ => throw new Exception());

                meals.Add(meal);
            }

            _apiService.WriteOrder(customer.Name, location.Name, meals);

            return order;
        }

        /// <summary>
        /// Update daily menu for restaurants
        /// </summary>
        public void UpdateRestaurantMenu()
        {
            _apiService.UpdateDailyMenuLipa();
            _apiService.UpdateDailyMenuMimas();
            _apiService.UpdateDailyMenuParrilla();
            _apiService.UpdateDailyMenuTopliObrok();
            _apiService.UpdateDailyMenuSerpica();
        }
    }
}
