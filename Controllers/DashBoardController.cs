using Microsoft.AspNetCore.Mvc;
using SmartHome.Models.Entity;
using SmartHome.Extend;
namespace SmartHome.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly SmartHomeContext _context;

        public DashBoardController(SmartHomeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var listState = GetStateData(200);


            return View(listState);
        }
        public StateData GetStateData(int n)
        {
            var listItem = _context.SecurityLogs.Take(n);
            var state = new StateData();
            state.temperature = listItem.Select(i => (float)i.Temperature).ToList();
            state.humidity = listItem.Select(i => (float)i.Humidity).ToList();
            state.water = listItem.Select(i => (float)i.Water).ToList();
            state.gas = listItem.Select(i => (float)i.Gas).ToList();
            state.light = listItem.Select(i => (float)i.Light).ToList();
            return state;

        }
    }
    
}
