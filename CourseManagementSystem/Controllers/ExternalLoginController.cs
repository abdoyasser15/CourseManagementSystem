using Course.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseManagementSystem.Controllers
{
    public class ExternalLoginController : Controller
    {

        private readonly SignInManager<ApplicationUsers> _signInManager;
        private readonly UserManager<ApplicationUsers> _userManager;

        public ExternalLoginController(SignInManager<ApplicationUsers> signInManager, UserManager<ApplicationUsers> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action("ExternalLoginCallback");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }

        public IActionResult FacebookLogin()
        {
            var redirectUrl = Url.Action("ExternalLoginCallback");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return Challenge(properties, "Facebook");
        }

        public async Task<IActionResult> ExternalLoginCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction("Login", "Account");

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            // First time login
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            var user = new ApplicationUsers
            {
                UserName = email,
                Email = email,
                Role = "Student" // 👈 علشان العمود مش nullable
            };

            var createResult = await _userManager.CreateAsync(user);
            if (createResult.Succeeded)
            {
                await _userManager.AddLoginAsync(user, info);

                // 👈 إضافة المستخدم إلى دور Identity (افتراضي Student)
                await _userManager.AddToRoleAsync(user, "Student");

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            // لو في أخطاء أثناء الإنشاء
            foreach (var error in createResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToAction("Login", "Account");
        }


    }
}
