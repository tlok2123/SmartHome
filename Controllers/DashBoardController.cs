using Microsoft.AspNetCore.Mvc;
using SmartHome.Models.Entity;
using SmartHome.Extend;
using API_Mail.Extend;
namespace SmartHome.Controllers
{
    public class DashBoardController : BaseController
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
        [HttpGet]
        public async void AddLogAPI()
        {
            string status = Request.Query["status"];
            DateTime time = DateTime.Now;
            Log log = new Log() { Status = status, Time = time };
            
            _context.Add(log);
            // print(log.Status);
            // print(log.Time.ToString());
             _context.SaveChanges();


        }
        public StateData GetStateData(int n)
        {
            var listItem = _context.SecurityLogs.OrderByDescending(l => l.Id).Take(n);
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
