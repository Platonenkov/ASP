﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Implementations
{
    public class CookieCartService : ICartService
    {
        private readonly IProductData _ProductData;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly string _CartName;

        private Cart Cart
        {
            get
            {
                var http_context = _HttpContextAccessor.HttpContext;
                var cookie = http_context.Request.Cookies[_CartName];

                Cart cart = null;

                if(cookie is null)
                {
                    cart = new Cart();
                    http_context.Response.Cookies.Append(
                        _CartName,
                        JsonConvert.SerializeObject(cart));
                }
                else
                {
                    cart = JsonConvert.DeserializeObject<Cart>(cookie);
                    http_context.Response.Cookies.Delete(_CartName);
                    http_context.Response.Cookies.Append(_CartName, cookie, new CookieOptions
                    {
                        Expires=DateTime.Now.AddDays(1)
                    });
                }
                return cart;
            }

            set
            {
                var http_context = _HttpContextAccessor.HttpContext;
                var json = JsonConvert.SerializeObject(value);
                http_context.Response.Cookies.Delete(_CartName);
                http_context.Response.Cookies.Append(_CartName, json, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                });

            }
        }

        public CookieCartService(IProductData ProductData, IHttpContextAccessor HttpContextAccessor)
        {
            _ProductData = ProductData;
            _HttpContextAccessor = HttpContextAccessor;

            var user = HttpContextAccessor.HttpContext.User;

            var user_name = user.Identity.IsAuthenticated ? user.Identity.Name : null;

            _CartName = $"cart{user_name}";
        }

        public void AddtoCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item != null)
                item.Quantity++;
            else
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            Cart = cart;

        }

        public void DecrementFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item != null)
            {
                if (item.Quantity > 0)
                    item.Quantity--;
                if (item.Quantity == 0)
                    cart.Items.Remove(item);
                Cart = cart;
            }

        }

        public void RemoveAll()
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;

        }

        public void RemoveFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item != null)
            {
                cart.Items.Remove(item);
                Cart = cart;
            }

        }

        public CartViewModel TransformCart()
        {
            var products = _ProductData.GetProducts(new ProductFilter
            {
                Ids=Cart.Items.Select(item=>item.ProductId).ToList()
            });

            var products_view_model = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Brand = p.Brand?.Name
            });
            return new CartViewModel
                {
                Items= Cart.Items.ToDictionary(
                    x=>products_view_model.First(p=>p.Id==x.ProductId),
                    x=>x.Quantity)
                };
        }
    }
}
