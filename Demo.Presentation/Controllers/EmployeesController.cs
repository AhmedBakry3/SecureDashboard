using Demo.BusinessLogic.DataTransferObjects.DepartmentDto;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDto;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _EmployeeService,
        IWebHostEnvironment _environment,
        ILogger<EmployeesController> _logger) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _EmployeeService.GetAllEmployees();
            return View(Employees);
        }

        #region Create Employee

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    int Result = _EmployeeService.CreateEmployee(employeeDto);
                    if (Result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't Be Created");
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }

            }
            return View(employeeDto);
        }
        #endregion

        #region Details Of Employees
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest(); 
            var Employee = _EmployeeService.GetEmployeeByID(id.Value);
            return Employee is null ?  NotFound() : View(Employee);
        }
        #endregion
    }
}
