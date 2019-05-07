using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Models;
using WebStore.Interfaces.Servcies;


namespace WebStore.Controllers.Implementations
{
    public class InMemoryEmployeesData : IEmployeesData
    {

        private List<Employee> _Employees = new List<Employee>
        {
            new Employee {Id=1, SurName="Иванов", FirstName="Иван", Patronymic="Иванович", Age=25},
            new Employee {Id=2, SurName="Петров", FirstName="Пётр", Patronymic="Петрович", Age=30 },
            new Employee {Id=3, SurName="Сидоров", FirstName="Сидор", Patronymic="Сидорович", Age=33 }
        };

        public void AddNew(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));
            if(_Employees.Contains(employee)||_Employees.Any(e=>e.Id==employee.Id)) return;
            employee.Id = _Employees.Count == 0 ? 1 : _Employees.Max(e => e.Id) + 1;
            _Employees.Add(employee);
        }

        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee is null) return;
            _Employees.Remove(employee);
        }

        public IEnumerable<Employee> GetAll() => _Employees;

        public Employee GetById(int id) => _Employees.FirstOrDefault(e => e.Id == id);

        public void SaveChanges(){}
    }
}
