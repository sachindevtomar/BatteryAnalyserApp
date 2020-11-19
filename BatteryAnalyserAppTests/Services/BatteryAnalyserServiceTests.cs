using BatteryAnalyserApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BatteryAnalyserAppTests.Services
{
    [TestClass]
    public class BatteryAnalyserServiceTests
    {
        public Mock<IBatteryAnalyserService> batteryAnalyserService { get; set; }

        [TestInitialize]
        public void Init()
        {
            this.batteryAnalyserService = new Mock<IBatteryAnalyserService>();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
