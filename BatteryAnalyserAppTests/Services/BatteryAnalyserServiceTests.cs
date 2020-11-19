using BatteryAnalyserApp.Models;
using BatteryAnalyserApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryAnalyserAppTests.Services
{
    [TestClass]
    public class BatteryAnalyserServiceTests
    {
        private BatteryAnalyserService batteryAnalyserService { get; set; }
        private Mock<IWebClientWrapper> webClientWrapper { get; set; }

        public string batteryDataFaultyJSONString, batteryDataGoodJSONString, batteryDataGoodFaultyJSONString, batteryDataUnknownJSONString;

        [TestInitialize]
        public void Init()
        {
            this.webClientWrapper = new Mock<IWebClientWrapper>();
            //Contains two distinct serial number faulty with both Good
            this.batteryDataFaultyJSONString = "[{\"academyId\":30006,\"batteryLevel\":0.55,\"employeeId\":\"T1007384\",\"serialNumber\":\"1805C67HD02009\",\"timestamp\":\"2019-05-17T07:47:25.833+01:00\"},{\"academyId\":30006,\"batteryLevel\":0.51,\"employeeId\":\"T1001417\",\"serialNumber\":\"1805C67HD02009\",\"timestamp\":\"2019-05-17T07:48:49.147+01:00\"},{\"academyId\":30006,\"batteryLevel\":0.5,\"employeeId\":\"T1008250\",\"serialNumber\":\"1805C67HD02009\",\"timestamp\":\"2019-05-17T07:50:35.158+01:00\"},{\"academyId\":30006,\"batteryLevel\":0.5,\"employeeId\":\"T1001417\",\"serialNumber\":\"1805C67HD02332\",\"timestamp\":\"2019-05-17T07:57:08.29+01:00\"},{\"academyId\":30006,\"batteryLevel\":0.48,\"employeeId\":\"T1001820\",\"serialNumber\":\"1805C67HD02332\",\"timestamp\":\"2019-05-17T07:57:58.979+01:00\"}]";

            //Contains two distinct serial number with both Good
            this.batteryDataGoodJSONString = "[{\"academyId\":30006,\"batteryLevel\":0.55,\"employeeId\":\"T1007384\",\"serialNumber\":\"1805C67HD02009\",\"timestamp\":\"2019-05-17T07:47:25.833+01:00\"},{\"academyId\":30006,\"batteryLevel\":0.51,\"employeeId\":\"T1001417\",\"serialNumber\":\"1805C67HD02009\",\"timestamp\":\"2019-05-18T06:48:49.147+01:00\"},{\"academyId\":30006,\"batteryLevel\":0.5,\"employeeId\":\"T1008250\",\"serialNumber\":\"1805C67HD02009\",\"timestamp\":\"2019-05-19T03:50:35.158+01:00\"},{\"academyId\":30006,\"batteryLevel\":0.5,\"employeeId\":\"T1001417\",\"serialNumber\":\"1805C67HD02332\",\"timestamp\":\"2019-05-17T07:57:08.29+01:00\"},{\"academyId\":30006,\"batteryLevel\":0.48,\"employeeId\":\"T1001820\",\"serialNumber\":\"1805C67HD02332\",\"timestamp\":\"2019-05-18T06:52:58.979+01:00\"}]";

            //Contains two distinct serial number with one Good and one faulty
            this.batteryDataGoodFaultyJSONString = "[{\"academyId\":30006,\"batteryLevel\":0.55,\"employeeId\":\"T1007384\",\"serialNumber\":\"1805C67HD02009\",\"timestamp\":\"2019-05-17T07:47:25.833+01:00\"},{\"academyId\":30006,\"batteryLevel\":0.51,\"employeeId\":\"T1001417\",\"serialNumber\":\"1805C67HD02009\",\"timestamp\":\"2019-05-18T06:48:49.147+01:00\"},{\"academyId\":30006,\"batteryLevel\":0.5,\"employeeId\":\"T1008250\",\"serialNumber\":\"1805C67HD02009\",\"timestamp\":\"2019-05-19T03:50:35.158+01:00\"},{\"academyId\":30006,\"batteryLevel\":0.5,\"employeeId\":\"T1001417\",\"serialNumber\":\"1805C67HD02332\",\"timestamp\":\"2019-05-17T07:57:08.29+01:00\"},{\"academyId\":30006,\"batteryLevel\":0.2,\"employeeId\":\"T1001820\",\"serialNumber\":\"1805C67HD02332\",\"timestamp\":\"2019-05-17T08:52:58.979+01:00\"}]";

            //Contains two distinct serial number with Unknown
            this.batteryDataUnknownJSONString = "[{\"academyId\":30006,\"batteryLevel\":0.55,\"employeeId\":\"T1007384\",\"serialNumber\":\"1805C67HD02009\",\"timestamp\":\"2019-05-17T07:47:25.833+01:00\"}]";

            this.batteryAnalyserService = new BatteryAnalyserService(this.webClientWrapper.Object);
        }

        [TestMethod]
        public void ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new BatteryAnalyserService(null));
        }

        [TestMethod]
        public void SuccessWithBatteryStatusFaultyData()
        {
            this.webClientWrapper.Setup(wc => wc.DownloadString(It.IsAny<string>())).Returns(this.batteryDataFaultyJSONString);
            var result = this.batteryAnalyserService.GetDevicesStatusWithAverage();
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Where(r => r.batteryStatus == BatteryStatus.Faulty).ToList().Count(), 2);
        }

        [TestMethod]
        public void SuccessWithBatteryStatusGoodData()
        {
            this.webClientWrapper.Setup(wc => wc.DownloadString(It.IsAny<string>())).Returns(this.batteryDataGoodJSONString);
            var result = this.batteryAnalyserService.GetDevicesStatusWithAverage();
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Where(r => r.batteryStatus == BatteryStatus.Good).ToList().Count(), 2);
        }

        [TestMethod]
        public void SuccessWithBatteryStatusGoodAndFaultyData()
        {
            this.webClientWrapper.Setup(wc => wc.DownloadString(It.IsAny<string>())).Returns(this.batteryDataGoodFaultyJSONString);
            var result = this.batteryAnalyserService.GetDevicesStatusWithAverage();
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Where(r => r.batteryStatus == BatteryStatus.Good).ToList().Count(), 1);
            Assert.AreEqual(result.Where(r => r.batteryStatus == BatteryStatus.Faulty).ToList().Count(), 1);
        }

        [TestMethod]
        public void SuccessWithBatteryStatusUnknownData()
        {
            this.webClientWrapper.Setup(wc => wc.DownloadString(It.IsAny<string>())).Returns(this.batteryDataUnknownJSONString);
            var result = this.batteryAnalyserService.GetDevicesStatusWithAverage();
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Where(r => r.batteryStatus == BatteryStatus.Unknown).ToList().Count(), 1);
        }
    }
}
