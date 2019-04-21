﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Controllers.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Authorize]
    public class EmployesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployesController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }

        public IActionResult Index()
        {
            return View(_EmployeesData.GetAll());
        }

        public IActionResult Details(int id)
        {
            var employe = _EmployeesData.GetById(id);
            if (employe == null) return NotFound();

            return View("EmployeeView", employe);
        }

        [Authorize(Roles =Domain.Entities.User.RoleAdmin)]
        public IActionResult Edit(int? id)
        {
            Employee employee;
            if (id != null)
            {
                employee = _EmployeesData.GetById((int)id);
                if (employee is null)
                    return NotFound();
            }
            else
            {
                employee = new Employee();
            }
            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = Domain.Entities.User.RoleAdmin)]

        public IActionResult Edit(Employee employee, [FromServices] IMapper mapper)
        {
            if (employee.Age < 18) ModelState.AddModelError("Age", "Возраст слишком маленький");
            if (employee.Age > 120) ModelState.AddModelError("Age", "С возрастом что-то не так");

            if (!ModelState.IsValid) return View(employee);
            if (employee.Id > 0)
            {
                var db_employee = _EmployeesData.GetById(employee.Id);
                if (db_employee is null)
                    return NotFound();
                //AutoMapper.Mapper.Map(employee, db_employee); //automapper
                mapper.Map(employee, db_employee);
                //db_employee.FirstName = employee.FirstName;
                //db_employee.SurName = employee.SurName;
                //db_employee.Patronymic = employee.Patronymic;
                //db_employee.Age = employee.Age;
            }
            else
            {
                _EmployeesData.AddNew(employee);
            }
            _EmployeesData.SaveChanges();
            return RedirectToAction("Index");
        }


        [Authorize(Roles = Domain.Entities.User.RoleAdmin)]

        public IActionResult Delete(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee is null) return NotFound();
            _EmployeesData.Delete(id);
            return RedirectToAction("Index");
        }
    }
}