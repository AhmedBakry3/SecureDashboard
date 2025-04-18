using Demo.BusinessLogic.DataTransferObjects.DepartmentDto;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDto;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.Presentation.ViewModels.DepartmentViewModel;
using Demo.Presentation.ViewModels.EmployeeViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _EmployeeService,
        IWebHostEnvironment _environment,
        ILogger<EmployeesController> _logger) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName)
        {
            var Employees = _EmployeeService.GetAllEmployees(EmployeeSearchName);
            return View(Employees);
        }

        #region Create Employee

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    var EmployeeDto = new CreatedEmployeeDto()
                    {
                        Name = employeeViewModel.Name,
                        Email = employeeViewModel.Email,
                        EmployeeType = employeeViewModel.EmployeeType,
                        Address = employeeViewModel.Address,
                        Age = employeeViewModel.Age,
                        Gender = employeeViewModel.Gender,
                        HiringDate = employeeViewModel.HiringDate,
                        IsActive = employeeViewModel.IsActive,
                        Salary  = employeeViewModel.Salary,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        DepartmentId = employeeViewModel.DepartmentId,
                        Image = employeeViewModel.Image,
                    };
                    int Result = _EmployeeService.CreateEmployee(EmployeeDto);
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
            return View(employeeViewModel);
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

            var EmployeeViewModel = new EmployeeViewModel()
            {
                Name = Employee.Name,
                Email = Employee.Email,
                EmployeeType = Enum.Parse<EmployeeType>(Employee.EmployeeType.ToString()),
                Gender = Enum.Parse<Gender>(Employee.Gender.ToString())    ,
                Age = Employee.Age,
                HiringDate = Employee.HiringDate,
                IsActive = Employee.IsActive,
                Salary = Employee.Salary,
                PhoneNumber = Employee.PhoneNumber,
                DepartmentId = Employee.DepartmentId,
     
            };
            return View(EmployeeViewModel);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int? id , EmployeeViewModel employeeViewModel)
        {
            if(!id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return View(employeeViewModel);
            else
                try
                {
                    var EmployeeDto = new UpdatedEmployeeDto()
                    {
                        Id= id.Value,
                        Name = employeeViewModel.Name,
                        Email = employeeViewModel.Email,
                        EmployeeType = employeeViewModel.EmployeeType,
                        Address = employeeViewModel.Address,
                        Age = employeeViewModel.Age,
                        Gender = employeeViewModel.Gender,
                        HiringDate = employeeViewModel.HiringDate,
                        IsActive = employeeViewModel.IsActive,
                        Salary = employeeViewModel.Salary,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        DepartmentId = employeeViewModel.DepartmentId,
                    };
                    var Result = _EmployeeService.UpdateEmployee(EmployeeDto);
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
                        return View(employeeViewModel);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View("Error");
                    }
                }
            return View(employeeViewModel);
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
