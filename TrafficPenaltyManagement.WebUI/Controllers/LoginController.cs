using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrafficPenaltyManagement.Application.Dtos.LoginDtos;
using TrafficPenaltyManagement.Infrastructure.Identitiy;

namespace TrafficPenaltyManagement.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return View(loginDto);

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email veya şifre hatalı.");
                return View(loginDto);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user,
                loginDto.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email veya şifre hatalı.");
                return View(loginDto);
            }

            return RedirectToAction("PenaltyList", "Penalty");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("SignIn");
        }
    }
}