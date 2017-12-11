using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedAndSeparationMonitoring
{
	/// <summary>
	/// Calculator for the safety separation distance between human and a robot
	/// </summary>
	public class SeparationCalculator
	{
		private const string PARAMETER_ERROR = "All parameters have to be positive values";

		private double reactionTimeRobot;
		private double stoppingTimeRobot;
		private double intrusionDistance;
		private double uncertaintyHuman;
		private double uncertaintyRobot;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="reactionTimeRobot"> Time that passes until the robotic system reacts [s] </param>
		/// <param name="stoppingTimeRobot"> Time that passes until the robotic system stops [s] </param>
		/// <param name="intrusionDistance"> Distance that an object can intrude into the sensing field [mm] </param>
		/// <param name="uncertaintyHuman"> Position uncertainty of the sensing field [mm] </param>
		/// <param name="uncertaintyRobot"> Position uncertainty of the robot [mm] </param>
		public SeparationCalculator(double reactionTimeRobot, double stoppingTimeRobot,
			double intrusionDistance, double uncertaintyHuman, double uncertaintyRobot)
		{
			CheckParameters(reactionTimeRobot, stoppingTimeRobot, intrusionDistance, uncertaintyHuman, uncertaintyRobot);

			this.reactionTimeRobot = reactionTimeRobot;
			this.stoppingTimeRobot = stoppingTimeRobot;
			this.intrusionDistance = intrusionDistance;
			this.uncertaintyHuman = uncertaintyHuman;
			this.uncertaintyRobot = uncertaintyRobot;
		}

		/// <summary>
		/// Calculates the minimum separation distance between the human and robot.
		/// </summary>
		/// <param name="humanVelocity"> Speed of the human in the direction of the robot [mm/s] </param>
		/// <param name="robotVelocity"> Speed of the robot in the direction of the human [mm/s] </param>
		/// <returns> Minumim separation distance between human and robot [mm] </returns>
		public double GetSeparationDistance(double humanVelocity, double robotVelocity)
		{
			double humanDistance = humanVelocity * (reactionTimeRobot + stoppingTimeRobot);
			double reactionDistance = robotVelocity * reactionTimeRobot;
			double stoppingDistance = robotVelocity * stoppingTimeRobot;

			double distance = humanDistance + reactionDistance + stoppingDistance +
				intrusionDistance + uncertaintyHuman + uncertaintyRobot;

			return (distance > 0.0) ? distance : 0.0;
		}

		private static void CheckParameters(double reactionTimeRobot, double stoppingTimeRobot, double intrusionDistance, double uncertaintyHuman, double uncertaintyRobot)
		{
			if (reactionTimeRobot < 0 || stoppingTimeRobot < 0 || intrusionDistance < 0
				|| uncertaintyHuman < 0 || uncertaintyRobot < 0)
			{
				throw new ArgumentException(PARAMETER_ERROR);
			}
		}
	}
}
