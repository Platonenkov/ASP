﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        public IActionResult Register() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrerUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model); // Если данные в форме некоректны, то на доработку

            var new_user = new User         // Создаём нового пользователя
            {
                UserName = model.UserName
            };
            // Пытаемся зарегистрировать его в системе с указанным паролем
            var creation_result = await _UserManager.CreateAsync(new_user, model.Password);
            if (creation_result.Succeeded) // Если получилось
            {
                await _SignInManager.SignInAsync(new_user, false); // То сразу логиним его на сайте
                 
                return RedirectToAction("Index", "Home"); // и отправляем на главную страницу
            }

            foreach (var error in creation_result.Errors)         // Если что-то пошло не так...
                ModelState.AddModelError("", error.Description);  // Все ошибки заносим в состояние модели

            return View(model);                                   // И модель отправляем на доработку
        }

        public IActionResult Login() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid) return View(login);

            var login_result = await _SignInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, false);

            if (login_result.Succeeded)
            {
                _Logger.LogInformation("Пользователь {0} успешно вошёл в систему", login.UserName);

                if (Url.IsLocalUrl(login.ReturnUrl))
                    return Redirect(login.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Имя пользователя, или пароль неверны!");
            _Logger.LogWarning("Неудачная попытка входа пользователя {0} в систему", login.UserName);
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