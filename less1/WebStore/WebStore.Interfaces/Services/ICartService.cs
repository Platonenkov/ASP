using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
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
        void AddToCart(int id);

        /// <summary>
        /// Формирует на основе сервиса модель представления в корзины чтобы передать ее в представление
        /// </summary>
        /// <returns></returns>
        CartViewModel TransformCart();
    }
}
