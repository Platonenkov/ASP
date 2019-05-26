﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Filters;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        //[ActionFilterAsync]
        public IActionResult Index() => View();

        public IActionResult ContactUs() => View();

        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult ErrorStatus(string id)
        {
            switch (id)
            {
                case "404": return RedirectToAction("NotFoundPage");
                default: return Content($"Статусный код ошибки {id}");
            }
        }

        public IActionResult NotFoundPage() => View();
    }
}