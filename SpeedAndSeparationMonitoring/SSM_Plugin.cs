using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using VisualComponents.Create3D;
using VisualComponents.UX.Shared;

namespace SpeedAndSeparationMonitoring
{
	/// <summary>
	/// Plugin for handling SSM_Data Events
	/// </summary>
	[Export (typeof(IPlugin))]
	public class SSM_Plugin : IPlugin, IHandle<SSM_Data>
	{
		private SeparationCalculator separationCalculator;
		private SpeedCalculator speedCalculator;

		/// <summary>
		/// Constructs the plugin and subscribes to events
		/// </summary>
		/// <param name="eventAggregator"></param>
		[ImportingConstructor]
		public SSM_Plugin([Import(typeof(IEventAggregator))] IEventAggregator eventAggregator)
		{
			eventAggregator.Subscribe(this);
			this.separationCalculator = GetDefaultSeparationCalculator();
			this.speedCalculator = getDefaultSpeedCalculator();
		}

		/// <summary>
		/// Calculates safety separation distance and allowed velocity, when SSM_Data object is received.
		/// </summary>
		/// <param name="data"> Information regarding the robot and a sensed human </param>
		public void Handle(SSM_Data data)
		{
			double separationDistance = separationCalculator
				.GetSeparationDistance(data.GetHumanVelocity(), data.GetRobotVelocity());
			double allowedVelocity = speedCalculator
				.GetAllowedVelocity(data.GetBodyPart(), data.GetHumanVelocity(), data.GetContactArea());

			string message = "Distance: " + separationDistance + "| Velocity: " + allowedVelocity;

			IMessageService ms = IoC.Get<IMessageService>();
			ms.AppendMessage(message, MessageLevel.Warning);
		}

		private SeparationCalculator GetDefaultSeparationCalculator()
		{
			return new SeparationCalculator(0.1, 0.31, 160, 160, 0.04);
		}

		private SpeedCalculator getDefaultSpeedCalculator()
		{
			return new SpeedCalculator(160, 4.5);
		}

		/// <summary>
		/// Plugin does nothing when exiting
		/// </summary>
		void IPlugin.Exit()
		{
		}

		/// <summary>
		/// Plugin does nothing while initializing
		/// </summary>
		void IPlugin.Initialize()
		{
		}
	}
}
