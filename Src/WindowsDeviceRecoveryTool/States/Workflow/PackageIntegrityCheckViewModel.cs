using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Workflow
{
	// Token: 0x02000018 RID: 24
	[Export]
	public class PackageIntegrityCheckViewModel : BaseViewModel, ICanHandle<FfuIntegrityCheckMessage>, ICanHandle, ICanHandle<ProgressMessage>, INotifyLiveRegionChanged
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00008041 File Offset: 0x00006241
		[ImportingConstructor]
		public PackageIntegrityCheckViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000119 RID: 281 RVA: 0x00008054 File Offset: 0x00006254
		// (remove) Token: 0x0600011A RID: 282 RVA: 0x0000808C File Offset: 0x0000628C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler LiveRegionChanged;

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000080C4 File Offset: 0x000062C4
		// (set) Token: 0x0600011C RID: 284 RVA: 0x000080DC File Offset: 0x000062DC
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000811C File Offset: 0x0000631C
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00008134 File Offset: 0x00006334
		public bool CheckInProgress
		{
			get
			{
				return this.checkInProgress;
			}
			set
			{
				base.SetValue<bool>(() => this.CheckInProgress, ref this.checkInProgress, value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00008174 File Offset: 0x00006374
		public bool ProgressBarVisible
		{
			get
			{
				return this.progress != -1;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00008194 File Offset: 0x00006394
		public bool ProgressRingVisible
		{
			get
			{
				return this.progress == -1;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000081B0 File Offset: 0x000063B0
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000081C8 File Offset: 0x000063C8
		public string LiveText
		{
			get
			{
				return this.liveText;
			}
			set
			{
				base.SetValue<string>(() => this.LiveText, ref this.liveText, value);
				bool flag = !string.IsNullOrWhiteSpace(this.liveText);
				if (flag)
				{
					this.OnLiveRegionChanged();
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00008230 File Offset: 0x00006430
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00008248 File Offset: 0x00006448
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				base.SetValue<string>(() => this.Message, ref this.message, value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00008288 File Offset: 0x00006488
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000082A0 File Offset: 0x000064A0
		public int Progress
		{
			get
			{
				return this.progress;
			}
			set
			{
				base.SetValue<int>(() => this.Progress, ref this.progress, value);
				base.RaisePropertyChanged<bool>(() => this.ProgressBarVisible);
				base.RaisePropertyChanged<bool>(() => this.ProgressRingVisible);
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00008358 File Offset: 0x00006558
		public override void OnStarted()
		{
			base.OnStarted();
			this.CheckInProgress = true;
			this.Message = string.Empty;
			this.LiveText = string.Empty;
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("PackageIntegrityCheck"), ""));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(true, LocalizationManager.GetTranslation("FlashCancelMessage"), null));
			base.Commands.Run((FlowController c) => c.CheckPackageIntegrity(null, CancellationToken.None));
			this.Progress = -1;
			this.LiveText = LocalizationManager.GetTranslation("VerificationStarted");
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00008459 File Offset: 0x00006659
		public override void OnStopped()
		{
			((IAsyncDelegateCommand)base.Commands["CheckPackageIntegrity"]).Cancel();
			base.OnStopped();
			this.LiveText = LocalizationManager.GetTranslation("VerificationCompleted");
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008490 File Offset: 0x00006690
		public void Handle(ProgressMessage progressMessage)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				this.Progress = progressMessage.Progress;
				this.Message = progressMessage.Message;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000084C4 File Offset: 0x000066C4
		public void Handle(FfuIntegrityCheckMessage integrityCheckMessage)
		{
			this.CheckInProgress = false;
			bool result = integrityCheckMessage.Result;
			if (result)
			{
				string nextState = ((this.AppContext.CurrentPhone.Type == PhoneTypes.HoloLensAccessory) ? "FlashingState" : "BatteryCheckingState");
				base.Commands.Run((AppController c) => c.SwitchToState(nextState));
			}
			else
			{
				this.Message = string.Format(LocalizationManager.GetTranslation("FirmwareIntegrityError"), this.AppContext.CurrentPhone.PackageFilePath);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000085AC File Offset: 0x000067AC
		private void OnLiveRegionChanged()
		{
			EventHandler liveRegionChanged = this.LiveRegionChanged;
			bool flag = liveRegionChanged != null;
			if (flag)
			{
				liveRegionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x040000A4 RID: 164
		private string liveText;

		// Token: 0x040000A5 RID: 165
		private string message;

		// Token: 0x040000A6 RID: 166
		private bool checkInProgress;

		// Token: 0x040000A7 RID: 167
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x040000A8 RID: 168
		private int progress;
	}
}
