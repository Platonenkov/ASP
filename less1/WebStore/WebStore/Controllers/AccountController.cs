using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
        }
        public IActionResult Register() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model); //Если  данные не корректные то на доработку

            var new_user = new User  //Создаем новго пользователя
            {
                UserName = model.UserName
            };

            //Пытаемся зарегистрировать его в системе с указанным паролем
            var creation_result = await _UserManager.CreateAsync(new_user, model.Password);
            if (creation_result.Succeeded)                           // Если получилось
            {
                await _SignInManager.SignInAsync(new_user, false);   //То сразу логиним его на сайте

                return RedirectToAction("Index", "Home");            // И отправляем на главную страницу
            }

            foreach (var error in creation_result.Errors)            // Если чтото пошло не так...
                ModelState.AddModelError("", error.Description);     // Все ошибки заносим в состояние модели
            return View(model);                                      // и модель отправляем на доработку       
        }

        public IActionResult Login() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid) return View(login);

            var login_result = await _SignInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, false);

            if (login_result.Succeeded)
            {
                if (Url.IsLocalUrl(login.ReturnUrl))
                    return Redirect(login.ReturnUrl);

                return RedirectToAction("index", "Home");
            }
            ModelState.AddModelError("", "Имя пользователя, или пароль не верны!");
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied() => View();

    }
}