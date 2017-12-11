using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedAndSeparationMonitoring
{
	/// <summary>
	/// Data that is needed for SSM calculation
	/// </summary>
	public class SSM_Data
	{
		private double humanVelocity;
		private double robotVelocity;
		private BodyPart bodyPart;
		private double contactArea;

		/// <summary>
		/// Dynamic data for Speed and Separation Monitoring.
		/// </summary>
		/// <param name="humanVelocity"> Velocity of the human in the direction of the robot [mm/s] </param>
		/// <param name="robotVelocity"> Velocity of the robot in the direction of the human [mm/s} </param>
		/// <param name="bodyPart"> Human body part which is closest to the robot moving part </param>
		/// <param name="contactArea"> Area of potential contact between a human and the robot [cm^2] </param>
		public SSM_Data(double humanVelocity, double robotVelocity, BodyPart bodyPart, double contactArea)
		{
			this.humanVelocity = humanVelocity;
			this.robotVelocity = robotVelocity;
			this.bodyPart = bodyPart;
			this.contactArea = contactArea;
		}

		public double GetHumanVelocity()
		{
			return this.humanVelocity;
		}

		public double GetRobotVelocity()
		{
			return this.robotVelocity;
		}

		public BodyPart GetBodyPart()
		{
			return this.bodyPart;
		}

		public double GetContactArea()
		{
			return this.contactArea;
		}
	}
}
