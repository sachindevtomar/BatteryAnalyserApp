using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BatteryAnalyserApp.Services
{
    public class WebClientWrapper : IWebClientWrapper
    {
        private WebClient client;
        public WebClientWrapper() 
        {
            this.client = new WebClient();
        }

        public string DownloadString(string url)
        {
            return this.client.DownloadString(Constants.BatteryJSONDownloadURL);
        }
    }
}
