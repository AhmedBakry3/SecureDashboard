using Demo.BusinessLogic.DataTransferObjects.EmployeeDto;
using Demo.BusinessLogic.DataTransferObjects.UserDto;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.DataAccess.Models.UserManagerModel;
using Demo.Presentation.ViewModels.EmployeeViewModel;
using Demo.Presentation.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    [Authorize]
    public class UserController(IUserService _userService,
                IWebHostEnvironment _environment,
                ILogger<UserController> _logger) : Controller
    {
        #region Index Action
        [HttpGet]
        public IActionResult Index(string? UserSearchName)
        {
            var Users = _userService.GetAllUsers(UserSearchName);
            return View(Users);
        }
        #endregion

        #region Details Action
        [HttpGet]
        public IActionResult Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            var User = _userService.GetUserByID(id);
            return User is null ? NotFound() : View(User);
        }
        #endregion

        #region Edit Action
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id is null) return BadRequest();
            var User = _userService.GetUserByID(id);
            if (User is null) return NotFound();

            var UserViewModel = new UserViewModel()
            {
                Id = User.Id,
                Fname = User.Fname,
                Lname = User.Lname,
                PhoneNumber = User.PhoneNumber,
            };
            return View(UserViewModel);
        }
        [HttpPost]
        public IActionResult Edit(string id, UserViewModel userViewModel)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            if (!ModelState.IsValid) return View(userViewModel);
            else
                try
                {
                    var UserDto = new UpdatedUserDto()
                    {
                        Id = userViewModel.Id,
                        Fname = userViewModel.Fname,
                        Lname = userViewModel.Lname,
                        PhoneNumber = userViewModel.PhoneNumber,
                    };
                    var Result = _userService.UpdateUser(UserDto);
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
                        return View(userViewModel);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View("Error");
                    }
                }
            return View(userViewModel);
        }

        #endregion

        #region Delete Action
        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            var User = _userService.GetUserByID(id);
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();
            else return View(User);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmation(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            try
            {
                bool IsDeleted = _userService.DeleteUser(id);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "User is not Deleted");
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
