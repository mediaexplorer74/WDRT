using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x0200002E RID: 46
	[Export]
	public class MainSettingsViewModel : BaseViewModel, ICanHandle<SettingsPreviousStateMessage>, ICanHandle
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000C05C File Offset: 0x0000A25C
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000C074 File Offset: 0x0000A274
		public SettingsPage? SelectedPage
		{
			get
			{
				return this.selectedPage;
			}
			set
			{
				SettingsPage? settingsPage = this.selectedPage;
				SettingsPage? settingsPage2 = value;
				bool flag = (settingsPage.GetValueOrDefault() == settingsPage2.GetValueOrDefault()) & (settingsPage != null == (settingsPage2 != null));
				if (!flag)
				{
					base.SetValue<SettingsPage?>(() => this.SelectedPage, ref this.selectedPage, value);
					SettingsPage? settingsPage3 = value;
					if (settingsPage3 != null)
					{
						string nextState;
						switch (settingsPage3.GetValueOrDefault())
						{
						case SettingsPage.Network:
							nextState = "NetworkState";
							break;
						case SettingsPage.Preferences:
							nextState = "PreferencesState";
							break;
						case SettingsPage.Troubleshooting:
							nextState = "TraceState";
							break;
						case SettingsPage.Packages:
							nextState = "PackagesState";
							break;
						case SettingsPage.ApplicationData:
							nextState = "ApplicationDataState";
							break;
						default:
							goto IL_F6;
						}
						base.Commands.Run((AppController c) => c.SwitchSettingsState(nextState));
					}
					IL_F6:;
				}
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		public override void OnStarted()
		{
			this.SelectedPage = new SettingsPage?(this.previousPage ?? SettingsPage.Preferences);
			this.previousPage = null;
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			base.OnStarted();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000C25C File Offset: 0x0000A45C
		public override void OnStopped()
		{
			Settings.Default.Save();
			base.Commands.Run((SettingsController c) => c.SetApplicationSettings());
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		public void Handle(SettingsPreviousStateMessage message)
		{
			bool flag = !string.IsNullOrEmpty(message.PreviousState);
			if (flag)
			{
				string previousState = message.PreviousState;
				string text = previousState;
				if (!(text == "TraceState"))
				{
					if (text == "PackagesState")
					{
						this.previousPage = new SettingsPage?(SettingsPage.Packages);
					}
				}
				else
				{
					this.previousPage = new SettingsPage?(SettingsPage.Troubleshooting);
				}
			}
		}

		// Token: 0x040000F6 RID: 246
		private SettingsPage? selectedPage;

		// Token: 0x040000F7 RID: 247
		private SettingsPage? previousPage;
	}
}
