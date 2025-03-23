using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.Services;
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
    }
}
