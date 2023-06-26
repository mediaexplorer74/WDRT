using System;
using System.ComponentModel.Composition;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x02000057 RID: 87
	[Export]
	public class AwaitRecoveryDeviceViewModel : BaseViewModel, ICanHandle<DeviceConnectedMessage>, ICanHandle
	{
		// Token: 0x06000362 RID: 866 RVA: 0x00012F40 File Offset: 0x00011140
		[ImportingConstructor]
		public AwaitRecoveryDeviceViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00012F54 File Offset: 0x00011154
		public string RebootPhoneInstructions
		{
			get
			{
				return LocalizationManager.GetTranslation("RebootPhoneInstructions");
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00012F70 File Offset: 0x00011170
		// (set) Token: 0x06000365 RID: 869 RVA: 0x00012F88 File Offset: 0x00011188
		public bool AreInstructionsVisible
		{
			get
			{
				return this.areInstructionsVisible;
			}
			set
			{
				base.SetValue<bool>(() => this.AreInstructionsVisible, ref this.areInstructionsVisible, value);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00012FC8 File Offset: 0x000111C8
		public bool IsCancelVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00012FDC File Offset: 0x000111DC
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00012FF4 File Offset: 0x000111F4
		public Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext AppContext
		{
			get
			{
				return this.appContext;
			}
			set
			{
				base.SetValue<Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext>(() => this.AppContext, ref this.appContext, value);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00013034 File Offset: 0x00011234
		public override string PreviousStateName
		{
			get
			{
				return "AutomaticManufacturerSelectionState";
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001304C File Offset: 0x0001124C
		public override void OnStarted()
		{
			this.timer = new Timer(new TimerCallback(this.OnTimerCallback), null, 30000, 0);
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("ConnectPhone"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			this.AreInstructionsVisible = false;
			base.Commands.Run((LumiaController c) => c.StartLumiaDetection(DetectionType.RecoveryMode, CancellationToken.None));
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00013141 File Offset: 0x00011341
		private void OnTimerCallback(object state)
		{
			this.timer.Dispose();
			this.AreInstructionsVisible = true;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00013158 File Offset: 0x00011358
		public override void OnStopped()
		{
			this.timer.Dispose();
			base.Commands.Run((LumiaController c) => c.StopLumiaDetection());
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000131C0 File Offset: 0x000113C0
		public void Handle(DeviceConnectedMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				bool flag = message.Phone.Type == PhoneTypes.Lumia && this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.IsDeviceInEmergencyMode();
				if (flag)
				{
					base.Commands.Run((AppController a) => a.SwitchToState("ManualDeviceTypeSelectionState"));
				}
			}
		}

		// Token: 0x0400017E RID: 382
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x0400017F RID: 383
		private Timer timer;

		// Token: 0x04000180 RID: 384
		private bool areInstructionsVisible;
	}
}
