using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
}