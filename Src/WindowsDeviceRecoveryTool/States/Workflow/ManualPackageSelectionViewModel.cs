using System;
using System.ComponentModel.Composition;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Workflow
{
	// Token: 0x0200001D RID: 29
	[Export]
	public class ManualPackageSelectionViewModel : BaseViewModel, ICanHandle<PackageDirectoryMessage>, ICanHandle, ICanHandle<FfuFilePlatformIdMessage>
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00008CEC File Offset: 0x00006EEC
		[ImportingConstructor]
		public ManualPackageSelectionViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.appContext = appContext;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00008D00 File Offset: 0x00006F00
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00008D18 File Offset: 0x00006F18
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

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00008D58 File Offset: 0x00006F58
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00008D98 File Offset: 0x00006F98
		public string FfuFilePath
		{
			get
			{
				bool flag = this.appContext.CurrentPhone != null;
				string text;
				if (flag)
				{
					text = this.appContext.CurrentPhone.PackageFilePath;
				}
				else
				{
					text = string.Empty;
				}
				return text;
			}
			set
			{
				bool flag = this.appContext.CurrentPhone != null;
				if (flag)
				{
					base.SetValue<string>(() => this.FfuFilePath, delegate
					{
						this.appContext.CurrentPhone.PackageFilePath = value;
					});
					base.RaisePropertyChanged<string>(() => this.FilePathDescription);
					base.RaisePropertyChanged<bool>(() => this.IsNextCommandEnabled);
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00008E80 File Offset: 0x00007080
		public string FilePathDescription
		{
			get
			{
				bool flag = !string.IsNullOrEmpty(this.FfuFilePath);
				string text;
				if (flag)
				{
					text = string.Format(LocalizationManager.GetTranslation("PackageFilePath"), this.FfuFilePath);
				}
				else
				{
					text = LocalizationManager.GetTranslation("PackagePathNotSet");
				}
				return text;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00008EC8 File Offset: 0x000070C8
		public bool IsNextCommandEnabled
		{
			get
			{
				bool flag = this.appContext.CurrentPhone != null && (this.PlatformId != null || this.appContext.CurrentPhone.Type == PhoneTypes.Lumia);
				return flag && !string.IsNullOrEmpty(this.FfuFilePath) && this.Compatibility;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00008F28 File Offset: 0x00007128
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00008F40 File Offset: 0x00007140
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00008F80 File Offset: 0x00007180
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00008F98 File Offset: 0x00007198
		public PlatformId PlatformId
		{
			get
			{
				return this.platformId;
			}
			set
			{
				base.SetValue<PlatformId>(() => this.PlatformId, ref this.platformId, value);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00008FD8 File Offset: 0x000071D8
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00008FF0 File Offset: 0x000071F0
		public bool Compatibility
		{
			get
			{
				return this.compatibility;
			}
			set
			{
				base.SetValue<bool>(() => this.Compatibility, ref this.compatibility, value);
				base.RaisePropertyChanged<bool>(() => this.IsNextCommandEnabled);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00009074 File Offset: 0x00007274
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000908C File Offset: 0x0000728C
		public bool CheckingPlatformId
		{
			get
			{
				return this.checkingPlatformId;
			}
			set
			{
				base.SetValue<bool>(() => this.CheckingPlatformId, ref this.checkingPlatformId, value);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000090CC File Offset: 0x000072CC
		public void Handle(PackageDirectoryMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				this.PlatformId = null;
				this.FfuFilePath = message.Directory;
				bool flag = !string.IsNullOrEmpty(this.FfuFilePath) && this.appContext.CurrentPhone.Type > PhoneTypes.Lumia;
				if (flag)
				{
					this.CheckingPlatformId = true;
					this.StatusInfo = LocalizationManager.GetTranslation("FFUCheckingFile");
					base.Commands.Run((FfuController c) => c.ReadFfuFilePlatformId(this.FfuFilePath, CancellationToken.None));
				}
				else
				{
					this.CheckCompatibility();
				}
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000091D8 File Offset: 0x000073D8
		public void Handle(FfuFilePlatformIdMessage message)
		{
			this.PlatformId = message.PlatformId;
			this.CheckingPlatformId = false;
			bool flag = this.appContext.CurrentPhone == null;
			if (!flag)
			{
				this.CheckCompatibility();
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00009218 File Offset: 0x00007418
		private void CheckCompatibility()
		{
			bool flag = this.appContext.CurrentPhone.Type == PhoneTypes.Analog;
			if (flag)
			{
				this.Compatibility = true;
				this.StatusInfo = (this.PlatformId.IsCompatibleWithDevicePlatformId(this.appContext.CurrentPhone.PlatformId) ? "File OK" : "Package do not match the device!");
			}
			else
			{
				bool flag2 = this.appContext.CurrentPhone.Type > PhoneTypes.Lumia;
				if (flag2)
				{
					this.Compatibility = this.PlatformId.IsCompatibleWithDevicePlatformId(this.appContext.CurrentPhone.PlatformId);
					this.StatusInfo = (this.Compatibility ? "File OK" : "Selected file is not compatible with connected phone!");
				}
				else
				{
					this.Compatibility = true;
					this.StatusInfo = "Compatibility forced to match for Lumia phones";
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000092E8 File Offset: 0x000074E8
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("PackageSelection"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			this.FfuFilePath = string.Empty;
			this.PlatformId = null;
			this.StatusInfo = string.Empty;
			this.Compatibility = false;
		}

		// Token: 0x040000B7 RID: 183
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x040000B8 RID: 184
		private PlatformId platformId;

		// Token: 0x040000B9 RID: 185
		private bool checkingPlatformId;

		// Token: 0x040000BA RID: 186
		private bool compatibility;

		// Token: 0x040000BB RID: 187
		private string statusInfo;
	}
}
