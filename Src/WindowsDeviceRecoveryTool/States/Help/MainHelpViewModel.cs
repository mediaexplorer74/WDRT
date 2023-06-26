using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Help
{
	// Token: 0x0200006E RID: 110
	[Export]
	public class MainHelpViewModel : BaseViewModel, ICanHandle<HelpScreenChangedMessage>, ICanHandle, ICanHandle<SupportedManufacturersMessage>
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0001492D File Offset: 0x00012B2D
		[ImportingConstructor]
		public MainHelpViewModel()
		{
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00014938 File Offset: 0x00012B38
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00014950 File Offset: 0x00012B50
		public string HTCBootloaderModeText
		{
			get
			{
				return this.htcBootloaderModeText;
			}
			set
			{
				base.SetValue<string>(() => this.HTCBootloaderModeText, ref this.htcBootloaderModeText, value);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00014990 File Offset: 0x00012B90
		// (set) Token: 0x060003CD RID: 973 RVA: 0x000149A8 File Offset: 0x00012BA8
		public HelpTabs? SelectedTab
		{
			get
			{
				return this.selectedTab;
			}
			set
			{
				HelpTabs? helpTabs = this.selectedTab;
				HelpTabs? helpTabs2 = value;
				bool flag = (helpTabs.GetValueOrDefault() == helpTabs2.GetValueOrDefault()) & (helpTabs != null == (helpTabs2 != null));
				if (!flag)
				{
					base.SetValue<HelpTabs?>(() => this.SelectedTab, ref this.selectedTab, value);
					HelpTabs? helpTabs3 = value;
					if (helpTabs3 != null)
					{
						switch (helpTabs3.GetValueOrDefault())
						{
						case HelpTabs.LumiaChoose:
							base.Commands.Run((AppController c) => c.SwitchHelpState("LumiaChooseHelpState"));
							break;
						case HelpTabs.LumiaEmergency:
							base.Commands.Run((AppController c) => c.SwitchHelpState("LumiaEmergencyHelpState"));
							break;
						case HelpTabs.LumiaFlashing:
							base.Commands.Run((AppController c) => c.SwitchHelpState("LumiaFlashingHelpState"));
							break;
						case HelpTabs.LumiaNormal:
							base.Commands.Run((AppController c) => c.SwitchHelpState("LumiaNormalHelpState"));
							break;
						case HelpTabs.HtcChoose:
							base.Commands.Run((AppController c) => c.SwitchHelpState("HtcChooseHelpState"));
							break;
						case HelpTabs.HtcBootloader:
							base.Commands.Run((AppController c) => c.SwitchHelpState("HtcBootloaderHelpState"));
							break;
						case HelpTabs.HtcNormal:
							base.Commands.Run((AppController c) => c.SwitchHelpState("HtcNormalHelpState"));
							break;
						}
					}
				}
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00014D4C File Offset: 0x00012F4C
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00014D64 File Offset: 0x00012F64
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

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00014DA4 File Offset: 0x00012FA4
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00014DBC File Offset: 0x00012FBC
		public bool HtcPluginOn
		{
			get
			{
				return this.htcPluginOn;
			}
			set
			{
				base.SetValue<bool>(() => this.HtcPluginOn, ref this.htcPluginOn, value);
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00014DFC File Offset: 0x00012FFC
		public void Handle(HelpScreenChangedMessage message)
		{
			this.SelectedTab = new HelpTabs?(message.SelectedTab);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00014E14 File Offset: 0x00013014
		public void Handle(SupportedManufacturersMessage message)
		{
			bool flag = message.Manufacturers != null;
			if (flag)
			{
				this.HtcPluginOn = message.Manufacturers.Any((ManufacturerInfo manufacturer) => manufacturer.Type == PhoneTypes.Htc);
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00014E64 File Offset: 0x00013064
		public override void OnStarted()
		{
			base.OnStarted();
			this.SelectedTab = new HelpTabs?(HelpTabs.LumiaChoose);
			this.Message = null;
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Help"), LocalizationManager.GetTranslation("ManufacturerHeader")));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			this.HTCBootloaderModeText = string.Format(LocalizationManager.GetTranslation("HtcBootloaderMode"), "boot-loader");
			base.Commands.Run((FlowController c) => c.GetSupportedManufacturers());
		}

		// Token: 0x040001B8 RID: 440
		private string message;

		// Token: 0x040001B9 RID: 441
		private HelpTabs? selectedTab;

		// Token: 0x040001BA RID: 442
		private bool htcPluginOn;

		// Token: 0x040001BB RID: 443
		private string htcBootloaderModeText;
	}
}
