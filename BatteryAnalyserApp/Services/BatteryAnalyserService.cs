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
            List<BatteryData> batteryData;
            List<string> faultySerialNumbers = new List<string>();
            //Create WebClient for downloading the battery data from Github
            using (WebClient wc = new WebClient())
            {
                var batteryJSON = wc.DownloadString(Constants.BatteryJSONDownloadURL);
                batteryData = JsonConvert.DeserializeObject<List<BatteryData>>(batteryJSON);

                //Group data based on SerialNumber
                var groupedBatteryData = from bd in batteryData group bd by bd.serialNumber;

                //iterate each group        
                foreach (var serialNumberGroup in groupedBatteryData)
                {
                    Console.WriteLine("Age Group: "+serialNumberGroup.Key+" : " + serialNumberGroup.Count()); //Each group has a key 

                    //Check if the device is faulty if yes then insert it into the Final Faulty List
                    if (IsFaultySerialNumberGroup(serialNumberGroup.ToList()))
                        faultySerialNumbers.Add(serialNumberGroup.Key);
                    //foreach (Student s in ageGroup) // Each group has inner collection
                    //    Console.WriteLine("Student Name: {0}", s.StudentName);
                }
            }

            var data = new List<string>() { "sachin" };
            return data;
        }

        private bool IsFaultySerialNumberGroup(List<BatteryData> list)
        {
            throw new NotImplementedException();
        }
    }
}
