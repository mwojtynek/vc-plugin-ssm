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
	/// Test Action for sending SSM_Data Events
	/// </summary>
	[Export(typeof(IActionItem))]
	public class SSM_Publisher : ActionItem
	{
		private IEventAggregator eventAggregator;

		[ImportingConstructor]
		public SSM_Publisher([Import(typeof(IEventAggregator))] IEventAggregator eventAggregator) : base("SSMCommand")
		{
			this.eventAggregator = eventAggregator;
		}

		/// <summary>
		/// Sends example SSM_Data Event
		/// </summary>
		public override void Execute()
		{
			SSM_Data data = new SSM_Data(1600, 400, BodyPart.Hands, 1);
			eventAggregator.Publish(data);
		}
	}

	/// <summary>
	/// Plugin that triggers SSM_Publisher on startup
	/// </summary>
	[Export(typeof(IPlugin))]
	public class SSM_Publisher_Plugin : IPlugin
	{
		void IPlugin.Exit()
		{
		}

		void IPlugin.Initialize()
		{
			ICommandRegistry cr = IoC.Get<ICommandRegistry>();
			cr.ExecuteAction("SSMCommand");
		}
	}
}
