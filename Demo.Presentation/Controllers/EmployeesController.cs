using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _EmployeeService) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _EmployeeService.GetAllEmployees();
            return View(Employees);
        }
    }
}
