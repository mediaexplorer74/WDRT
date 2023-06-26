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
	// Token: 0x0200001B RID: 27
	[Export]
	public class FlashingViewModel : BaseViewModel, ICanHandle<ProgressMessage>, ICanHandle, ICanHandle<DetectionTypeMessage>, INotifyLiveRegionChanged
	{
		// Token: 0x06000132 RID: 306 RVA: 0x000086C9 File Offset: 0x000068C9
		[ImportingConstructor]
		public FlashingViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000133 RID: 307 RVA: 0x000086DC File Offset: 0x000068DC
		// (remove) Token: 0x06000134 RID: 308 RVA: 0x00008714 File Offset: 0x00006914
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler LiveRegionChanged;

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000874C File Offset: 0x0000694C
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00008764 File Offset: 0x00006964
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000087A4 File Offset: 0x000069A4
		// (set) Token: 0x06000138 RID: 312 RVA: 0x000087BC File Offset: 0x000069BC
		public int Progress
		{
			get
			{
				return this.progress;
			}
			set
			{
				base.SetValue<int>(() => this.Progress, ref this.progress, value);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000087FC File Offset: 0x000069FC
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00008814 File Offset: 0x00006A14
		public bool ProgressPercentageVisible
		{
			get
			{
				return this.progressPercentageVisible;
			}
			set
			{
				base.SetValue<bool>(() => this.ProgressPercentageVisible, ref this.progressPercentageVisible, value);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00008854 File Offset: 0x00006A54
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000886C File Offset: 0x00006A6C
		public bool IsProgressIndeterminate
		{
			get
			{
				return this.isProgressIndeterminate;
			}
			set
			{
				base.SetValue<bool>(() => this.IsProgressIndeterminate, ref this.isProgressIndeterminate, value);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000088AC File Offset: 0x00006AAC
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000088C4 File Offset: 0x00006AC4
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000892C File Offset: 0x00006B2C
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00008944 File Offset: 0x00006B44
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00008984 File Offset: 0x00006B84
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000899C File Offset: 0x00006B9C
		public DetectionType DetectionType
		{
			get
			{
				return this.detectionType;
			}
			set
			{
				base.SetValue<DetectionType>(() => this.DetectionType, ref this.detectionType, value);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000089DC File Offset: 0x00006BDC
		public override void OnStarted()
		{
			base.OnStarted();
			this.Progress = 0;
			this.Message = string.Empty;
			this.LiveText = string.Empty;
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("DeviceFlashing"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(true, LocalizationManager.GetTranslation("FlashCancelMessage"), null));
			bool flag = this.appContext.CurrentPhone.IsDeviceInEmergencyMode();
			if (flag)
			{
				base.Commands.Run((FlowController c) => c.EmergencyFlashDevice(null, CancellationToken.None));
			}
			else
			{
				base.Commands.Run((FlowController c) => c.FlashDevice(this.DetectionType, CancellationToken.None));
			}
			this.LiveText = LocalizationManager.GetTranslation("InstallationStarted");
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00008B8E File Offset: 0x00006D8E
		public override void OnStopped()
		{
			base.OnStopped();
			this.LiveText = LocalizationManager.GetTranslation("InstallationCompleted");
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00008BAC File Offset: 0x00006DAC
		public void Handle(ProgressMessage progressMessage)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				this.Progress = progressMessage.Progress;
				this.Message = progressMessage.Message;
				bool flag = !string.IsNullOrEmpty(progressMessage.Message);
				if (flag)
				{
					string translation = LocalizationManager.GetTranslation(progressMessage.Message);
					bool flag2 = !translation.Contains("NOT FOUND");
					if (flag2)
					{
						this.Message = translation;
					}
				}
				this.ProgressPercentageVisible = this.Progress >= 0 || !string.IsNullOrWhiteSpace(this.Message);
				this.IsProgressIndeterminate = this.Progress == 0 || this.Progress > 100;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00008C5C File Offset: 0x00006E5C
		public void Handle(DetectionTypeMessage detectionMessage)
		{
			this.DetectionType = detectionMessage.DetectionType;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008C6C File Offset: 0x00006E6C
		private void OnLiveRegionChanged()
		{
			EventHandler liveRegionChanged = this.LiveRegionChanged;
			bool flag = liveRegionChanged != null;
			if (flag)
			{
				liveRegionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x040000AE RID: 174
		private int progress;

		// Token: 0x040000AF RID: 175
		private string liveText;

		// Token: 0x040000B0 RID: 176
		private string message;

		// Token: 0x040000B1 RID: 177
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x040000B2 RID: 178
		private bool progressPercentageVisible;

		// Token: 0x040000B3 RID: 179
		private DetectionType detectionType;

		// Token: 0x040000B4 RID: 180
		private bool isProgressIndeterminate;
	}
}
