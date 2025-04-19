using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Models.UserManagerModel;
using Demo.Presentation.Utilities;
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

            var User = new UserManager()
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

        #region Login Action
        //Login
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

        #region SignOut Action
        //Sign Out
        [HttpGet]
        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region Forget Password Action

        [HttpGet]
        public IActionResult ForgetPassword() => View();

        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
         if (ModelState.IsValid)
            {
                var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if(User is not null)
                {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(User).Result;
                    var ResetPassword= Url.Action("ResetPassword", "Account", new { email = viewModel.Email , Token }, Request.Scheme);
                    var Email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = ResetPassword
                    };
                    EmailSettings.SendEmail(Email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
         
            ModelState.AddModelError(string.Empty, "Invalid Operation");

            return View(nameof(ForgetPassword),viewModel);

        }
        #endregion

        #region CheckYourInbox Action

        [HttpGet]
        public IActionResult CheckYourInbox() => View();
        #endregion

        #region ResetPassword Action
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
           TempData["Email"] = email;
           TempData["Token"] = token;
           return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewmodel)
        {
            if(!ModelState.IsValid) return View(viewmodel);
            else
            {
                var Email = TempData["Email"] as string;
                var Token = TempData["Token"] as string;
                if (Email is not null && Token is not null)
                {
                    var User = _userManager.FindByEmailAsync(Email).Result;
                    if (User is not null)
                    {
                        var Result = _userManager.ResetPasswordAsync(User, Token, viewmodel.Password).Result;
                        if (Result.Succeeded)
                        {
                            return RedirectToAction(nameof(Login));
                        }
                        else
                        {
                            foreach (var Error in Result.Errors)
                                ModelState.AddModelError(string.Empty, Error.Description);
                        }
                    }
                }
                return View(nameof(ResetPassword), viewmodel);

            }
        }
        #endregion
    }
}
