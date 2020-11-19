using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryAnalyserApp.Models
{
    public class BatteryLevelTimestamp
    {
        public BatteryLevelTimestamp() { }

        public BatteryLevelTimestamp(double batteryLevel, DateTime timestamp)
        {
            this.batteryLevel = batteryLevel;
            this.timestamp = timestamp;
        }
        public double batteryLevel { get; set; }
        public DateTime timestamp { get; set; }
    }
}
