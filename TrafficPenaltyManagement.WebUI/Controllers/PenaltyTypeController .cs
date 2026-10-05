using Microsoft.AspNetCore.Mvc;
using TrafficPenaltyManagement.Application.Dtos.PenaltyDtos;
using TrafficPenaltyManagement.Application.Services.PenaltyTypeServices;

namespace TrafficPenaltyManagement.WebUI.Controllers
{
    public class PenaltyTypeController : Controller
    {
        private readonly IPenaltyTypeService _penaltyTypeService;

        public PenaltyTypeController(IPenaltyTypeService penaltyTypeService)
        {
            _penaltyTypeService = penaltyTypeService;
        }

        public async Task<IActionResult> PenaltyTypeList()
        {
            var penaltyTypes = await _penaltyTypeService.GetAllPenaltyTypeAsync();

            return View(penaltyTypes);
        }

        [HttpGet]
        public IActionResult CreatePenaltyType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePenaltyType(CreatePenaltyTypeDto createPenaltyTypeDto)
        {

            await _penaltyTypeService.CreatePenaltyTypeAsync(createPenaltyTypeDto);
            return RedirectToAction("PenaltyTypeList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePenaltyType(int id)
        {
            var penaltyType = await _penaltyTypeService.GetByIdAsync(id);
            return View(penaltyType);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePenaltyType(UpdatePenaltyTypeDto updatePenaltyTypeDto)
        {
            await _penaltyTypeService.UpdatePenaltyTypeAsync(updatePenaltyTypeDto);

            return RedirectToAction("PenaltyTypeList");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePenaltyType(int id)
        {
            await _penaltyTypeService.DeletePenaltyTypeAsync(id);

            return RedirectToAction("PenaltyTypeList");
        }
    }
}