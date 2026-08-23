using Microsoft.AspNetCore.Mvc;
using TrafficPenaltyManagement.Application.Dtos.VehicleDtos;
using TrafficPenaltyManagement.Application.Services.VehicleSerivces;

namespace TrafficPenaltyManagement.WebUI.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _service;

        public VehicleController(IVehicleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> VehicleList()
        {
            var value = await _service.GetAllVehiclesAsync();
            return View(value);
        }

        [HttpGet]
        public IActionResult CreateVehicle()
        {         
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(CreateVehicleDto createVehicleDto)
        {
            var value = await _service.CreateVehicleAsync(createVehicleDto);

            if (value == null)
            {
                ModelState.AddModelError("Plate", "Bu plaka ile kayıtlı bir araç zaten bulunmaktadır.");

                return View(createVehicleDto);
            }

            return RedirectToAction("VehicleList");
        }
    }
}
