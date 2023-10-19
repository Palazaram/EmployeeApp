using EmployeeApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Numerics;

namespace EmployeeApp.Repositories.EmployeeRepository
{
    public class EmployeeManager : IEmployee
    {
        private readonly List<Employee> _employees;

        public EmployeeManager()
        {
            _employees = new List<Employee>() 
            {
                new Employee()
                {
                    Id = 1,
                    FirstName = "Іван",
                    LastName = "Петров",
                    Patronimyc = "Іванович",
                    Birthday = new DateTime(1985, 5, 15),
                    Department = "Продажі",
                    Position = "Менеджер",
                    Salary = 50000
                },
                new Employee()
                {
                    Id = 2,
                    FirstName = "Олена",
                    LastName = "Сидорова",
                    Patronimyc = "Андріївна",
                    Birthday = new DateTime(1990, 8, 22),
                    Department = "Маркетинг",
                    Position = "Менеджер",
                    Salary = 55000
                },
                new Employee()
                {
                    Id = 3,
                    FirstName = "Олексій",
                    LastName = "Іванов",
                    Patronimyc = "Олександрович",
                    Birthday = new DateTime(1980, 3, 10),
                    Department = "Розробка",
                    Position = "Програміст",
                    Salary = 60000
                },
                new Employee()
                {
                    Id = 4,
                    FirstName = "Марія",
                    LastName = "Смирнова",
                    Patronimyc = "Павлівна",
                    Birthday = new DateTime(1995, 11, 5),
                    Department = "Відділ кадрів",
                    Position = "HR",
                    Salary = 48000
                },
                new Employee()
                {
                    Id = 5,
                    FirstName = "Дмитро",
                    LastName = "Резнік",
                    Patronimyc = "Миколаєвич",
                    Birthday = new DateTime(1988, 7, 20),
                    Department = "Фінанси",
                    Position = "Аналітик",
                    Salary = 62000
                }
            };
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(string sTerm = "")
        {
            try
            {
                IEnumerable<Employee> employees = await Task.Run(() =>
                {
                    return (from employee in _employees
                            where string.IsNullOrWhiteSpace(sTerm)
                            || (employee != null && employee.LastName.ToLower().StartsWith(sTerm.ToLower()))
                            || (employee != null && employee.Department.ToLower().StartsWith(sTerm.ToLower()))
                            || (employee != null && employee.Position.ToLower().StartsWith(sTerm.ToLower()))
                            select new Employee
                            {
                                Id = employee.Id,
                                FirstName = employee.FirstName,
                                LastName = employee.LastName,
                                Patronimyc = employee.Patronimyc,
                                Birthday = employee.Birthday,
                                Department = employee.Department,
                                Position = employee.Position,
                                Salary = employee.Salary
                            });
                });

                return employees.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int? id)
        {
            try
            {
                Employee? employee = await Task.Run(() => _employees.FirstOrDefault(e => e.Id == id));
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                await Task.Run(() => _employees.Add(employee));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            try
            {
                var _employee = await GetEmployeeByIdAsync(employee.Id);

                if (_employee == null)
                {
                    throw new Exception("Employee not found");
                }

                await Task.Run(() => _employees.Remove(_employee));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                int index = _employees.FindIndex(e => e.Id == employee.Id);
                if (index != -1)
                {
                    await Task.Run(() => _employees[index] = employee);
                }
                else
                {
                    throw new InvalidOperationException("The employee with the specified ID was not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
