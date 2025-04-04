using Demo.BusinessLogic.DataTransferObjects.DepartmentDto;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDto;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
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

        #region Edit Employee
        [HttpGet]
        public IActionResult Edit(int? id) 
        {
            if (!id.HasValue) return BadRequest();
            var Employee = _EmployeeService.GetEmployeeByID(id.Value);
            if (Employee is null) return NotFound();

            var EmployeeDto = new UpdatedEmployeeDto
            {
                Id = Employee.Id,
                Email = Employee.Email,
                Address = Employee.Address,
                Name =Employee.Name,
                Age=Employee.Age,
                HiringDate=Employee.HiringDate,
                IsActive=Employee.IsActive,
                PhoneNumber=Employee.PhoneNumber,
                Salary=Employee.Salary,
                Gender = Enum.Parse<Gender>(Employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(Employee.EmployeeType)

            };
            return View(EmployeeDto);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int? id , UpdatedEmployeeDto employeeDto)
        {
            if(!id.HasValue || id!= employeeDto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(employeeDto);
            else
                try
                {
                    var Result = _EmployeeService.UpdateEmployee(employeeDto);
                    if (Result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't Be Updated");
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(employeeDto);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View("Error");
                    }
                }
            return View(employeeDto);
        }
        #endregion

        #region Delete Employee

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool IsDeleted = _EmployeeService.DeleteEmployee(id);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee is not Deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }

            }

            catch (Exception ex)
            {

                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
            #endregion
        }
    }
