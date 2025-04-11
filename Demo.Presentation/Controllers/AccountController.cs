using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels.AccountViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager) : Controller
    {
        #region Register Action
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(RegisterViewModel ViewModel)
        {
            if (!ModelState.IsValid) return View(ViewModel);

            var User = new ApplicationUser()
            {
                FirstName = ViewModel.FirstName,
                LastName = ViewModel.LastName,
                UserName = ViewModel.UserName,
                Email = ViewModel.Email,
            };

            var Result = _userManager.CreateAsync(User , ViewModel.Password).Result;
            if (Result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var Error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, Error.Description);
                }
                return View(ViewModel);
            }
        }
        #endregion
        //Login
        //Sign Out
    }
}
