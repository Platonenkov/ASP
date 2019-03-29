using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class Employee
    {
        /// <summary>
        /// Id сотрудника
        /// </summary>
        public int Id { get; set; }


        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }

        public DateTime Date { get; set; }
    }
}
