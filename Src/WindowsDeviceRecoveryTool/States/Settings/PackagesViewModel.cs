using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x02000029 RID: 41
	[Export]
	public class PackagesViewModel : BaseViewModel, ICanHandle<PackageDirectoryMessage>, ICanHandle
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public string PackagesPath
		{
			get
			{
				return Settings.Default.PackagesPath;
			}
			set
			{
				base.SetValue<string>(() => this.PackagesPath, delegate
				{
					Settings.Default.PackagesPath = value;
				});
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000BC20 File Offset: 0x00009E20
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000BC3C File Offset: 0x00009E3C
		public bool CustomPackagesPathEnabled
		{
			get
			{
				return Settings.Default.CustomPackagesPathEnabled;
			}
			set
			{
				Settings.Default.CustomPackagesPathEnabled = value;
				FileSystemInfo.CustomPackagesPath = (value ? this.PackagesPath : string.Empty);
				base.RaisePropertyChanged<bool>(() => this.CustomPackagesPathEnabled);
				base.RaisePropertyChanged<bool>(() => this.CustomPathVisible);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000BCD8 File Offset: 0x00009ED8
		public bool CustomPathVisible
		{
			get
			{
				return this.CustomPackagesPathEnabled && !string.IsNullOrEmpty(this.PackagesPath);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000BD04 File Offset: 0x00009F04
		public override void OnStopped()
		{
			bool flag = this.CustomPackagesPathEnabled && string.IsNullOrEmpty(this.PackagesPath);
			if (flag)
			{
				this.CustomPackagesPathEnabled = false;
			}
			else
			{
				Settings.Default.Save();
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000BD44 File Offset: 0x00009F44
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Settings"), LocalizationManager.GetTranslation("Packages")));
			base.RaisePropertyChanged<bool>(() => this.CustomPackagesPathEnabled);
			base.RaisePropertyChanged<string>(() => this.PackagesPath);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		public void Handle(PackageDirectoryMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				this.PackagesPath = message.Directory;
				this.CustomPackagesPathEnabled = !string.IsNullOrEmpty(this.PackagesPath);
				base.RaisePropertyChanged<bool>(() => this.CustomPathVisible);
			}
		}
	}
}
