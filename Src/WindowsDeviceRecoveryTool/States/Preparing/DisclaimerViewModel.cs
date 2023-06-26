using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x0200004F RID: 79
	[Export]
	public class DisclaimerViewModel : BaseViewModel, ICanHandle<DeviceConnectionStatusReadMessage>, ICanHandle
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0001122C File Offset: 0x0000F42C
		[ImportingConstructor]
		public DisclaimerViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.appContext = appContext;
			this.NextButtonCommand = new DelegateCommand<object>(new Action<object>(this.NextButtonSelected));
			this.SurveyCommand = new DelegateCommand<object>(new Action<object>(this.OpenSurvey));
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00011278 File Offset: 0x0000F478
		// (set) Token: 0x06000311 RID: 785 RVA: 0x00011280 File Offset: 0x0000F480
		public ICommand SurveyCommand { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00011289 File Offset: 0x0000F489
		// (set) Token: 0x06000313 RID: 787 RVA: 0x00011291 File Offset: 0x0000F491
		public ICommand NextButtonCommand { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0001129C File Offset: 0x0000F49C
		// (set) Token: 0x06000315 RID: 789 RVA: 0x000112B4 File Offset: 0x0000F4B4
		public string PhoneSettingsBackupPath
		{
			get
			{
				return this.phoneSettingsBackupPath;
			}
			set
			{
				base.SetValue<string>(() => this.PhoneSettingsBackupPath, ref this.phoneSettingsBackupPath, value);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000316 RID: 790 RVA: 0x000112F4 File Offset: 0x0000F4F4
		public override string PreviousStateName
		{
			get
			{
				return "AutomaticManufacturerSelectionState";
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001130C File Offset: 0x0000F50C
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("DisclaimerHeader"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			this.PhoneSettingsBackupPath = LocalizationManager.GetTranslation(this.appContext.CurrentPhone.IsWp10Device() ? "PhoneSettingsBackupPathWin10" : "PhoneSettingsBackupPathWin8");
			bool flag = this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.Type == PhoneTypes.Analog;
			if (flag)
			{
				base.Commands.Run((FlowController c) => c.CheckIfDeviceStillConnected(this.appContext.CurrentPhone, CancellationToken.None));
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00011444 File Offset: 0x0000F644
		private void NextButtonSelected(object obj)
		{
			bool flag = this.appContext.CurrentPhone.IsPhoneDeviceType();
			if (flag)
			{
				base.Commands.Run((AppController c) => c.SwitchToState("SurveyState"));
			}
			else
			{
				bool offlinePackage = this.appContext.CurrentPhone.PackageFileInfo.OfflinePackage;
				if (offlinePackage)
				{
					base.Commands.Run((AppController c) => c.SwitchToState("PackageIntegrityCheckState"));
				}
				else
				{
					base.Commands.Run((AppController c) => c.SwitchToState("DownloadPackageState"));
				}
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000115B4 File Offset: 0x0000F7B4
		public void Handle(DeviceConnectionStatusReadMessage message)
		{
			bool flag = base.IsStarted && !message.Status;
			if (flag)
			{
				throw new DeviceNotFoundException();
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000115E4 File Offset: 0x0000F7E4
		private void OpenSurvey(object obj)
		{
			base.Commands.Run((AppController c) => c.SwitchToState("SurveyState"));
		}

		// Token: 0x0400015D RID: 349
		private readonly Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x0400015E RID: 350
		private string phoneSettingsBackupPath;
	}
}
