﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Domain.DTO;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Servcies;
using Assert = Xunit.Assert;

namespace WebStore.Tests
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void CheckOut_ModelState_Invalid_Returns_ViewModel()
        {
            var cart_service_mock = new Mock<ICartService>();
            var order_service_mock = new Mock<IOrderService>();
            var logger_mock = new Mock<ILogger<CartController>>();

            var controller = new CartController(cart_service_mock.Object, order_service_mock.Object, logger_mock.Object);

            controller.ModelState.AddModelError("error", "InvalidModel");

            const string expected_model_name = "Test order";
            var result = controller.CheckOut(new OrderViewModel { Name = expected_model_name });

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<DetailsViewModel>(view_result.ViewData.Model);

            Assert.Equal(expected_model_name, model.OrderViewModel.Name);
        }

        [TestMethod]
        public void CheckOut_Calls_Service_and_Return_Redirect()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "1") }));

            var cart_service_mock = new Mock<ICartService>();
            cart_service_mock
               .Setup(c => c.TransformCart())
               .Returns(() => new CartViewModel
               {
                   Items = new Dictionary<ProductViewModel, int>
                    {
                        { new ProductViewModel(), 1 }
                    }
               });

            var order_service_mock = new Mock<IOrderService>();
            order_service_mock
               .Setup(c => c.CreateOrder(It.IsAny<CreateOrderModel>(), It.IsAny<string>()))
               .Returns(new OrderDTO { Id = 1 });

            var logger_mock = new Mock<ILogger<CartController>>();

            var controller = new CartController(cart_service_mock.Object, order_service_mock.Object, logger_mock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = user
                    }
                }
            };

            var result = controller.CheckOut(new OrderViewModel
            {
                Name = "Test",
                Address = "",
                Phone = ""
            });

            var redirect_result = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirect_result.ControllerName);
            Assert.Equal(nameof(CartController.OrderConfirmed), redirect_result.ActionName);

            Assert.Equal(1, redirect_result.RouteValues["id"]);
        }
    }
}