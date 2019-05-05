using System.Collections.Generic;
using System.Linq;

namespace WebStore.Domain.Models
{
    /// <summary>
    /// Клас описывающий товары в корзине
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Количество товаров в записи
        /// </summary>
        public int Quantity { get; set; }
    }


    /// <summary>
    /// Класс описывающий корзину
    /// </summary>
    public class Cart
    {

        /// <summary>
        /// Список товаров
        /// </summary>
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        /// <summary>
        /// Количество товаров
        /// </summary>
        public int ItemsCount => Items?.Sum(Item => Item.Quantity) ?? 0;

    }
}
