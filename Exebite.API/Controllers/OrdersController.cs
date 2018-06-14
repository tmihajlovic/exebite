using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.Business;
using Exebite.DataAccess.Repositories;
using Exebite.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/orders")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMenuService _menuService;
        private readonly IOrderService _orderService;
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public OrdersController(ICustomerRepository customerRepository, IMenuService menuService, IOrderService orderService, IFoodRepository foodRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _menuService = menuService;
            _orderService = orderService;
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var listOfOrders = _orderService.GetAllOrders();
            return Ok(_mapper.Map<IEnumerable<OrderModel>>(listOfOrders));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderModel>(order));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var createdOrder = _orderService.CreateOrder(_mapper.Map<Order>(model));

            return Ok(createdOrder.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateOrderModel model)
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