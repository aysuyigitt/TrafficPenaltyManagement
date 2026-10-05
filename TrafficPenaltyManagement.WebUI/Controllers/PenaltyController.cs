using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrafficPenaltyManagement.Application.Dtos.PenaltyDtos;
using TrafficPenaltyManagement.Application.Services.EmployeeServices;
using TrafficPenaltyManagement.Application.Services.PenaltyServices;
using TrafficPenaltyManagement.Application.Services.PenaltyTypeServices;
using TrafficPenaltyManagement.Infrastructure.Identitiy;

namespace TrafficPenaltyManagement.WebUI.Controllers
{
    public class PenaltyController : Controller
    {
        private readonly IPenaltyService _penaltyService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeService _employeeService;
        private readonly IPenaltyTypeService _penaltyTypeService;

        public PenaltyController(IPenaltyService penaltyService, UserManager<ApplicationUser> userManager, IEmployeeService employeeService, IPenaltyTypeService penaltyTypeService)
        {
            _penaltyService = penaltyService;
            _userManager = userManager;
            _employeeService = employeeService;
            _penaltyTypeService = penaltyTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> PenaltyList()
        {
            var penalties = await _penaltyService.GetAllPenaltiesAsync();

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            ViewBag.UserRole = user.Role;

            return View(penalties);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePenalty()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            var penaltyTypes = await _penaltyTypeService.GetAllPenaltyTypeAsync();

            ViewBag.Employees = employees;
            ViewBag.PenaltyTypes = penaltyTypes;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePenalty(CreatePenaltyDto createPenaltyDto)
        {
            await _penaltyService.CreatePenaltyAsync(createPenaltyDto);
            return RedirectToAction("PenaltyList");
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null){
                return RedirectToAction("SignIn", "Login");
            }

            try
            {
                await _penaltyService.ApprovePenaltyAsync(id,user.Id,user.Role);

                TempData["SuccessMessage"] = "Ceza başarıyla onaylandı.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("PenaltyList");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id,string rejectionReason)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null){
                return RedirectToAction("SignIn", "Login");
            }

            try{
                await _penaltyService.RejectPenaltyAsync(id,rejectionReason,user.Id,user.Role);
                TempData["SuccessMessage"] = "Ceza reddedildi.";
            }
            catch (InvalidOperationException ex){
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("PenaltyList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePenalty(int id)
        {
            var penalty = await _penaltyService.GetPenaltyByIdAsync(id);

            if (penalty == null)
            {
                return NotFound();
            }

            var employees = await _employeeService.GetAllEmployeesAsync();
            var penaltyTypes = await _penaltyTypeService.GetAllPenaltyTypeAsync();

            ViewBag.Employees = employees;
            ViewBag.PenaltyTypes = penaltyTypes;

            var updateDto = new UpdatePenaltyDto
            {
                Id = penalty.Id,
                Plate = penalty.Plate,
                Amount = penalty.Amount,
                PenaltyDate = penalty.PenaltyDate,
                PenaltyTypeId = penalty.PenaltyTypeId
            };

            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePenalty(UpdatePenaltyDto updatePenaltyDto)
        {
            await _penaltyService.UpdatePenaltyAsync(updatePenaltyDto);

                return RedirectToAction("PenaltyList");
            
            }
        


        [HttpGet]
        public async Task<IActionResult> History(int id)
        {
            var histories = await _penaltyService.GetPenaltyHistoryAsync(id);

            foreach (var history in histories)
            {
                var user = await _userManager.FindByIdAsync(history.UserId);

                if (user != null)
                {
                    history.UserName = user.Name;
                }
            }

            return View(histories);
        }
    }
    }

