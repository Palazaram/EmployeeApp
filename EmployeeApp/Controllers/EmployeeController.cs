using EmployeeApp.Models;
using EmployeeApp.Repositories.EmployeeRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Numerics;

namespace EmployeeApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee _employee;

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        public async Task<IActionResult> Index(string sterm = "") 
        {
            try
            {
                var data = await _employee.GetEmployeesAsync(sterm);
                return View(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            try
            {
                await _employee.AddEmployeeAsync(employee);
                var data = await _employee.GetEmployeesAsync();

                employee.Id = data.Max(p => p.Id);
                employee.Id++;

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id != null)
                {
                    Employee employee = new Employee { Id = id.Value };

                    if (employee != null)
                    {
                        await _employee.DeleteEmployeeAsync(employee);

                        return RedirectToAction("Index");
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id != null)
                {
                    Employee? employee = await _employee.GetEmployeeByIdAsync(id);
                    if (employee != null)
                    {
                        return View(employee);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            try
            {
                await _employee.UpdateEmployeeAsync(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
