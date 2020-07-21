using System;
using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.API.Authorization;
using Exebite.Business;
using Exebite.Common;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.DtoModels;
using Exebite.GoogleSheetAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderQueryRepository _queryRepo;
        private readonly IOrderCommandRepository _commandRepo;
        private readonly ICustomerQueryRepository _queryCustomer;
        private readonly ILocationQueryRepository _queryLocation;
        private readonly IMealQueryRepository _queryMeal;
        private readonly IEitherMapper _mapper;
        private readonly IGoogleSheetAPIService _apiService;
        private readonly IRestaurantService _restaurantService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(
            IOrderQueryRepository queryRepo,
            IOrderCommandRepository commandRepo,
            ICustomerQueryRepository queryCustomer,
            ILocationQueryRepository queryLocation,
            IMealQueryRepository queryMeal,
            IEitherMapper mapper,
            IGoogleSheetAPIService apiService,
            IRestaurantService restaurantService,
            ILogger<OrdersController> logger)
        {
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _queryCustomer = queryCustomer;
            _queryLocation = queryLocation;
            _queryMeal = queryMeal;
            _mapper = mapper;
            _apiService = apiService;
            _restaurantService = restaurantService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Policy = nameof(AccessPolicy.CreateOrdersAccessPolicy))]
        public IActionResult Post([FromBody] CreateOrderDto model)
        {
            return _restaurantService.PlaceOrdersForRestaurant(_mapper.Map<Order>(model).Reduce(r => null, ex => Console.WriteLine(ex.ToString())))
                .Map(x => Created(new { id = x }))
                .Reduce(_ => BadRequest(), error => error is ArgumentNotSet)
                .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = nameof(AccessPolicy.UpdateOrdersAccessPolicy))]
        public IActionResult Put(int id, [FromBody] UpdateOrderDto model)
        {
            List<Meal> meals = new List<Meal>();
            var customer = _queryCustomer.Query(new CustomerQueryModel() { Id = model.CustomerId })
                .Map(c => c.Items.ToList())
                .Reduce(_ => throw new Exception());
            foreach (var mealOrder in model.Meals)
            {
                var meal = _queryMeal.Query(new MealQueryModel() { Id = mealOrder.MealId })
                .Map(c => c.Items.ToList())
                .Reduce(_ => throw new Exception());

                meals.Add(meal.FirstOrDefault());
            }

            _apiService.WriteOrder(customer.FirstOrDefault(), meals);
            return _mapper.Map<OrderUpdateModel>(model)
                       .Map(x => _commandRepo.Update(id, x))
                       .Map(x => AllOk(new { updated = x }))
                       .Reduce(_ => NotFound(), error => error is RecordNotFound)
                       .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = nameof(AccessPolicy.DeleteOrdersAccessPolicy))]
        public IActionResult Delete(int id) =>
            _commandRepo.Delete(id)
                        .Map(_ => OkNoContent())
                        .Reduce(_ => NotFound(), error => error is RecordNotFound)
                        .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("Query")]
        [Authorize(Policy = nameof(AccessPolicy.ReadOrdersAccessPolicy))]
        public IActionResult Query([FromQuery]OrderQueryDto query) =>
            _mapper.Map<OrderQueryModel>(query)
                   .Map(_queryRepo.Query)
                   .Map(_mapper.Map<PagingResult<OrderDto>>)
                   .Map(x => AllOk(x))
                   .Reduce(_ => BadRequest(), error => error is ArgumentNotSet, x => _logger.LogError(x.ToString()))
                   .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));

        [HttpGet("GetAllOrdersForRestaurant")]
        [Authorize(Policy = nameof(AccessPolicy.ReadOrdersAccessPolicy))]
        public IActionResult GetAllOrdersForRestaurant(int restaurantId, int page, int size) =>
            _queryRepo.GetAllOrdersForRestaurant(restaurantId, page, size)
                      .Map(_mapper.Map<PagingResult<OrderDto>>)
                      .Map(AllOk)
                      .Reduce(_ => InternalServerError(), x => _logger.LogError(x.ToString()));
    }
}