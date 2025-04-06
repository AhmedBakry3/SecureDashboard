using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDto;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models;
using Demo.Presentation.ViewModels.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentsController(IDepartmentService _departmentService,
        ILogger<DepartmentsController> _logger,
        IWebHostEnvironment _environment) : Controller
    {
        //Get BaseURL/Departments/Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = _departmentService.GetAllDepartments();    
            return View(Departments);
        }

        #region Create Department

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        //[ValidateAntiForgeryToken] //Action Filter
        public IActionResult Create(DepartmentViewModel departmentViewModel) 
        {
            if (ModelState.IsValid) //Server-Side Validation
            {
                try
                {
                    var DepartmentDto = new CreatedDepartmentDto()
                    {
                        Code = departmentViewModel.Code,
                        Name = departmentViewModel.Name,
                        DateOfCreation = departmentViewModel.DateOfCreation,
                        Description = departmentViewModel.Description,
                    };

                  int Result =  _departmentService.AddDepartment(DepartmentDto);
                    string message;
                    if (Result > 0)
                        message = $"Department {departmentViewModel.Name} Is Created successfully";
                    else
                        message = $"Department {departmentViewModel.Name} Can't Be Created";

                    TempData["Message"] = message ;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    { 
                        //1. Development => Log Error in console and Return same view with error message
                       ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        //2. Deployment => Log Error in File | Table in Database and return view Error 
                        _logger.LogError(ex.Message);
                    }
                }
            
            }
            return View(departmentViewModel);
        }

        #endregion

        #region Details of Department

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest(); //400
            var Department = _departmentService.GetDepartmentByID(id.Value);
            if(Department is null) return NotFound(); //404
            else return View(Department);
        }
        #endregion

        #region Edit Department

        [HttpGet]
        public IActionResult Edit(int? id) 
        {
            if (!id.HasValue) return BadRequest(); 
            var Department = _departmentService.GetDepartmentByID(id.Value);
            if (Department is null) return NotFound();
            var DepartmentViewModel = new DepartmentViewModel()
            {
                Name = Department.Name,
                Code = Department.Code,
                Description = Department.Description,
                DateOfCreation = Department.DateOfCreation
            };
            return View(DepartmentViewModel) ;
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, DepartmentViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var UpdatedDepartment = new UpdatedDepartmentDto()
                    {
                        Id = id ,
                        Name = viewModel.Name,
                        code = viewModel.Code,
                        Description = viewModel.Description,
                        DateOfCreation = viewModel.DateOfCreation
                    };
                    int Result = _departmentService.UpdateDepartment(UpdatedDepartment);
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department is not updated");
                    }
                }
            }
            catch (Exception ex)
            {

                if (_environment.IsDevelopment())
                {
                    //1. Development => Log Error in console and Return same view with error message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    //2. Deployment => Log Error in File | Table in Database and return view Error 
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
            return View(viewModel);
        }
        #endregion

        #region Delete Department

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest(); //400
            var Department = _departmentService.GetDepartmentByID(id.Value);
            if (Department is null) return NotFound(); //404
            else return View(Department);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id == 0) return BadRequest();
            try
            { 
                    bool IsDeleted = _departmentService.DeleteDepartment(id);
                    if (IsDeleted)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department is not Deleted");
                        return RedirectToAction(nameof(Delete), new { id });
                    }
                
            }

            catch (Exception ex)
            {

                if (_environment.IsDevelopment())
                {
                    //1. Development => Log Error in console and Return same view with error message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    //2. Deployment => Log Error in File | Table in Database and return view Error 
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }


        }
        #endregion
    }
}
