using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        /// <summary>Сервис корзины</summary>
        private readonly ICartService _CartService;
        /// <summary>Сервис заказов</summary>
        private readonly IOrderService _OrderService;

        /// <summary>Новый контроллер корзины</summary>
        /// <param name="CartService">Сервис корзины</param>
        /// <param name="OrderService">Сервис заказов</param>
        public CartController(ICartService CartService, IOrderService OrderService) => (_CartService, _OrderService) = (CartService, OrderService);

        /// <summary>Детали корзины</summary>
        /// <returns>Представление деталей корзины</returns>
        public IActionResult Details() => View(new DetailsViewModel { CartViewModel = _CartService.TransformCart(), OrderViewModel = new OrderViewModel() });

        public IActionResult DecrementFromCart(int id)
        {
            _CartService.DecrementFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveFromCart(int id)
        {
            _CartService.RemoveFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveAll(int id)
        {
            _CartService.RemoveAll();
            return RedirectToAction("Details");
        }

        public IActionResult AddToCart(int id)
        {
            _CartService.AddToCart(id);
            return RedirectToAction("Details");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckOut(OrderViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(nameof(Details), new DetailsViewModel
                {
                    CartViewModel = _CartService.TransformCart(),
                    OrderViewModel = Model
                });

            var create_order_model = new CreateOrderModel
            {
                OrderViewModel = Model,
                OrderItems = _CartService.TransformCart().Items.Select(item => new OrderItemDTO
                {
                    Id = item.Key.Id,
                    Price = item.Key.Price,
                    Quantity = item.Value
                }).ToList()
            };

            var order = _OrderService.CreateOrder(create_order_model, User.Identity.Name);
            _CartService.RemoveAll();

            return RedirectToAction(nameof(OrderConfirmed), new { order.Id });
        }

        /// <summary>Подтверждение заказа</summary>
        /// <param name="Id">Идентификатор заказа</param>
        /// <returns>Представление подтверждения заказа</returns>
        public IActionResult OrderConfirmed(int Id)
        {
            ViewBag.OrderId = Id;
            return View();
        }
    }
}