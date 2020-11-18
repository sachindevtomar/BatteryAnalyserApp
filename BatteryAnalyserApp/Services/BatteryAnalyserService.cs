using BatteryAnalyserApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BatteryAnalyserApp.Services
{
    public class BatteryAnalyserService : IBatteryAnalyserService
    {
        public BatteryAnalyserService() { }

        public List<string> GetDevicesWithFaultyBatteries()
        {
            List<BatteryData> result;
            using (WebClient wc = new WebClient())
            {
                var batteryJSON = wc.DownloadString(Constants.BatteryJSONDownloadURL);
                result = JsonConvert.DeserializeObject<List<BatteryData>>(batteryJSON);
            }

            var data = new List<string>() { "sachin" };
            return data;
        }
    }
}
