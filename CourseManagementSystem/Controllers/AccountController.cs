using Course.DAL.Models;
using CourseManagementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace CourseManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly SignInManager<ApplicationUsers> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> SignInManager
            , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = SignInManager;
            _roleManager = roleManager;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingUserByEmail != null)
            {
                ModelState.AddModelError(string.Empty, "Email Address is already in use.");
                return View(model);
            }
            string baseUserName = model.Email.Split('@')[0];
            string finalUserName = baseUserName;
            int counter = 1;
            while (await _userManager.FindByNameAsync(finalUserName) != null)
            {
                finalUserName = $"{baseUserName}{counter++}";
            }
            var user = new ApplicationUsers
            {
                UserName = finalUserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                Role = model.Roles
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var roleExists = await _roleManager.RoleExistsAsync(user.Role);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(user.Role));
                }

                await _userManager.AddToRoleAsync(user, user.Role);
                return RedirectToAction(nameof(LogIn));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(model);
        }
        public async new Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
    }
}
