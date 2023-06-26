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

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x02000049 RID: 73
	[Export]
	public class DownloadAppUpdateViewModel : BaseViewModel, ICanHandle<ProgressMessage>, ICanHandle, INotifyLiveRegionChanged
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0000FF4D File Offset: 0x0000E14D
		[ImportingConstructor]
		public DownloadAppUpdateViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060002D5 RID: 725 RVA: 0x0000FF60 File Offset: 0x0000E160
		// (remove) Token: 0x060002D6 RID: 726 RVA: 0x0000FF98 File Offset: 0x0000E198
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler LiveRegionChanged;

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00010028 File Offset: 0x0000E228
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00010040 File Offset: 0x0000E240
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00010080 File Offset: 0x0000E280
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00010098 File Offset: 0x0000E298
		public bool ProgressUpdated
		{
			get
			{
				return this.progressUpdated;
			}
			set
			{
				base.SetValue<bool>(() => this.ProgressUpdated, ref this.progressUpdated, value);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000100D8 File Offset: 0x0000E2D8
		// (set) Token: 0x060002DE RID: 734 RVA: 0x000100F0 File Offset: 0x0000E2F0
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00010158 File Offset: 0x0000E358
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00010170 File Offset: 0x0000E370
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x000101B0 File Offset: 0x0000E3B0
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x000101C8 File Offset: 0x0000E3C8
		public string TimeLeftMessage
		{
			get
			{
				return this.timeLeftMessage;
			}
			set
			{
				base.SetValue<string>(() => this.TimeLeftMessage, ref this.timeLeftMessage, value);
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00010208 File Offset: 0x0000E408
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("DownloadingInstallPacket"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			this.ProgressUpdated = false;
			this.Message = string.Empty;
			this.TimeLeftMessage = string.Empty;
			this.LiveText = string.Empty;
			base.Commands.Run((AppController c) => c.UpdateApplication(null, CancellationToken.None));
			this.LiveText = LocalizationManager.GetTranslation("DownloadStarted");
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00010302 File Offset: 0x0000E502
		public override void OnStopped()
		{
			base.OnStopped();
			this.LiveText = LocalizationManager.GetTranslation("DownloadCompleted");
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00010320 File Offset: 0x0000E520
		public void Handle(ProgressMessage progressMessage)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				this.Progress = progressMessage.Progress;
				bool flag = !string.IsNullOrEmpty(progressMessage.Message);
				if (flag)
				{
					bool flag2 = progressMessage.Message == "DownloadingFiles";
					if (flag2)
					{
						this.ProgressUpdated = true;
						string text = "...";
						bool flag3 = progressMessage.BytesPerSecond > 0.0;
						if (flag3)
						{
							text = ComputerUnitsConverter.SpeedToString(progressMessage.BytesPerSecond);
							bool flag4 = progressMessage.SecondsLeft < 60L;
							if (flag4)
							{
								this.TimeLeftMessage = LocalizationManager.GetTranslation("DownloadProgressMinuteLess");
							}
							else
							{
								bool flag5 = progressMessage.SecondsLeft > 60L && progressMessage.SecondsLeft < 120L;
								if (flag5)
								{
									this.TimeLeftMessage = LocalizationManager.GetTranslation("DownloadProgressMinute");
								}
								else
								{
									this.TimeLeftMessage = string.Format(LocalizationManager.GetTranslation("DownloadProgressExactMinute"), TimeSpan.FromSeconds((double)progressMessage.SecondsLeft).TotalMinutes.ToString("F0"));
								}
							}
						}
						this.Message = string.Format(LocalizationManager.GetTranslation("DownloadingFiles"), ComputerUnitsConverter.SizeToString(progressMessage.DownloadedSize), ComputerUnitsConverter.SizeToString(progressMessage.TotalSize), text);
					}
					else
					{
						this.Message = LocalizationManager.GetTranslation(progressMessage.Message);
					}
				}
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00010488 File Offset: 0x0000E688
		private void OnLiveRegionChanged()
		{
			EventHandler liveRegionChanged = this.LiveRegionChanged;
			bool flag = liveRegionChanged != null;
			if (flag)
			{
				liveRegionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000147 RID: 327
		private bool progressUpdated;

		// Token: 0x04000148 RID: 328
		private int progress;

		// Token: 0x04000149 RID: 329
		private string liveText;

		// Token: 0x0400014A RID: 330
		private string message;

		// Token: 0x0400014B RID: 331
		private string timeLeftMessage;

		// Token: 0x0400014C RID: 332
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;
	}
}
