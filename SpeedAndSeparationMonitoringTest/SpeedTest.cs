using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpeedAndSeparationMonitoring;

namespace SpeedAndSeparationMonitoringTest
{
	[TestClass]
	public class SpeedTest
	{
		private const string ERROR_MESSAGE = "Velocity not calculated correctly";

		// Default robot and payload mass
		private const double ROBOT_MASS = 160;
		private const double PAYLOAD_MASS = 4.5;

		// Default speed calculator
		private readonly SpeedCalculator SC = new SpeedCalculator(ROBOT_MASS, PAYLOAD_MASS);

		[TestMethod]
		public void TestHand()
		{
			double expected = 197.69;
			double actual = SC.GetAllowedVelocity(BodyPart.Hands, 1600, 1);

			Assert.AreEqual(expected, actual, 0.01, ERROR_MESSAGE);
		}

		[TestMethod]
		public void TestFace()
		{
			double expected = 0.0;
			double actual = SC.GetAllowedVelocity(BodyPart.Face, 1600, 1);

			Assert.AreEqual(expected, actual, 0.01, ERROR_MESSAGE);
		}

		[TestMethod]
		public void TestZeroHumanSpeed()
		{
			double expected = 291.32;
			double actual = SC.GetAllowedVelocity(BodyPart.Chest, 0, 1);

			Assert.AreEqual(expected, actual, 0.01, ERROR_MESSAGE);
		}

		[TestMethod]
		public void TestNegativeHumanSpeed()
		{
			double expected = 3397.69;
			double actual = SC.GetAllowedVelocity(BodyPart.Hands, -1600, 1);

			Assert.AreEqual(expected, actual, 0.01, ERROR_MESSAGE);
		}
	}
}
