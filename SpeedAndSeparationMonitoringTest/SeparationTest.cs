using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpeedAndSeparationMonitoring;

namespace SpeedAndSeparationMonitoringTest
{
	[TestClass]
	public class SeparationTest
	{
		private const string FAIL_MESSAGE = "Separation not calculated correctly";

		// Default velocity values in mm/s
		private const double HUMAN_SPEED = 1600;
		private const double ROBOT_SPEED = 400;

		// Default reaction/stopping time in s
		private const double REACTION_TIME_ROBOT = 0.1;
		private const double STOPPING_TIME_ROBOT = 0.31;

		// Default intrusion distance/uncertainty in mm
		private const double INTRUSTION_DISTANCE = 160;
		private const double UNCERTAINTY_HUMAN = 160;
		private const double UNCERTAINTY_ROBOT = 0.04;

		// Default separation calculator
		private readonly SeparationCalculator sc = new SeparationCalculator(
				REACTION_TIME_ROBOT, STOPPING_TIME_ROBOT, INTRUSTION_DISTANCE, UNCERTAINTY_HUMAN, UNCERTAINTY_ROBOT);


		[TestMethod]
		public void TestDefaultValues()
		{
			double expected = 1140.04;
			double actual = sc.GetSeparationDistance(HUMAN_SPEED, ROBOT_SPEED);

			Assert.AreEqual(expected, actual, 0.001, FAIL_MESSAGE);
		}

		[TestMethod]
		public void TestNegativeHumanSpeed()
		{
			double negativeSpeed = HUMAN_SPEED * -1;

			double expected = 0.0;
			double actual = sc.GetSeparationDistance(negativeSpeed, ROBOT_SPEED);

			Assert.AreEqual(expected, actual, 0.001, FAIL_MESSAGE);
		}

		[TestMethod]
		public void TestNegativeRobotSpeed()
		{
			double negativeSpeed = ROBOT_SPEED * -1;

			double expected = 812.04;
			double actual = sc.GetSeparationDistance(HUMAN_SPEED, negativeSpeed);

			Assert.AreEqual(expected, actual, 0.01, FAIL_MESSAGE);
		}
	}
}
