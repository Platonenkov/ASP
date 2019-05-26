using WebStore.Domain.Models;

namespace WebStore.Interfaces.Servcies
{
    /// <summary>
    /// Интерфейс хранения данных в корзине
    /// </summary>
    public interface ICartStore
    {
        Cart Cart { get; set; }
    }
}