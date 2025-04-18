using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels.AccountViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager , SignInManager<ApplicationUser> _signInManager) : Controller
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
        #region Login Action
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            else
            {
                var User = _userManager.FindByEmailAsync(viewModel.Email).GetAwaiter().GetResult();
                if (User is not null)
                {
                    var Flag = _userManager.CheckPasswordAsync(User, viewModel.Password).GetAwaiter().GetResult();

                    if (Flag)
                    {
                        var Result = _signInManager.PasswordSignInAsync(User, viewModel.Password, viewModel.RememberMe, false).GetAwaiter().GetResult();
                        if (Result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Login!");
                        return View(viewModel);
                    }

                }
                return View(viewModel);

            }
        }
        #endregion

        //Sign Out
    }
}
