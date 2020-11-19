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
        private readonly IWebClientWrapper webClientWrapper;
        public BatteryAnalyserService(IWebClientWrapper webClientWrapper) {
            this.webClientWrapper = webClientWrapper ?? throw new ArgumentNullException(nameof(webClientWrapper));
        }

        public List<AnalysedResult> GetDevicesStatusWithAverage()
        {
            List<BatteryData> batteryData;
            List<AnalysedResult> finalBatteryStatusList = new List<AnalysedResult>();
            try
            {
                //Create WebClient for downloading the battery data from Github
                var batteryJSON = this.webClientWrapper.DownloadString(Constants.BatteryJSONDownloadURL);
                batteryData = JsonConvert.DeserializeObject<List<BatteryData>>(batteryJSON);

                //Group data based on SerialNumber
                var groupedBatteryData = from bd in batteryData group bd by bd.serialNumber;

                //iterate each group        
                foreach (var serialNumberGroup in groupedBatteryData)
                {
                    //Get the battery status for every group
                    finalBatteryStatusList.Add(GetBatteryStatusOfSerialNumberGroup(serialNumberGroup.ToList()));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception Occured: " + ex.Message);
            }
            return finalBatteryStatusList;
        }

        private AnalysedResult GetBatteryStatusOfSerialNumberGroup(List<BatteryData> groupedBatteryData)
        {
            var analysedResult = new AnalysedResult(groupedBatteryData.FirstOrDefault().serialNumber);

            //Order by timestamp 
            var orderedGroupedBatteryData = groupedBatteryData.OrderBy(batteryData => batteryData.timestamp).ToList();

            //Initialization of required vars
            var previousBatteryLevel = 1.00;

            var continousDecrementedBatteryLevel = new List<BatteryLevelTimestamp>();
            var batteryLevelAvgTillIncrement = new List<double>();

            //Iterate over the list
            orderedGroupedBatteryData.ForEach(batteryData => { 

                if(batteryData.batteryLevel <= previousBatteryLevel)
                {
                    //Add the Object in the continous decremented battery level list
                    continousDecrementedBatteryLevel.Add(new BatteryLevelTimestamp(batteryData.batteryLevel, batteryData.timestamp));
                }
                else
                {
                    //Check if there are required elements available to find the average
                    if (continousDecrementedBatteryLevel.Count >= 2)
                    {
                        var percentageDecreaseInADay = GetPercentageDecreaseInADay(continousDecrementedBatteryLevel);

                        batteryLevelAvgTillIncrement.Add(percentageDecreaseInADay);
                        //Clear the continous list as we found the increment in the battery
                        continousDecrementedBatteryLevel.Clear();
                    }
                }
                previousBatteryLevel = batteryData.batteryLevel;
            });

            //handled the corner case where in the end there is no increment in the battery level
            if (continousDecrementedBatteryLevel.Count >= 2)
            {
                var percentageDecreaseInADay = GetPercentageDecreaseInADay(continousDecrementedBatteryLevel);

                batteryLevelAvgTillIncrement.Add(percentageDecreaseInADay);
                //Clear the continous list as we found the increment in the battery
                continousDecrementedBatteryLevel.Clear();
            }

            //find the Average of Average battery level (final average)
            if (batteryLevelAvgTillIncrement.Any())
            {
                if (batteryLevelAvgTillIncrement.Average() <= 30)
                {
                    analysedResult.batteryStatus = BatteryStatus.Good;
                }
                else
                {
                    analysedResult.batteryStatus = BatteryStatus.Faulty;
                }
                analysedResult.averageConsumption = batteryLevelAvgTillIncrement.Average();
            }
            else
            {
                analysedResult.batteryStatus = BatteryStatus.Unknown;
            }
            return analysedResult;
        }

        private double GetPercentageDecreaseInADay(List<BatteryLevelTimestamp> continousDecrementedBatteryLevel)
        {
            //Get the percentage decrease (i.e. 2% decrease in 4 hours)
            var percentageDecrease = (continousDecrementedBatteryLevel.FirstOrDefault().batteryLevel - continousDecrementedBatteryLevel.LastOrDefault().batteryLevel) * 100;

            //Time taken in decrease (in hours)
            var decreaseTimeInHours = (continousDecrementedBatteryLevel.LastOrDefault().timestamp - continousDecrementedBatteryLevel.FirstOrDefault().timestamp).TotalHours;

            //% decrease in a day (24 hours) based on time taken
            var percentageDecreaseInADay = (percentageDecrease / decreaseTimeInHours) * 24;
            return percentageDecreaseInADay;
        }
    }
}
