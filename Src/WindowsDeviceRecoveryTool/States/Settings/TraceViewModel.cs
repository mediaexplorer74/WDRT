using System;
using System.ComponentModel.Composition;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x02000035 RID: 53
	[Export]
	public class TraceViewModel : BaseViewModel, ICanHandle<TraceParametersMessage>, ICanHandle, ICanHandle<ApplicationDataSizeMessage>
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000D640 File Offset: 0x0000B840
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000D664 File Offset: 0x0000B864
		public string ZipFilePath
		{
			get
			{
				return Environment.ExpandEnvironmentVariables(Settings.Default.ZipFilePath);
			}
			set
			{
				Settings.Default.ZipFilePath = value;
				base.RaisePropertyChanged<string>(() => this.ZipFilePath);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		public bool ExportEnable
		{
			get
			{
				return this.TraceEnabled || this.logsSize > 0L;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000D6DC File Offset: 0x0000B8DC
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		public bool TraceEnabled
		{
			get
			{
				return Settings.Default.TraceEnabled;
			}
			set
			{
				Settings.Default.TraceEnabled = value;
				base.Commands.Run((SettingsController c) => c.SetTraceEnabled(value, CancellationToken.None));
				bool flag = !value;
				if (flag)
				{
					base.Commands.Run((SettingsController controller) => controller.CalculateLogsSize(null, CancellationToken.None));
				}
				base.RaisePropertyChanged<bool>(() => this.ExportEnable);
				base.RaisePropertyChanged<bool>(() => this.TraceEnabled);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		public void Handle(TraceParametersMessage message)
		{
			bool flag = !string.IsNullOrWhiteSpace(message.LogZipFilePath);
			if (flag)
			{
				this.ZipFilePath = message.LogZipFilePath;
			}
			base.RaisePropertyChanged<string>(() => this.ZipFilePath);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000D073 File Offset: 0x0000B273
		public override void OnStopped()
		{
			Settings.Default.Save();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D90C File Offset: 0x0000BB0C
		public override void OnStarted()
		{
			base.RaisePropertyChanged<string>(() => this.ZipFilePath);
			bool flag = !this.TraceEnabled;
			if (flag)
			{
				base.Commands.Run((SettingsController controller) => controller.CalculateLogsSize(null, CancellationToken.None));
			}
			base.RaisePropertyChanged<bool>(() => this.TraceEnabled);
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Settings"), LocalizationManager.GetTranslation("Troubleshooting")));
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000DA30 File Offset: 0x0000BC30
		public void Handle(ApplicationDataSizeMessage message)
		{
			bool flag = base.IsStarted && message.Type == ApplicationDataSizeMessage.DataType.Logs;
			if (flag)
			{
				this.logsSize = message.FilesSize;
				base.RaisePropertyChanged<bool>(() => this.ExportEnable);
			}
		}

		// Token: 0x0400011A RID: 282
		private long logsSize = 0L;
	}
}
