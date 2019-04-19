using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View(); //Content("Hello"); 
        public IActionResult NotFound()=> View();
        public IActionResult Blog()=> View();
        public IActionResult BlogSingle()=> View();
        public IActionResult Checkout()=> View();
        public IActionResult ContactUs()=> View();
        
    }
}