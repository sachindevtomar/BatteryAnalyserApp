using BatteryAnalyserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryAnalyserApp.Services
{
    public interface IBatteryAnalyserService
    {
        public List<AnalysedResult> GetDevicesStatusWithAverage();
    }
}
