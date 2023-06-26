using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Msr;
using Microsoft.WindowsDeviceRecoveryTool.Messages;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Workflow
{
	// Token: 0x02000016 RID: 22
	[Export]
	public sealed class DownloadPackageViewModel : BaseViewModel, ICanHandle<ProgressMessage>, ICanHandle, INotifyLiveRegionChanged
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00007B02 File Offset: 0x00005D02
		[ImportingConstructor]
		public DownloadPackageViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
			this.uiUpdateAccessTimer = new IntervalResetAccessTimer(MsrDownloadConfig.Instance.DownloadProgressUpdateIntervalMillis, true);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000103 RID: 259 RVA: 0x00007B2C File Offset: 0x00005D2C
		// (remove) Token: 0x06000104 RID: 260 RVA: 0x00007B64 File Offset: 0x00005D64
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler LiveRegionChanged;

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00007B9C File Offset: 0x00005D9C
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00007BB4 File Offset: 0x00005DB4
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00007BF4 File Offset: 0x00005DF4
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00007C0C File Offset: 0x00005E0C
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00007C4C File Offset: 0x00005E4C
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00007C64 File Offset: 0x00005E64
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00007CA4 File Offset: 0x00005EA4
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00007CBC File Offset: 0x00005EBC
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00007D24 File Offset: 0x00005F24
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00007D3C File Offset: 0x00005F3C
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00007D7C File Offset: 0x00005F7C
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00007D94 File Offset: 0x00005F94
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

		// Token: 0x06000111 RID: 273 RVA: 0x00007DD4 File Offset: 0x00005FD4
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("DownloadingPackage"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(true, LocalizationManager.GetTranslation("DownloadingCancelMessage"), null));
			this.ProgressUpdated = false;
			this.Message = string.Empty;
			this.TimeLeftMessage = string.Empty;
			this.LiveText = string.Empty;
			this.progressCount = 0L;
			this.uiUpdateAccessTimer.StartTimer();
			base.Commands.Run((FlowController c) => c.DownloadPackage(null, CancellationToken.None));
			this.LiveText = LocalizationManager.GetTranslation("DownloadStarted");
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007EFF File Offset: 0x000060FF
		public override void OnStopped()
		{
			base.OnStopped();
			this.uiUpdateAccessTimer.StopTimer();
			this.LiveText = LocalizationManager.GetTranslation("DownloadCompleted");
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007F28 File Offset: 0x00006128
		public void Handle(ProgressMessage progressMessage)
		{
			long num = this.progressCount + 1L;
			this.progressCount = num;
			bool flag = num % (long)(MsrDownloadConfig.Instance.NumberOfProgressEventsToSkipInUI + 1) != 0L;
			if (!flag)
			{
				bool isStarted = base.IsStarted;
				if (isStarted)
				{
					this.uiUpdateAccessTimer.RunIfAccessAvailable(delegate
					{
						this.Progress = progressMessage.Progress;
						Tracer<DownloadPackageViewModel>.WriteVerbose("Download progress: {0}", new object[] { this.progressCount });
						bool flag2 = !string.IsNullOrEmpty(progressMessage.Message);
						if (flag2)
						{
							bool flag3 = progressMessage.Message == "DownloadingFiles";
							if (flag3)
							{
								this.ProgressUpdated = true;
								string text = "...";
								bool flag4 = progressMessage.BytesPerSecond > 0.0;
								if (flag4)
								{
									text = ComputerUnitsConverter.SpeedToString(progressMessage.BytesPerSecond);
									bool flag5 = progressMessage.SecondsLeft < 60L;
									if (flag5)
									{
										this.TimeLeftMessage = LocalizationManager.GetTranslation("DownloadProgressMinuteLess");
									}
									else
									{
										bool flag6 = progressMessage.SecondsLeft > 60L && progressMessage.SecondsLeft < 120L;
										if (flag6)
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
					});
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007F9C File Offset: 0x0000619C
		private void OnLiveRegionChanged()
		{
			EventHandler liveRegionChanged = this.LiveRegionChanged;
			bool flag = liveRegionChanged != null;
			if (flag)
			{
				liveRegionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000099 RID: 153
		private long progressCount;

		// Token: 0x0400009A RID: 154
		private bool progressUpdated;

		// Token: 0x0400009B RID: 155
		private int progress;

		// Token: 0x0400009C RID: 156
		private string liveText;

		// Token: 0x0400009D RID: 157
		private string message;

		// Token: 0x0400009E RID: 158
		private string timeLeftMessage;

		// Token: 0x0400009F RID: 159
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x040000A0 RID: 160
		private readonly IntervalResetAccessTimer uiUpdateAccessTimer;
	}
}
