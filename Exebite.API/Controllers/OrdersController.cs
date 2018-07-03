using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.Business;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IOrderService _orderService;
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public OrdersController(
            IMenuService menuService,
            IOrderService orderService,
            IFoodRepository foodRepository,
            IMapper mapper)
        {
            _menuService = menuService;
            _orderService = orderService;
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var listOfOrders = _orderService.GetAllOrders();
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(listOfOrders));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderDto model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var createdOrder = _orderService.CreateOrder(_mapper.Map<Order>(model));

            return Ok(createdOrder.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateOrderDto model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Order currentOrder = _orderService.GetOrderById(id);
            if (currentOrder == null)
            {
                return NotFound();
            }

            _mapper.Map(model, currentOrder);
            var updatedOrder = _orderService.UpdateOrder(currentOrder);
            return Ok(updatedOrder.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}