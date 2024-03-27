using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SmartHome.Extend
{
    public class StateData
    {
        // public StateData(List<float> temperture, List<float> humidity, List<float> water, List<float> gas, List<float> light)
        // {
        //     this.temperture = temperture;
        //     this.humidity = humidity;
        //     this.water = water;
        //     this.gas = gas;
        //     this.light = light;
        // }
        public List<float> temperature { get; set; }
        public List<float> humidity { get; set; }
        public List<float> water { get; set; }
        public List<float> gas { get; set; }
        public List<float> light { get; set; }
    }
    public class Notice
    {
        public string thongBao { get; set; }
    }
}
public class BaseController : Controller
{
    public string CurrentUser
    {
        get
        {
            return HttpContext.Session.GetString("UserName");

        }
        set
        {
            HttpContext.Session.SetString("UserName", value);
        }
    }
    public bool IsLogin
    {
        get
        {
            return string.IsNullOrEmpty(CurrentUser);
        }
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);
    }
    public static void print(string s)
    {
        System.Console.WriteLine("=== start");
        System.Console.WriteLine(s);
        System.Console.WriteLine("== end ==");
    }
}
