﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Servcies;
using Assert = Xunit.Assert;

namespace WebStore.Services.Tests
{
    [TestClass]
    public class CartServiceTests
    {
        [TestMethod]
        public void Cart_Class_ItemsCount_Returns_Correct_Quantity()
        {
            const int expected_count = 4;
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 1 },
                    new CartItem { ProductId = 2, Quantity = 3 }
                }
            };

            var result = cart.ItemsCount;

            Assert.Equal(expected_count, result);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_ItemsCount()
        {
            const int expected_count = 3;
            var cart_view_model = new CartViewModel
            {
                Items = new Dictionary<ProductViewModel, int>
                {
                    { new ProductViewModel { Id = 1, Name = "Item 1", Price = 0.5m}, 1 },
                    { new ProductViewModel { Id = 2, Name = "Item 2", Price = 1.0m}, 2 },
                }
            };

            var result = cart_view_model.ItemsCount;

            Assert.Equal(expected_count, result);
        }

        [TestMethod]
        public void CartService_AddToCart_WorksCorrect()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>()
            };

            var product_data_mock = new Mock<IProductData>();
            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock
               .Setup(c => c.Cart)
               .Returns(cart);

            var cart_service = new CartService(product_data_mock.Object, cart_store_mock.Object);

            const int expected_id = 5;
            cart_service.AddToCart(expected_id);

            Assert.Equal(1, cart.ItemsCount);
            Assert.Equal(1, cart.Items.Count);
            Assert.Equal(expected_id, cart.Items[0].ProductId);

        }

        [TestMethod]
        public void CartService_RemoveFromCart_Remove_Correct_Item()
        {
            const int item_id = 1;
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = item_id, Quantity = 1 },
                    new CartItem { ProductId = 2, Quantity = 3 }
                }
            };

            var product_data_mock = new Mock<IProductData>();
            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock
               .Setup(c => c.Cart)
               .Returns(cart);

            var cart_service = new CartService(product_data_mock.Object, cart_store_mock.Object);


            cart_service.RemoveFromCart(item_id);

            Assert.Equal(1, cart.Items.Count);
            Assert.Equal(2, cart.Items[0].ProductId);
        }
    }
}