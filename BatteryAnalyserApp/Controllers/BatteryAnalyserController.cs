using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BatteryAnalyserApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatteryAnalyserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> GetFaultyDevices()
        {
            var data = new List<string>() { "sachin"};
            return data.ToArray();
        }
    }
}