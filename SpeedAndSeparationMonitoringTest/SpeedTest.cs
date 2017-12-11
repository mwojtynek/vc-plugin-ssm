using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpeedAndSeparationMonitoring;

namespace SpeedAndSeparationMonitoringTest
{
	[TestClass]
	public class SpeedTest
	{
		private const double robotMass = 160;
		private const double payLoadMass = 4.5;
		private SpeedCalculator sc;

		[TestInitialize]
		public void Init()
		{
			this.sc = new SpeedCalculator(robotMass, payLoadMass);
		}

		[TestMethod]
		public void TestHand()
		{
			double expected = 197.69;
			double actual = sc.GetAllowedVelocity(BodyPart.Hands, 1600, 1);

			Assert.AreEqual(expected, actual, 0.01, "Velocity not calculated correctly");
		}

		[TestMethod]
		public void TestFace()
		{
			double expected = 0.0;
			double actual = sc.GetAllowedVelocity(BodyPart.Face, 1600, 1);

			Assert.AreEqual(expected, actual, "Velocity not calculated correctly!");
		}
	}
}
