using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Services;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Workflow
{
	// Token: 0x0200000F RID: 15
	[Export]
	public class ManualDeviceTypeSelectionViewModel : BaseViewModel, ICanHandle<CompatibleFfuFilesMessage>, ICanHandle
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00005198 File Offset: 0x00003398
		[ImportingConstructor]
		public ManualDeviceTypeSelectionViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.appContext = appContext;
			this.SelectDeviceCommand = new DelegateCommand<object>(new Action<object>(this.DeviceSelected));
			this.ShowDeviceCannotBeRecoveredInfo = new DelegateCommand<object>(delegate(object o)
			{
				this.ChangeViewState();
			});
			this.FillSupportedDeviceTypes();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000051EB File Offset: 0x000033EB
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000051F3 File Offset: 0x000033F3
		public ICommand SelectDeviceCommand { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000051FC File Offset: 0x000033FC
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00005204 File Offset: 0x00003404
		public ICommand ShowDeviceCannotBeRecoveredInfo { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00005210 File Offset: 0x00003410
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00005228 File Offset: 0x00003428
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000052AC File Offset: 0x000034AC
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000052C4 File Offset: 0x000034C4
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00005350 File Offset: 0x00003550
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00005368 File Offset: 0x00003568
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000053A8 File Offset: 0x000035A8
		public bool PackagesListVisible
		{
			get
			{
				return this.FoundPackages != null && this.FoundPackages.Any<PackageFileInfo>();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000053D0 File Offset: 0x000035D0
		public override string PreviousStateName
		{
			get
			{
				return "AutomaticManufacturerSelectionState";
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000053E8 File Offset: 0x000035E8
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00005400 File Offset: 0x00003600
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00005440 File Offset: 0x00003640
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00005458 File Offset: 0x00003658
		internal ManualDeviceTypeSelectionState CurrentState
		{
			get
			{
				return this.currentState;
			}
			set
			{
				base.SetValue<ManualDeviceTypeSelectionState>(() => this.CurrentState, ref this.currentState, value);
				base.RaisePropertyChanged<bool>(() => this.DeviceCannotBeRecovered);
				base.RaisePropertyChanged<bool>(() => this.FfuSelection);
				base.RaisePropertyChanged<bool>(() => this.TypeDesignatorSelection);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005548 File Offset: 0x00003748
		public bool DeviceCannotBeRecovered
		{
			get
			{
				return this.currentState == ManualDeviceTypeSelectionState.DeviceCannotBeRecovered;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005564 File Offset: 0x00003764
		public bool FfuSelection
		{
			get
			{
				return this.currentState == ManualDeviceTypeSelectionState.FfuSelection;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005580 File Offset: 0x00003780
		public bool TypeDesignatorSelection
		{
			get
			{
				return this.currentState == ManualDeviceTypeSelectionState.TypeDesignatorSelection;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000559C File Offset: 0x0000379C
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000055B4 File Offset: 0x000037B4
		public List<Phone> SupportedDeviceTypes
		{
			get
			{
				return this.supportedDeviceTypes;
			}
			set
			{
				base.SetValue<List<Phone>>(() => this.SupportedDeviceTypes, ref this.supportedDeviceTypes, value);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000055F4 File Offset: 0x000037F4
		private void FillSupportedDeviceTypes()
		{
			this.SupportedDeviceTypes = new List<Phone>();
			List<string> list = new List<string>
			{
				"RM-927", "RM-937", "RM-938", "RM-939", "RM-940", "RM-974", "RM-975", "RM-976", "RM-977", "RM-978",
				"RM-979", "RM-983", "RM-984", "RM-985", "RM-1010", "RM-1017", "RM-1018", "RM-1019", "RM-1020", "RM-1027",
				"RM-1031", "RM-1032", "RM-1034", "RM-1038", "RM-1039", "RM-1040", "RM-1041", "RM-1045", "RM-1049", "RM-1050",
				"RM-1051", "RM-1052", "RM-1062", "RM-1064", "RM-1066", "RM-1067", "RM-1068", "RM-1069", "RM-1070", "RM-1071",
				"RM-1072", "RM-1073", "RM-1075", "RM-1077", "RM-1078", "RM-1087", "RM-1089", "RM-1090", "RM-1091", "RM-1092",
				"RM-1109", "RM-1113", "RM-1114", "RM-1115"
			};
			foreach (string text in list)
			{
				this.SupportedDeviceTypes.Add(new Phone(SalesNames.NameForProductType(text), text));
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000058F4 File Offset: 0x00003AF4
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("ManualDeviceSelection"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			base.OnStarted();
			this.FoundPackages = null;
			this.CurrentState = ManualDeviceTypeSelectionState.FfuSelection;
			this.StatusInfo = LocalizationManager.GetTranslation("ConnectedDeviceCannotBeRecovered");
			string productsPath = FileSystemInfo.GetLumiaProductsPath("");
			base.Commands.Run((FlowController c) => c.FindAllLumiaPackages(productsPath, CancellationToken.None));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005A0C File Offset: 0x00003C0C
		public void Handle(CompatibleFfuFilesMessage message)
		{
			bool flag = message.Packages.Count == 0;
			if (flag)
			{
				this.CurrentState = ManualDeviceTypeSelectionState.TypeDesignatorSelection;
			}
			else
			{
				this.FoundPackages = message.Packages;
				foreach (PackageFileInfo packageFileInfo in this.FoundPackages)
				{
					packageFileInfo.SalesName = SalesNames.NameForPackageName(packageFileInfo.Name);
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005A9C File Offset: 0x00003C9C
		private void DeviceSelected(object obj)
		{
			bool flag = obj is Phone && string.IsNullOrWhiteSpace(this.appContext.CurrentPhone.HardwareModel);
			if (flag)
			{
				this.appContext.CurrentPhone.HardwareModel = ((Phone)obj).HardwareModel;
			}
			else
			{
				this.SelectedPackage = (PackageFileInfo)obj;
			}
			base.Commands.Run((AppController c) => c.SwitchToState("DownloadEmergencyPackageState"));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005B60 File Offset: 0x00003D60
		private void ChangeViewState()
		{
			bool flag = this.currentState == ManualDeviceTypeSelectionState.FfuSelection;
			if (flag)
			{
				this.CurrentState = ManualDeviceTypeSelectionState.TypeDesignatorSelection;
			}
			else
			{
				this.CurrentState = ManualDeviceTypeSelectionState.DeviceCannotBeRecovered;
			}
		}

		// Token: 0x04000073 RID: 115
		private readonly Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x04000074 RID: 116
		private bool nextButtonEnabled;

		// Token: 0x04000075 RID: 117
		private List<PackageFileInfo> foundPackages;

		// Token: 0x04000076 RID: 118
		private PackageFileInfo selectedPackage;

		// Token: 0x04000077 RID: 119
		private string statusInfo;

		// Token: 0x04000078 RID: 120
		private List<Phone> supportedDeviceTypes;

		// Token: 0x04000079 RID: 121
		private ManualDeviceTypeSelectionState currentState;
	}
}
