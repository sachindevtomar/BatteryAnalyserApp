using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryAnalyserApp.Services
{
    public interface IWebClientWrapper
    {
        public string DownloadString(string url);
    }
}
