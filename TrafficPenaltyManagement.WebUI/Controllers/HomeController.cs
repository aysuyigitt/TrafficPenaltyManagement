using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrafficPenaltyManagement.Application.Services.DashboardService;
using TrafficPenaltyManagement.WebUI.Models;

namespace TrafficPenaltyManagement.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboardService _dashboardService;

        public HomeController(ILogger<HomeController> logger, IDashboardService service)
        {
            _logger = logger;
            _dashboardService = service;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard = await _dashboardService.GetDashboardDataAsync();
            return View(dashboard);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
