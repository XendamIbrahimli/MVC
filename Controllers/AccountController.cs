using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniqloMVC.Enums;
using UniqloMVC.Models;
using UniqloMVC.ViewModel.Auths;

namespace UniqloMVC.Controllers
{
    public class AccountController(UserManager<User> _userManager,SignInManager<User> _signInManager) : Controller
    {
        bool IsAuthenticated=>User.Identity?.IsAuthenticated ?? false;
        public IActionResult Register()
        {
            if (IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserCreateVM vm)
        {
            if(IsAuthenticated)
                return RedirectToAction("Index","Home");
            if(!ModelState.IsValid)
                return View();
            User user = new User
            {
                Email = vm.Email,
                Fullname=vm.Fullname,
                UserName=vm.Username,
                ProfileImageUrl="photo.jpg"
            };
            var result= await _userManager.CreateAsync(user, vm.Password);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            var roleResult = await _userManager.AddToRoleAsync(user, nameof(Roles.User));
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Login()
        {
            if (IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
                return View();

            User? user = null;
            if (vm.UsernameOrEmail.Contains('@'))
                user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
            else
                user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);
            if (user is null)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View();
            }

            var result=await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
            if (!result.Succeeded) 
            {
                if (result.IsNotAllowed)
                    ModelState.AddModelError("", "Username or password is wrong");
                if(result.IsLockedOut)
                    ModelState.AddModelError("","Wait until"+user.LockoutEnd!.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                return View();
            }

            if (string.IsNullOrEmpty(ReturnUrl))
            {
                if(await _userManager.IsInRoleAsync(user,"Admin"))
                {
                    return RedirectToAction("Index", new { Controller = "Dashboard", Area = "Admin" });
                }
                return RedirectToAction("Index", "Home");
            }

                return LocalRedirect(ReturnUrl);
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
