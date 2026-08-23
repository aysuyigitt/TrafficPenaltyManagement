using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrafficPenaltyManagement.Application.Dtos.LoginDtos;
using TrafficPenaltyManagement.Infrastructure.Identitiy;

namespace TrafficPenaltyManagement.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return View(registerDto);

            var user = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                Name = registerDto.Name,
                Role = registerDto.Role
            };

            var result = await _userManager.CreateAsync(
                user,
                registerDto.Password);

            if (result.Succeeded){
                return RedirectToAction("SignIn", "Login");
            }

            foreach (var error in result.Errors){
                ModelState.AddModelError("", error.Description);
            }

            return View(registerDto);
        }
    }
}