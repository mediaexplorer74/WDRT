using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Workflow
{
	// Token: 0x0200000C RID: 12
	[Export]
	public sealed class DownloadEmergencyPackageViewModel : BaseViewModel, ICanHandle<ProgressMessage>, ICanHandle, INotifyLiveRegionChanged
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00004B18 File Offset: 0x00002D18
		[ImportingConstructor]
		public DownloadEmergencyPackageViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600007D RID: 125 RVA: 0x00004B2C File Offset: 0x00002D2C
		// (remove) Token: 0x0600007E RID: 126 RVA: 0x00004B64 File Offset: 0x00002D64
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler LiveRegionChanged;

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004B9C File Offset: 0x00002D9C
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00004BB4 File Offset: 0x00002DB4
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004BF4 File Offset: 0x00002DF4
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00004C0C File Offset: 0x00002E0C
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004C4C File Offset: 0x00002E4C
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00004C64 File Offset: 0x00002E64
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004CA4 File Offset: 0x00002EA4
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00004CBC File Offset: 0x00002EBC
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004D24 File Offset: 0x00002F24
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00004D3C File Offset: 0x00002F3C
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00004D7C File Offset: 0x00002F7C
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00004D94 File Offset: 0x00002F94
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

		// Token: 0x0600008B RID: 139 RVA: 0x00004DD4 File Offset: 0x00002FD4
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("DownloadingEmergencyPackage"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(true, LocalizationManager.GetTranslation("DownloadingCancelMessage"), null));
			this.ProgressUpdated = false;
			this.Message = string.Empty;
			this.TimeLeftMessage = string.Empty;
			this.LiveText = string.Empty;
			bool flag = string.IsNullOrWhiteSpace(this.AppContext.CurrentPhone.HardwareModel);
			if (flag)
			{
				VariantInfo variantInfo = VariantInfo.GetVariantInfo(this.AppContext.CurrentPhone.PackageFilePath);
				this.AppContext.CurrentPhone.HardwareModel = variantInfo.ProductType;
			}
			Tracer<DownloadEmergencyPackageViewModel>.WriteInformation("Selected device type: {0}", new object[] { this.AppContext.CurrentPhone.HardwareModel });
			base.Commands.Run((FlowController c) => c.DownloadEmergencyPackage(null, CancellationToken.None));
			this.LiveText = LocalizationManager.GetTranslation("DownloadStarted");
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004F57 File Offset: 0x00003157
		public override void OnStopped()
		{
			base.OnStopped();
			this.LiveText = LocalizationManager.GetTranslation("DownloadCompleted");
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004F74 File Offset: 0x00003174
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
						bool flag3 = progressMessage.TotalSize > 0L;
						if (flag3)
						{
							this.ProgressUpdated = true;
						}
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
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000050F0 File Offset: 0x000032F0
		private void OnLiveRegionChanged()
		{
			EventHandler liveRegionChanged = this.LiveRegionChanged;
			bool flag = liveRegionChanged != null;
			if (flag)
			{
				liveRegionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000066 RID: 102
		private bool progressUpdated;

		// Token: 0x04000067 RID: 103
		private int progress;

		// Token: 0x04000068 RID: 104
		private string liveText;

		// Token: 0x04000069 RID: 105
		private string message;

		// Token: 0x0400006A RID: 106
		private string timeLeftMessage;

		// Token: 0x0400006B RID: 107
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;
	}
}
