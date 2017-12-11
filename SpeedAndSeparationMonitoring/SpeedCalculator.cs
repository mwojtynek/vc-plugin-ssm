using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedAndSeparationMonitoring
{

	/// <summary>
	/// Human body parts which may get into contact with the moving robot parts.
	/// </summary>
	public enum BodyPart { Skull, Face, Neck, Back, Chest, Abdomen,
		Pelvis, UpperArms, LowerArms, Hands, UpperLegs, LowerLegs };

	public class SpeedCalculator
	{
		private static readonly Dictionary<BodyPart, int> allowedPressureDict = new Dictionary<BodyPart, int>
		{
			[BodyPart.Skull] = 110,
			[BodyPart.Face] = 110,
			[BodyPart.Neck] = 140,
			[BodyPart.Back] = 160,
			[BodyPart.Chest] = 120,
			[BodyPart.Abdomen] = 140,
			[BodyPart.Pelvis] = 210,
			[BodyPart.UpperArms] = 190,
			[BodyPart.LowerArms] = 180,
			[BodyPart.Hands] = 190,
			[BodyPart.UpperLegs] = 220,
			[BodyPart.LowerLegs] = 210
		};

		private static readonly Dictionary<BodyPart, int> springConstantDict = new Dictionary<BodyPart, int>
		{
			[BodyPart.Skull] = 150,
			[BodyPart.Face] = 75,
			[BodyPart.Neck] = 50,
			[BodyPart.Back] = 35,
			[BodyPart.Chest] = 25,
			[BodyPart.Abdomen] = 10,
			[BodyPart.Pelvis] = 25,
			[BodyPart.UpperArms] = 30,
			[BodyPart.LowerArms] = 40,
			[BodyPart.Hands] = 75,
			[BodyPart.UpperLegs] = 50,
			[BodyPart.LowerLegs] = 60
		};

		private static readonly Dictionary<BodyPart, double> bodyPartMassDict = new Dictionary<BodyPart, double>
		{
			[BodyPart.Skull] = 4.4,
			[BodyPart.Face] = 4.4,
			[BodyPart.Neck] = 1.2,
			[BodyPart.Back] = 40,
			[BodyPart.Chest] = 40,
			[BodyPart.Abdomen] = 40,
			[BodyPart.Pelvis] = 40,
			[BodyPart.UpperArms] = 3,
			[BodyPart.LowerArms] = 2,
			[BodyPart.Hands] = 0.6,
			[BodyPart.UpperLegs] = 75,
			[BodyPart.LowerLegs] = 75
		};

		private const double transientMultiplier = 2;
		private double effectiveRobotMass;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="robotMass"> Total mass of the moving parts of the robot [kg] </param>
		/// <param name="payLoadMass"> Mass of the paylad (including tools) [kg] </param>
		public SpeedCalculator(double robotMass, double payLoadMass)
		{
			this.effectiveRobotMass = GetEffectiveRobotMass(robotMass, payLoadMass);
		}

		/// <summary>
		/// Calculates the allowed velocity of the robot in direction of the human
		/// </summary>
		/// <param name="bodyPart"> Human body part which is closest to the robot moving part </param>
		/// <param name="humanVelocity"> Speed of the human in direction of the robot [mm/s] </param>
		/// <param name="contactArea"> Area of potential contact between a human and the robot [cm^2] </param>
		/// <returns> Allowed robot velocity [mm/s] </returns>
		public double GetAllowedVelocity(BodyPart bodyPart, double humanVelocity, double contactArea)
		{
			double allowedPressure = allowedPressureDict[bodyPart];
			double springConstant = springConstantDict[bodyPart];
			double bodyPartMass = bodyPartMassDict[bodyPart];

			double reducedMass = GetReducedMass(this.effectiveRobotMass, bodyPartMass);

			double relativeVelocity = 1000 * (transientMultiplier * allowedPressure * contactArea) /
				Math.Sqrt(reducedMass * springConstant * 1000); // 1000 = 1/(10^-3)

			double absoluteVelocity = relativeVelocity - humanVelocity;

			return (absoluteVelocity > 0.0) ? absoluteVelocity : 0.0;
		}

		/// <summary>
		/// Calculates the reduced mass of a two-body system
		/// </summary>
		/// <param name="mass1"> First mass of the two-body system [kg] </param>
		/// <param name="mass2"> Second part of the two-body system [kg]</param>
		/// <returns> Reduced mass [kg] </returns>
		private double GetReducedMass(double mass1, double mass2)
		{
			return 1 / ((1 / mass1) + (1 / mass2));
		}

		/// <summary>
		/// Calculates the effective robot mass.
		/// </summary>
		/// <param name="robotMass"> Total mass of the moving parts of the robot [kg] </param>
		/// <param name="payLoadMass"> Mass of the payload (including tools) [kg] </param>
		/// <returns> Effective robot mass [kg] </returns>
		private double GetEffectiveRobotMass(double robotMass, double payLoadMass)
		{
			return (robotMass / 2) + payLoadMass;
		}
	}
}
