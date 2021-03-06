﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Interfaces
{
    public interface ICartService
    {
        /// <summary>
        /// Уменьшать количество элементов тавара в корзине
        /// </summary>
        /// <param name="id"></param>
        void DecrementFromCart(int id);
        /// <summary>
        /// Удалить по ID
        /// </summary>
        /// <param name="id"></param>
        void RemoveFromCart(int id);
        /// <summary>
        /// Удалить все из корзины
        /// </summary>
        void RemoveAll();
        /// <summary>
        /// Добавить элемент в корзину
        /// </summary>
        /// <param name="id"></param>
        void AddtoCart(int id);

        /// <summary>
        /// Формирует на основе сервиса модель представления корзины чтобы передать ее в представление
        /// </summary>
        /// <returns></returns>
        CartViewModel TransformCart();
    }
}
