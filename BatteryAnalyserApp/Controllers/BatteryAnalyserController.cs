using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BatteryAnalyserApp.Models;
using BatteryAnalyserApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BatteryAnalyserApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatteryAnalyserController : ControllerBase
    {
        private readonly IBatteryAnalyserService batteryAnalyserService;
        public BatteryAnalyserController(IBatteryAnalyserService batteryAnalyserService)
        {
            this.batteryAnalyserService = batteryAnalyserService ?? throw new ArgumentNullException(nameof(batteryAnalyserService));
        }

        [HttpGet]
        public IEnumerable<AnalysedResult> GetFaultyDevices()
        {
            List<AnalysedResult> result;
            try
            {
                result = this.batteryAnalyserService.GetDevicesStatusWithAverage();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception Occured: " + ex.Message);
                return null;
            }
            return result;
        }
    }
}