using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc_day2.Models;
using Mvc_day2.ViewModel;

namespace Mvc_day2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationIdentityUser> userManager;
        private readonly SignInManager<ApplicationIdentityUser> signInManager;

        public AccountController(UserManager<ApplicationIdentityUser>userManager,SignInManager<ApplicationIdentityUser>signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel userVM)
        {
            if(ModelState.IsValid)
            {
                var userModel = new ApplicationIdentityUser();
                userModel.UserName=userVM.UserName;
                userModel.PasswordHash = userVM.Password;
                userModel.Address = userVM.Address;
              IdentityResult result= await userManager.CreateAsync(userModel, userVM.Password);
                if (result.Succeeded)
                {
                   await userManager.AddToRoleAsync(userModel, "Student");
                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", result.Errors.FirstOrDefault().Description);
                }
            }
            return View(userVM);

        }
      
        /// ////////////////////////////////////////////////////
       
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task< IActionResult> LogIn(LogInUserViewModel userVm)
        {
            if(ModelState.IsValid)
            {
                var userModel = await userManager.FindByNameAsync(userVm.UserName);
                if(userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, userVm.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(userModel, userVm.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "userNAme or Password incorrect");
                
            }
            return View(userVm);
        }

        /// ////////////////////////////////////////////////////
        [Authorize]
        public IActionResult SignOut()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
