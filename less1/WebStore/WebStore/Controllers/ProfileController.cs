﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IOrderService _OrderServise;

        public ProfileController(IOrderService OrderServise)
        {
            _OrderServise = OrderServise;
        }
        public IActionResult Index() => View();

        public IActionResult Orders()
        {
            var orders = _OrderServise.GetUserOrders(User.Identity.Name);


            return View(orders.Select(order=>new UserOrderViewModel
            {
                Id = order.Id,
                Name=order.Name,
                Address=order.Address,
                Phone=order.Phone,
                TotalSum = order.OrderItems.Sum( o=> o.Quantity * o.Price)
            }));
        }
    }
}