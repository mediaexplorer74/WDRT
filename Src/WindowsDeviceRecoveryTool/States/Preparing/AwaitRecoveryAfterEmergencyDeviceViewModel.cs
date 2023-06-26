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
	// Token: 0x02000042 RID: 66
	[Export]
	public class AwaitRecoveryAfterEmergencyDeviceViewModel : BaseViewModel, ICanHandle<DeviceConnectedMessage>, ICanHandle
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000F46D File Offset: 0x0000D66D
		[ImportingConstructor]
		public AwaitRecoveryAfterEmergencyDeviceViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000F480 File Offset: 0x0000D680
		public string RebootPhoneInstructions
		{
			get
			{
				return LocalizationManager.GetTranslation("AwaitRecoveryAfterEmergencyFlashingInstruction");
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000F49C File Offset: 0x0000D69C
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
		public bool AreInstructionsVisible
		{
			get
			{
				return this.areInstructionsVisible;
			}
			set
			{
				base.SetValue<bool>(() => this.AreInstructionsVisible, ref this.areInstructionsVisible, value);
				base.RaisePropertyChanged<bool>(() => this.IsCancelVisible);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000F538 File Offset: 0x0000D738
		public bool IsCancelVisible
		{
			get
			{
				return this.areInstructionsVisible;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000F550 File Offset: 0x0000D750
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000F568 File Offset: 0x0000D768
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

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		public override string PreviousStateName
		{
			get
			{
				return "AutomaticManufacturerSelectionState";
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		public override void OnStarted()
		{
			this.timer = new Timer(new TimerCallback(this.OnTimerCallback), null, 30000, 0);
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("WaitingForConnection"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(true, null, null));
			this.AreInstructionsVisible = false;
			base.Commands.Run((LumiaController c) => c.StartLumiaDetection(DetectionType.RecoveryModeAfterEmergencyFlashing, CancellationToken.None));
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000F6B5 File Offset: 0x0000D8B5
		private void OnTimerCallback(object state)
		{
			this.timer.Dispose();
			this.AreInstructionsVisible = true;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public override void OnStopped()
		{
			this.timer.Dispose();
			base.Commands.Run((LumiaController c) => c.StopLumiaDetection());
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000F734 File Offset: 0x0000D934
		public void Handle(DeviceConnectedMessage message)
		{
			bool flag = base.IsStarted && message.Phone.Type == PhoneTypes.Lumia && this.appContext.CurrentPhone != null;
			if (flag)
			{
				base.Commands.Run((FlowController a) => a.FinishAwaitRecoveryAfterEmergency(false, CancellationToken.None));
			}
		}

		// Token: 0x0400013B RID: 315
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x0400013C RID: 316
		private Timer timer;

		// Token: 0x0400013D RID: 317
		private bool areInstructionsVisible;
	}
}
