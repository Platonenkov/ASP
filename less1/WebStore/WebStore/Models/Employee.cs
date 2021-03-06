﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class Employee
    {
        [HiddenInput(DisplayValue=false)]
        /// <summary>
        /// Id сотрудника
        /// </summary>
        public int Id { get; set; }

        [Display(Name ="Имя"), Required(ErrorMessage="Test message")]
        [RegularExpression(@"(^[А-ЯЁ][а-яё]{2,150}$)|(^[A-z][a-z]{2,150}$)", ErrorMessage ="Некорректный формат имени")]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия"), Required(ErrorMessage = "Поле является обязательным")]
        [RegularExpression(@"(^[А-ЯЁ][а-яё]{2,150}$)|(^[A-z][a-z]{2,150}$)", ErrorMessage = "Некорректный формат Фамилии")]
        [MinLength(2)]
        public string SurName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime Date { get; set; }
    }
}
