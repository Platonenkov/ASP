using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployesController : Controller
    {

        private static List<Employee> __Employees = new List<Employee>
        {
            new Employee {Id=1, SurName="Иванов", FirstName="Иван", Patronymic="Иванович", Age=25, Date= new DateTime(2007, 7, 7) },
            new Employee {Id=2, SurName="Петров", FirstName="Пётр", Patronymic="Петрович", Age=30, Date= new DateTime(2008, 8, 8) },
            new Employee {Id=3, SurName="Сидоров", FirstName="Сидор", Patronymic="Сидорович", Age=33, Date= new DateTime(2009, 9, 9) }
        };


        public IActionResult Index()
        {
            return View(__Employees);
        }

        public IActionResult Details(int id)
        {
            var employe = __Employees.FirstOrDefault(e => e.Id == id);
            if (employe == null) return NotFound();

            return View(employe);
        }
    }
}