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

		[TestMethod]
		public void TestHand()
		{
			double expected = 197.69;

			SpeedCalculator sc = new SpeedCalculator(robotMass, payLoadMass);
			double actual = sc.GetAllowedVelocity(BodyPart.Hands, 1600, 1);

			Assert.AreEqual(expected, actual, 0.01, "Velocity not calculated correctly");
		}
	}
}
