using Microsoft.AspNetCore.Mvc;
using TrafficPenaltyManagement.Application.Dtos.EmployeeDtos;
using TrafficPenaltyManagement.Application.Interfaces;
using TrafficPenaltyManagement.Application.Services.EmployeeServices;

namespace TrafficPenaltyManagement.WebUI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IVehicleRepository _vehicleRepository;

        public EmployeeController(
            IEmployeeService employeeService,
            IVehicleRepository vehicleRepository)
        {
            _employeeService = employeeService;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IActionResult> EmployeeList(string department = "", string branch = "")
        {
            var employees = await _employeeService.GetEmployeesAsync(department, branch);

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> CreateEmployee()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();

            ViewBag.Vehicles = vehicles.Select(x => new
            {
                Id = x.Id,
                Plate = x.Plate
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                var vehicles = await _vehicleRepository.GetAllAsync();

                ViewBag.Vehicles = vehicles.Select(x => new
                {
                    Id = x.Id,
                    Plate = x.Plate
                }).ToList();

                return View(createEmployeeDto);
            }

            var employee = await _employeeService.CreateEmployeeAsync(createEmployeeDto);

            if (employee == null)
            {
                ModelState.AddModelError("", "Çalışan eklenirken bir hata oluştu.");

                var vehicles = await _vehicleRepository.GetAllAsync();

                ViewBag.Vehicles = vehicles.Select(x => new
                {
                    Id = x.Id,
                    Plate = x.Plate
                }).ToList();

                return View(createEmployeeDto);
            }

            return RedirectToAction("EmployeeList");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var vehicles = await _vehicleRepository.GetAllAsync();

            ViewBag.Vehicles = vehicles.Select(x => new
            {
                Id = x.Id,
                Plate = x.Plate
            }).ToList();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateEmployeeDto);
            }

            await _employeeService.UpdateEmployeeAsync(updateEmployeeDto);

            return RedirectToAction(nameof(EmployeeList));
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeDetail(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

    }
}