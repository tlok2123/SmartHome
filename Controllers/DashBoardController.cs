using Microsoft.AspNetCore.Mvc;
using SmartHome.Models.Entity;
using SmartHome.Extend;
using API_Mail.Extend;
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
            var listState = GetStateData(50);


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
        public SecurityLog LastStateData()
        {
            var last = _context.SecurityLogs.OrderByDescending(l => l.Id).FirstOrDefault();
            return last;
        }
       
        public string IsWarning(int n)
        {
            var last = _context.SecurityLogs.OrderByDescending(l => l.Id).Take(n).Select(l => l.State).ToList();
            var check = true;
            foreach (var item in last)
            {
                if (item != last[0])
                {
                    check = false;
                }
            }
            if (check)
            {
                return last[0];
            }
            else
            {
                return "Stable";
            }

        }

    }

}
