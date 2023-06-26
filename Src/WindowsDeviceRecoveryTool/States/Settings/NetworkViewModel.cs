using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x02000030 RID: 48
	[Export]
	public class NetworkViewModel : BaseViewModel
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0000C4CA File Offset: 0x0000A6CA
		[ImportingConstructor]
		public NetworkViewModel(EventAggregator eventAggregator)
		{
			this.eventAggregator = eventAggregator;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
		public bool UseManualProxy
		{
			get
			{
				return Settings.Default.UseManualProxy;
			}
			set
			{
				base.SetValue<bool>(() => this.UseManualProxy, delegate
				{
					Settings.Default.UseManualProxy = value;
				});
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000C55C File Offset: 0x0000A75C
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000C584 File Offset: 0x0000A784
		public string Password
		{
			get
			{
				return new Credentials().DecryptString(Settings.Default.ProxyPassword);
			}
			set
			{
				Settings.Default.ProxyPassword = new Credentials().EncryptString(value);
				base.RaisePropertyChanged<string>(() => this.Password);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		public string ProxyUsername
		{
			get
			{
				return Settings.Default.ProxyUsername;
			}
			set
			{
				base.SetValue<string>(() => this.ProxyUsername, delegate
				{
					Settings.Default.ProxyUsername = value;
				});
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000C660 File Offset: 0x0000A860
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000C67C File Offset: 0x0000A87C
		public int ProxyPort
		{
			get
			{
				return Settings.Default.ProxyPort;
			}
			set
			{
				base.SetValue<int>(() => this.ProxyPort, delegate
				{
					Settings.Default.ProxyPort = value;
				});
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		public string ProxyAddress
		{
			get
			{
				return Settings.Default.ProxyAddress;
			}
			set
			{
				base.SetValue<string>(() => this.ProxyAddress, delegate
				{
					Settings.Default.ProxyAddress = value;
				});
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000C760 File Offset: 0x0000A960
		public override void OnStarted()
		{
			this.eventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Settings"), LocalizationManager.GetTranslation("Network")));
			this.eventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			base.RaisePropertyChanged<int>(() => this.ProxyPort);
			base.RaisePropertyChanged<string>(() => this.ProxyAddress);
			base.RaisePropertyChanged<string>(() => this.ProxyUsername);
			base.RaisePropertyChanged<string>(() => this.Password);
			base.RaisePropertyChanged<bool>(() => this.UseManualProxy);
			base.OnStarted();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000C8BC File Offset: 0x0000AABC
		public override void OnStopped()
		{
			base.Commands.Run((SettingsController controller) => controller.SetProxy(null));
			Settings.Default.Save();
		}

		// Token: 0x04000100 RID: 256
		private readonly EventAggregator eventAggregator;
	}
}
