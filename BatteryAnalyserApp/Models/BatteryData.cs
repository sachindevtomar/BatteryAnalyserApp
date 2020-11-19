using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryAnalyserApp.Models
{
    public class BatteryData
    {
        public BatteryData() { }

        public BatteryData(int academyId, double batteryLevel, string employeeId, string serialNumber, DateTime timestamp)
        {
            this.academyId = academyId;
            this.batteryLevel = batteryLevel;
            this.employeeId = employeeId;
            this.serialNumber = serialNumber;
            this.timestamp = timestamp;
        }
        public int academyId { get; set; }
        public double batteryLevel { get; set; }
        public string employeeId { get; set; }
        public string serialNumber { get; set; }
        public DateTime timestamp { get; set; }
    }
}
