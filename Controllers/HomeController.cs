using Microsoft.AspNetCore.Mvc;
using SmartHome.Models;
using SmartHome.Models.Entity;
using System.Diagnostics;

namespace SmartHome.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SmartHomeContext _context;

        public HomeController(ILogger<HomeController> logger, SmartHomeContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            print(CurrentUser);
            return View();
        }

        public async Task<IActionResult> Camera()
        {
            var listLog = _context.Logs.OrderByDescending(x=> x.Id).Take(10).ToList();
            return View(listLog);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}