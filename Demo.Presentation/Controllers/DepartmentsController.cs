using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.Services;
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
        public IActionResult Create(CreatedDepartmentDto departmentDto) 
        {
            if (ModelState.IsValid) //Server-Side Validation
            {
                try
                {
                  int Result =  _departmentService.AddDepartment(departmentDto);
                    if(Result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty , "Department Can't Be Created");
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
                    }
                }
            
            }
            return View(departmentDto);
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
            var DepartmentViewModel = new DepartmentEditViewModel()
            {
                Name = Department.Name,
                Code = Department.Code,
                Description = Department.Description,
                DateOfCreation = Department.CreatedOn
            };
            return View(DepartmentViewModel) ;
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, DepartmentEditViewModel viewModel)
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
                    int Resullt = _departmentService.UpdateDepartment(UpdatedDepartment);
                    if (Resullt > 0)
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
    }
}
