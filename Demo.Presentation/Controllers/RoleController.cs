using Demo.BusinessLogic.DataTransferObjects.EmployeeDto;
using Demo.BusinessLogic.DataTransferObjects.RoleDto;
using Demo.BusinessLogic.DataTransferObjects.UserDto;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.RoleManagerModel;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.Presentation.ViewModels.EmployeeViewModel;
using Demo.Presentation.ViewModels.RoleViewModel;
using Demo.Presentation.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    [Authorize]
    public class RoleController(IRoleService _roleService,
                        IWebHostEnvironment _environment,
                        ILogger<UserController> _logger) : Controller
    {
        #region Index Action
        [HttpGet]
        public IActionResult Index(string? RoleSearchName)
        {
            var Roles = _roleService.GetAllRoles(RoleSearchName);
            return View(Roles);
        }
        #endregion

        #region Create Action
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(RoleViewModel RoleviewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var RoleDto = new CreatedRoleDto()
                    {
                        Id = RoleviewModel.Id,
                        RoleName = RoleviewModel.roleName,
                    };
                    int Result = _roleService.CreateRole(RoleDto);
                    if (Result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Role Can't Be Created");
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
            return View(RoleviewModel);
        }
        #endregion

        #region Details Action
        public IActionResult Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            var Role = _roleService.GetRoleByID(id);
            return Role is null ? NotFound() : View(Role);
        }
        #endregion

        #region Edit Action
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id is null) return BadRequest();
            var Role = _roleService.GetRoleByID(id);
            if (Role is null) return NotFound();

            var roleViewModel = new RoleViewModel()
            {
                Id = Role.Id,
                roleName = Enum.Parse<RoleName>(Role.RoleName.ToString()),
            };
            return View(roleViewModel);
        }
        [HttpPost]
        public IActionResult Edit(string id, RoleViewModel roleViewModel)
        {
            if (id is null) return BadRequest();
            if (!ModelState.IsValid) return View(roleViewModel);
            else
                try
                {
                    var roleDto = new UpdatedRoleDto()
                    {
                        Id = roleViewModel.Id,
                        RoleName = Enum.Parse<RoleName>(roleViewModel.roleName.ToString()),
                    };
                    var Result = _roleService.UpdateRole(roleDto);
                    if (Result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User Can't Be Updated");
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(roleViewModel);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View("Error");
                    }
                }
            return View(roleViewModel);
        }

        #endregion

        #region Delete Action
        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (id is null) return BadRequest();
            var Role = _roleService.GetRoleByID(id);
            if (Role is null) return NotFound();
            else return View(Role);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmation(string id)
        {
            if (id is null) return BadRequest();
            try
            {
                bool IsDeleted = _roleService.DeleteRole(id);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Role is not Deleted");
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



            #endregion
        }
    }
}