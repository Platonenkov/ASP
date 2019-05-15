using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Sql
{
    /// <summary>Реализация сервиса управления заказами на основе SQL-сервера БД</summary>
    public class SqlOrdersService : IOrderService
    {
        private readonly WebStoreContext _Context;
        private readonly UserManager<User> _UserManager;
        public SqlOrdersService(WebStoreContext context, UserManager<User> UserManager)
        {
            _Context = context;
            _UserManager = UserManager;
        }
        public IEnumerable<OrderDTO> GetUserOrders(string UserName) =>
            _Context.Orders
                .Include(o => o.User)
                .Include(o => o.Orders)
                .Where(o => o.User.UserName == UserName)
                .Select(o=> new OrderDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Address = o.Address,
                    Phone = o.Phone,
                    Date = o.Date,
                    OrderItems = o.Orders.Select(item=>new OrderItemDTO
                    {
                        Id=item.Id,
                        Price = item.Price,
                        Quantity = item.Quantity

                    }).ToArray()
                })
                .ToArray();

        public OrderDTO GetOrderById(int id)
        {
            var order = _Context.Orders.Include(o => o.Orders).FirstOrDefault(o => o.Id == id);
            if (order is null) return null;
            return new OrderDTO
            {
                Id = order.Id,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                Date = order.Date,
                OrderItems = order.Orders.Select(item => new OrderItemDTO
                {
                    Id = item.Id,
                    Price = item.Price,
                    Quantity = item.Quantity

                }).ToArray()
            };
        }
        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName)
        {
            var user = _UserManager.FindByNameAsync(UserName).Result;
            using (var transaction = _Context.Database.BeginTransaction())
            {
                var order = new Order
                {
                    Name = OrderModel.OrderViewModel.Name,
                    Address = OrderModel.OrderViewModel.Address,
                    Date = DateTime.Now,
                    Phone = OrderModel.OrderViewModel.Phone,
                    User = user
                };
                _Context.Orders.Add(order);
                foreach (var item in OrderModel.OrderItems)
                {
                    var product = _Context.Products.FirstOrDefault(p=>p.Id == item.Id);
                    if (product is null)
                        throw new InvalidOperationException($"Продукт id:{item.Id} отсутвтует в базе");
                    _Context.OrderItems.Add(new OrderItem
                    {
                        Order = order,
                        Product = product,
                        Price = product.Price,
                        Quantity = item.Quantity
                    });
                }
                _Context.SaveChanges();
                transaction.Commit();
                return new OrderDTO
                {
                    Id = order.Id,
                    Name = order.Name,
                    Address = order.Address,
                    Phone = order.Phone,
                    Date = order.Date,
                    OrderItems = order.Orders.Select(item => new OrderItemDTO
                    {
                        Id = item.Id,
                        Price = item.Price,
                        Quantity = item.Quantity

                    }).ToArray()
                }; ;
            }
        }
    }
}