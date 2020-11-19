using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryAnalyserApp.Models
{
    public class AnalysedResult
    {
        public AnalysedResult(string serialNumber)
        {
            this.serialNumber = serialNumber;
        }
        public string serialNumber { get; set; }
        public double averageConsumption { get; set; }
        public BatteryStatus batteryStatus { get; set; }
    }
}
