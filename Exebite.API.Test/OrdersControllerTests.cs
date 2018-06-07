using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Exebite.API.Controllers;
using Exebite.API.Models;
using Exebite.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Exebite.API.Test
{
    public class OrdersControllerTests
    {
        private Mock<ICustomerService> _customerService;
        private Mock<IMenuService> _menuService;
        private Mock<IOrderService> _orderService;
        private Mock<IFoodService> _foodService;

        [Fact]
        public void Get_CustomerDoesNotExists_NotFoundResultReturned()
        {
            // Arrange
            var sut = CreateOrdersControllerInstance();
            CreateClaimForUser(sut);

            // Act
            var result = sut.Get();

            // Assert
            Assert.Equal(typeof(NotFoundResult), result.GetType());
        }

        [Fact]
        public void Get_CustomerDoesNotExists_OkObjectResultReturned()
        {
            // Arrange
            string foodName = "test food";
            var sut = CreateOrdersControllerInstance();
            _customerService.Setup(x => x.GetCustomerByIdentityId(It.IsAny<string>())).Returns(new Model.Customer { Id = 1 });
            _orderService.Setup(x => x.GetAllOrdersForCustomer(It.IsAny<int>())).Returns(new List<Model.Order>());
            _menuService.Setup(x => x.GetRestorantsWithMenus()).Returns(
                new List<Model.Restaurant>
                {
                    new Model.Restaurant
                    {
                        DailyMenu = new List<Model.Food>
                        {
                            new Model.Food { Name = foodName }
                        }
                    }
                });
            CreateClaimForUser(sut);

            // Act
            var result = sut.Get();

            // Assert
            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.Equal(foodName, ((OrdersViewModel)((OkObjectResult)result).Value).TodayFoods.ElementAt(0).Name);
        }

        private void CreateClaimForUser(OrdersController sut)
        {
            var claims = new List<Claim> { new Claim("TestClaimType", "Test Claim", ClaimValueTypes.String) };
            var userIdentity = new ClaimsIdentity(claims, "User");
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            sut.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(userPrincipal)
            };
        }

        private void Initialize()
        {
            _customerService = new Mock<ICustomerService>();
            _menuService = new Mock<IMenuService>();
            _orderService = new Mock<IOrderService>();
            _foodService = new Mock<IFoodService>();
        }

        private OrdersController CreateOrdersControllerInstance()
        {
            Initialize();
            return new OrdersController(_customerService.Object, _menuService.Object, _orderService.Object, _foodService.Object);
        }
    }
}
