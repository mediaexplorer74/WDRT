using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Workflow
{
	// Token: 0x02000015 RID: 21
	[Export]
	public class AutomaticPackageSelectionViewModel : BaseViewModel, ICanHandle<PackageDirectoryMessage>, ICanHandle, ICanHandle<CompatibleFfuFilesMessage>, ICanHandle<DeviceConnectionStatusReadMessage>
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000072AC File Offset: 0x000054AC
		[ImportingConstructor]
		public AutomaticPackageSelectionViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.appContext = appContext;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000072C0 File Offset: 0x000054C0
		// (set) Token: 0x060000ED RID: 237 RVA: 0x000072D8 File Offset: 0x000054D8
		public List<PackageFileInfo> FoundPackages
		{
			get
			{
				return this.foundPackages;
			}
			set
			{
				base.SetValue<List<PackageFileInfo>>(() => this.FoundPackages, ref this.foundPackages, value);
				base.RaisePropertyChanged<bool>(() => this.PackagesListVisible);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000735C File Offset: 0x0000555C
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00007374 File Offset: 0x00005574
		public PackageFileInfo SelectedPackage
		{
			get
			{
				return this.selectedPackage;
			}
			set
			{
				base.SetValue<PackageFileInfo>(() => this.SelectedPackage, ref this.selectedPackage, value);
				bool flag = value != null;
				if (flag)
				{
					this.appContext.CurrentPhone.PackageFilePath = value.Path;
					this.appContext.CurrentPhone.PackageFileInfo = value;
				}
				this.NextButtonEnabled = value != null;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00007400 File Offset: 0x00005600
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00007418 File Offset: 0x00005618
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00007458 File Offset: 0x00005658
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00007470 File Offset: 0x00005670
		public string StatusInfo
		{
			get
			{
				return this.statusInfo;
			}
			set
			{
				base.SetValue<string>(() => this.StatusInfo, ref this.statusInfo, value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000074B0 File Offset: 0x000056B0
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000074C8 File Offset: 0x000056C8
		public bool CheckingPackageDirectory
		{
			get
			{
				return this.checkingPackageDirectory;
			}
			set
			{
				base.SetValue<bool>(() => this.CheckingPackageDirectory, ref this.checkingPackageDirectory, value);
				base.RaisePropertyChanged<bool>(() => this.PackagesListVisible);
				this.NextButtonEnabled = false;
				this.FoundPackages = null;
				this.PackagePath = string.Empty;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00007568 File Offset: 0x00005768
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00007580 File Offset: 0x00005780
		public bool NextButtonEnabled
		{
			get
			{
				return this.nextButtonEnabled;
			}
			set
			{
				base.SetValue<bool>(() => this.NextButtonEnabled, ref this.nextButtonEnabled, value);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000075C0 File Offset: 0x000057C0
		public bool PackagesListVisible
		{
			get
			{
				bool flag = this.FoundPackages != null;
				return flag && this.FoundPackages.Any<PackageFileInfo>();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000075F0 File Offset: 0x000057F0
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00007608 File Offset: 0x00005808
		public string PackageDirectory
		{
			get
			{
				return this.packageDirectory;
			}
			set
			{
				base.SetValue<string>(() => this.PackageDirectory, ref this.packageDirectory, value);
				base.RaisePropertyChanged<string>(() => this.SelectedDirectoryDescription);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000768C File Offset: 0x0000588C
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000076A4 File Offset: 0x000058A4
		public string PackagePath
		{
			get
			{
				return this.packagePath;
			}
			set
			{
				base.SetValue<string>(() => this.PackagePath, ref this.packagePath, value);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000076E4 File Offset: 0x000058E4
		public string SelectedDirectoryDescription
		{
			get
			{
				bool flag = !string.IsNullOrEmpty(this.PackageDirectory);
				string text;
				if (flag)
				{
					text = string.Format(LocalizationManager.GetTranslation("PackageDirectory"), this.PackageDirectory);
				}
				else
				{
					text = LocalizationManager.GetTranslation("DirectoryNotSet");
				}
				return text;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000772C File Offset: 0x0000592C
		public override void OnStarted()
		{
			this.PackageDirectory = FileSystemInfo.GetCustomProductsPath();
			bool flag = string.IsNullOrEmpty(this.PackageDirectory);
			if (flag)
			{
				this.PackageDirectory = FileSystemInfo.GetLumiaPackagesPath("");
			}
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("AutomaticPackageSelection"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			base.OnStarted();
			this.FoundPackages = null;
			this.PackagePath = string.Empty;
			this.CheckingPackageDirectory = true;
			base.Commands.Run((FlowController c) => c.CheckIfDeviceStillConnected(this.AppContext.CurrentPhone, CancellationToken.None));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007870 File Offset: 0x00005A70
		public void Handle(PackageDirectoryMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				this.PackageDirectory = message.Directory;
				bool flag = !string.IsNullOrEmpty(this.PackageDirectory);
				if (flag)
				{
					this.CheckingPackageDirectory = true;
				}
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000078B4 File Offset: 0x00005AB4
		public void Handle(CompatibleFfuFilesMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				this.CheckingPackageDirectory = false;
				int count = message.Packages.Count;
				int num = count;
				if (num != 0)
				{
					if (num != 1)
					{
						this.StatusInfo = string.Format(LocalizationManager.GetTranslation("MoreThanOnePackageFound"), LocalizationManager.GetTranslation("ButtonNext"));
						this.FoundPackages = message.Packages;
					}
					else
					{
						this.NextButtonEnabled = true;
						this.StatusInfo = string.Format(LocalizationManager.GetTranslation("OnePackageFound"), message.Packages.First<PackageFileInfo>().PackageId, LocalizationManager.GetTranslation("ButtonNext"));
						PackageFileInfo packageFileInfo = message.Packages.First<PackageFileInfo>();
						this.appContext.CurrentPhone.PackageFileInfo = packageFileInfo;
						this.appContext.CurrentPhone.PackageFilePath = packageFileInfo.Path;
						this.PackagePath = packageFileInfo.Path;
					}
				}
				else
				{
					this.StatusInfo = LocalizationManager.GetTranslation("NoPackagesFoundSelectAnotherDirectory");
				}
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000079B8 File Offset: 0x00005BB8
		public void Handle(DeviceConnectionStatusReadMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				bool status = message.Status;
				if (!status)
				{
					throw new DeviceNotFoundException();
				}
				bool flag = this.AppContext.CurrentPhone == null || this.AppContext.CurrentPhone.PlatformId == null;
				if (flag)
				{
					base.Commands.Run((AppController c) => c.SwitchToState("ManualPackageSelectionState"));
				}
				base.Commands.Run((FlowController c) => c.FindCorrectPackage(this.PackageDirectory, CancellationToken.None));
			}
		}

		// Token: 0x04000091 RID: 145
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x04000092 RID: 146
		private string statusInfo;

		// Token: 0x04000093 RID: 147
		private string packageDirectory;

		// Token: 0x04000094 RID: 148
		private string packagePath;

		// Token: 0x04000095 RID: 149
		private bool checkingPackageDirectory;

		// Token: 0x04000096 RID: 150
		private bool nextButtonEnabled;

		// Token: 0x04000097 RID: 151
		private List<PackageFileInfo> foundPackages;

		// Token: 0x04000098 RID: 152
		private PackageFileInfo selectedPackage;
	}
}
