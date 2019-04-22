using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Interfaces
{
    /// <summary>
    /// Описание сервиса заказов
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Показывает заказы пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>перечисление заказов</returns>
        IEnumerable<Order> GetUserOrders(User user);

        /// <summary>
        /// Получение заказа по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Заказ</returns>
        Order GetOrderById(int id);

        /// <summary>
        /// Создание нового заказа на основе данных
        /// </summary>
        /// <param name="OrderModel">Модель заказа</param>
        /// <param name="CartModel">Модель корзины</param>
        /// <param name="UserName">Имя пользователя</param>
        /// <returns></returns>
        Order CreateOrder(OrderViewModel OrderModel, CartViewModel CartModel, string UserName);
    }
}
