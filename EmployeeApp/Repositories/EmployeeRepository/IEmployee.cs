using EmployeeApp.Models;

namespace EmployeeApp.Repositories.EmployeeRepository
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(string sTerm = "");

        Task<Employee> GetEmployeeByIdAsync(int? id);

        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Employee employee);
    }
}
