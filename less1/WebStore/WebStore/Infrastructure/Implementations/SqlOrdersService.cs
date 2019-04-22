using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Implementations
{
    public class SqlOrdersService : IOrderService
    {
        private readonly UserManager<User> _UserManager;
        private readonly WebStoreContext _db;

        public SqlOrdersService(WebStoreContext db, UserManager<User> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
        }


        public Order CreateOrder(OrderViewModel OrderModel, CartViewModel CartModel, string UserName)
        {
            var user = _UserManager.FindByNameAsync(UserName).Result;
            using (var transaction = _db.Database.BeginTransaction())
            {
                var order = new Order
                {
                    Name = OrderModel.Name,
                    Address = OrderModel.Address,
                    Phone = OrderModel.Phone,
                    User = user,
                    Date = DateTime.Now
                };
                _db.Orders.Add(order);
                foreach (var (product_model, quantity)  in CartModel.Items)
                {
                    var product = _db.Products.FirstOrDefault(p => p.Id == product_model.Id);
                    if (product is null)
                        throw new InvalidOperationException($"Товар с идентификатором {product_model.Id} в базе данных не найден");

                    var order_item = new OrderItem
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = quantity,
                        Product = product
                    };
                    _db.OrderItems.Add(order_item);
                }

                //сохраняем изменения
                _db.SaveChanges();
                transaction.Commit();

                return order;
            }
        }

        public Order GetOrderById(int id)
        {
            return _db.Orders
                .Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> GetUserOrders(string UserName)
        {
            return _db.Orders
                 .Include(order => order.User)
                 .Include(order => order.OrderItems)
                 .Where(order => order.User.UserName == UserName)
                 .ToArray();
        }
    }
}
