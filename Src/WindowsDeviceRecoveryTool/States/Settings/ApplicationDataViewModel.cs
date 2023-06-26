using System;
using System.ComponentModel.Composition;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x02000026 RID: 38
	[Export]
	public class ApplicationDataViewModel : BaseViewModel, ICanHandle<ApplicationDataSizeMessage>, ICanHandle, ICanHandle<ApplicationInvalidateSizeMessage>
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000A1D0 File Offset: 0x000083D0
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000A1E8 File Offset: 0x000083E8
		public long LogFilesSize
		{
			get
			{
				return this.logFilesSize;
			}
			set
			{
				bool flag = this.logFilesSize != value;
				if (flag)
				{
					base.SetValue<long>(() => this.LogFilesSize, ref this.logFilesSize, value);
					base.RaisePropertyChanged<string>(() => this.LogFilesSizeString);
					base.RaisePropertyChanged<long>(() => this.AllFilesSize);
					base.RaisePropertyChanged<string>(() => this.AllFilesSizeString);
				}
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000A2EC File Offset: 0x000084EC
		public string LogFilesSizeString
		{
			get
			{
				return ComputerUnitsConverter.SizeToString(this.LogFilesSize);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000A30C File Offset: 0x0000850C
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0000A324 File Offset: 0x00008524
		public long ReportsFilesSize
		{
			get
			{
				return this.reportsFilesSize;
			}
			set
			{
				bool flag = this.reportsFilesSize != value;
				if (flag)
				{
					base.SetValue<long>(() => this.ReportsFilesSize, ref this.reportsFilesSize, value);
					base.RaisePropertyChanged<string>(() => this.ReportsFilesSizeString);
					base.RaisePropertyChanged<long>(() => this.AllFilesSize);
					base.RaisePropertyChanged<string>(() => this.AllFilesSizeString);
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000A428 File Offset: 0x00008628
		public string ReportsFilesSizeString
		{
			get
			{
				return ComputerUnitsConverter.SizeToString(this.ReportsFilesSize);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000A448 File Offset: 0x00008648
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000A460 File Offset: 0x00008660
		public long PackagesFilesSize
		{
			get
			{
				return this.packagesFilesSize;
			}
			set
			{
				bool flag = this.packagesFilesSize != value;
				if (flag)
				{
					base.SetValue<long>(() => this.PackagesFilesSize, ref this.packagesFilesSize, value);
					base.RaisePropertyChanged<string>(() => this.PackagesFilesSizeString);
					base.RaisePropertyChanged<long>(() => this.AllFilesSize);
					base.RaisePropertyChanged<string>(() => this.AllFilesSizeString);
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000A564 File Offset: 0x00008764
		public string PackagesFilesSizeString
		{
			get
			{
				return ComputerUnitsConverter.SizeToString(this.PackagesFilesSize);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000A584 File Offset: 0x00008784
		public long AllFilesSize
		{
			get
			{
				return this.LogFilesSize + this.ReportsFilesSize + this.PackagesFilesSize;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000A5AC File Offset: 0x000087AC
		public bool AllCalculationCompleted
		{
			get
			{
				return !this.LogsCalculationInProgress && !this.ReportsCalculationInProgress && !this.PackagesCalculationInProgress;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000A5DC File Offset: 0x000087DC
		public string AllFilesSizeString
		{
			get
			{
				return ComputerUnitsConverter.SizeToString(this.AllFilesSize);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000A5FC File Offset: 0x000087FC
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000A614 File Offset: 0x00008814
		public bool LogsCalculationInProgress
		{
			get
			{
				return this.logsCalculationInProgress;
			}
			set
			{
				base.SetValue<bool>(() => this.LogsCalculationInProgress, ref this.logsCalculationInProgress, value);
				base.RaisePropertyChanged<bool>(() => this.AllCalculationCompleted);
				base.RaisePropertyChanged<bool>(() => this.CleanAllAppDataBtnEnabled);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000A6CC File Offset: 0x000088CC
		// (set) Token: 0x060001AC RID: 428 RVA: 0x0000A6E4 File Offset: 0x000088E4
		public bool ReportsCalculationInProgress
		{
			get
			{
				return this.reportsCalculationInProgress;
			}
			set
			{
				base.SetValue<bool>(() => this.ReportsCalculationInProgress, ref this.reportsCalculationInProgress, value);
				base.RaisePropertyChanged<bool>(() => this.AllCalculationCompleted);
				base.RaisePropertyChanged<bool>(() => this.CleanAllAppDataBtnEnabled);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000A79C File Offset: 0x0000899C
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000A7B4 File Offset: 0x000089B4
		public bool PackagesCalculationInProgress
		{
			get
			{
				return this.packagesCalculationInProgress;
			}
			set
			{
				base.SetValue<bool>(() => this.PackagesCalculationInProgress, ref this.packagesCalculationInProgress, value);
				base.RaisePropertyChanged<bool>(() => this.AllCalculationCompleted);
				base.RaisePropertyChanged<bool>(() => this.CleanAllAppDataBtnEnabled);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000A86C File Offset: 0x00008A6C
		public bool CleanAllAppDataBtnEnabled
		{
			get
			{
				return this.AllCalculationCompleted && this.AllFilesSize > 0L;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000A893 File Offset: 0x00008A93
		public override void OnStopped()
		{
			this.cts.Cancel();
			Settings.Default.Save();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Settings"), LocalizationManager.GetTranslation("ApplicationData")));
			this.cts = new CancellationTokenSource();
			this.LogFilesSize = 0L;
			this.ReportsFilesSize = 0L;
			this.PackagesFilesSize = 0L;
			this.LogsCalculationInProgress = true;
			this.ReportsCalculationInProgress = true;
			this.PackagesCalculationInProgress = true;
			base.Commands.Run((SettingsController controller) => controller.CalculateLogsSize(null, this.cts.Token));
			base.Commands.Run((SettingsController controller) => controller.CalculateReportsSize(null, this.cts.Token));
			base.Commands.Run((SettingsController controller) => controller.CalculatePackagesSize(null, this.cts.Token));
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public void Handle(ApplicationDataSizeMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				switch (message.Type)
				{
				case ApplicationDataSizeMessage.DataType.Logs:
					this.LogFilesSize = message.FilesSize;
					this.LogsCalculationInProgress = false;
					break;
				case ApplicationDataSizeMessage.DataType.Reports:
					this.ReportsFilesSize = message.FilesSize;
					this.ReportsCalculationInProgress = false;
					break;
				case ApplicationDataSizeMessage.DataType.Packages:
					this.PackagesFilesSize = message.FilesSize;
					this.PackagesCalculationInProgress = false;
					break;
				}
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000AB5C File Offset: 0x00008D5C
		public void Handle(ApplicationInvalidateSizeMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				switch (message.Type)
				{
				case ApplicationInvalidateSizeMessage.DataType.Logs:
					this.LogFilesSize = 0L;
					this.LogsCalculationInProgress = true;
					base.Commands.Run((SettingsController controller) => controller.CalculateLogsSize(null, this.cts.Token));
					break;
				case ApplicationInvalidateSizeMessage.DataType.Reports:
					this.ReportsFilesSize = 0L;
					this.ReportsCalculationInProgress = true;
					base.Commands.Run((SettingsController controller) => controller.CalculateReportsSize(null, this.cts.Token));
					break;
				case ApplicationInvalidateSizeMessage.DataType.Packages:
					this.PackagesFilesSize = 0L;
					this.PackagesCalculationInProgress = true;
					base.Commands.Run((SettingsController controller) => controller.CalculatePackagesSize(null, this.cts.Token));
					break;
				}
			}
		}

		// Token: 0x040000D3 RID: 211
		private long logFilesSize;

		// Token: 0x040000D4 RID: 212
		private long reportsFilesSize;

		// Token: 0x040000D5 RID: 213
		private long packagesFilesSize;

		// Token: 0x040000D6 RID: 214
		private bool logsCalculationInProgress;

		// Token: 0x040000D7 RID: 215
		private bool reportsCalculationInProgress;

		// Token: 0x040000D8 RID: 216
		private bool packagesCalculationInProgress;

		// Token: 0x040000D9 RID: 217
		private CancellationTokenSource cts;
	}
}
