using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpeedAndSeparationMonitoring;

namespace SpeedAndSeparationMonitoringTest
{
	[TestClass]
	public class SeparationTest
	{
		// Default velocity values in mm/s
		private const double humanSpeed = 1600;
		private const double robotSpeed = 400;

		// Default reaction/stopping time in s
		private const double reactionTime = 0.1;
		private const double stoppingTime = 0.31;

		// Default intrusion distance/uncertainty in mm
		private const double intrustionDistance = 160;
		private const double uncertaintyHuman = 160;
		private const double uncertaintyRobot = 0.04;

		[TestMethod]
		public void TestDefaultValues()
		{
			double expected = 1140.04;

			SeparationCalculator sc = GetDefaultSC();
			double actual = sc.GetSeparationDistance(humanSpeed, robotSpeed);

			Assert.AreEqual(expected, actual, 0.001, "Separation not calculated correctly.");
		}

		[TestMethod]
		public void TestNegativeHumanSpeed()
		{
			double negativeSpeed = humanSpeed * -1;

			double expected = 0.0;

			SeparationCalculator sc = GetDefaultSC();
			double actual = sc.GetSeparationDistance(negativeSpeed, robotSpeed);

			Assert.AreEqual(expected, actual, 0.001, "Separation not calculated correctly.");
		}

		[TestMethod]
		public void TestNegativeRobotSpeed()
		{
			double negativeSpeed = robotSpeed * -1;

			double expected = humanSpeed * (reactionTime + stoppingTime)
				+ negativeSpeed * (reactionTime + stoppingTime)
				+ intrustionDistance + uncertaintyHuman + uncertaintyRobot;

			SeparationCalculator sc = GetDefaultSC();
			double actual = sc.GetSeparationDistance(humanSpeed, negativeSpeed);

			Assert.AreEqual(expected, actual, 0.001, "Separation not calculated correctly.");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestNegativeReactionTime()
		{
			SeparationCalculator sc = new SeparationCalculator(-1, 0, 0, 0, 0);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestNegativeStoppingTime()
		{
			SeparationCalculator sc = new SeparationCalculator(0, -1, 0, 0, 0);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestNegativeIntrustionDistance()
		{
			SeparationCalculator sc = new SeparationCalculator(0, 0, -1, 0, 0);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestNegativeUncertaintyHuman()
		{
			SeparationCalculator sc = new SeparationCalculator(0, 0, 0, -1, 0);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestNegativeUncertaintyRobot()
		{
			SeparationCalculator sc = new SeparationCalculator(0, 0, 0, 0, -1);
		}

		private SeparationCalculator GetDefaultSC()
		{
			return new SeparationCalculator(
				reactionTime, stoppingTime, intrustionDistance, uncertaintyHuman, uncertaintyRobot);
		}
	}
}
