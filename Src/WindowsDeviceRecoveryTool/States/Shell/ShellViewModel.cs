using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Messages;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Shell
{
	// Token: 0x0200000A RID: 10
	[Export]
	public class ShellViewModel : BaseViewModel, ICanHandle<SwitchStateMessage>, ICanHandle, ICanHandle<NotificationMessage>, ICanHandle<IsBusyMessage>, ICanHandle<HeaderMessage>, ICanHandle<IsBackButtonMessage>
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00004315 File Offset: 0x00002515
		[ImportingConstructor]
		public ShellViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.appContext = appContext;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004328 File Offset: 0x00002528
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00004340 File Offset: 0x00002540
		public bool ShowDetailedInfo
		{
			get
			{
				return this.showDetailedInfo;
			}
			set
			{
				base.SetValue<bool>(() => this.ShowDetailedInfo, ref this.showDetailedInfo, value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004380 File Offset: 0x00002580
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00004398 File Offset: 0x00002598
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000043D8 File Offset: 0x000025D8
		public string Title
		{
			get
			{
				return string.Format("{0} {1}", AppInfo.AppTitle(), AppInfo.AppVersion());
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00004400 File Offset: 0x00002600
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00004418 File Offset: 0x00002618
		public string HeaderText
		{
			get
			{
				return this.headerText;
			}
			set
			{
				base.SetValue<string>(() => this.HeaderText, ref this.headerText, value);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00004458 File Offset: 0x00002658
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00004470 File Offset: 0x00002670
		public string SubheaderText
		{
			get
			{
				return this.subheaderText;
			}
			set
			{
				base.SetValue<string>(() => this.SubheaderText, ref this.subheaderText, value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000044B0 File Offset: 0x000026B0
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000044C8 File Offset: 0x000026C8
		public bool IsBackButton
		{
			get
			{
				return this.isBackButton;
			}
			set
			{
				base.SetValue<bool>(() => this.IsBackButton, ref this.isBackButton, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004508 File Offset: 0x00002708
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00004520 File Offset: 0x00002720
		public bool IsAppBusy
		{
			get
			{
				return this.isAppBusy;
			}
			set
			{
				base.SetValue<bool>(() => this.IsAppBusy, ref this.isAppBusy, value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004560 File Offset: 0x00002760
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00004578 File Offset: 0x00002778
		public bool? IsNotificationVisible
		{
			get
			{
				return this.isNotificationVisible;
			}
			set
			{
				base.SetValue<bool?>(() => this.IsNotificationVisible, ref this.isNotificationVisible, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000045B8 File Offset: 0x000027B8
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000045D0 File Offset: 0x000027D0
		public string NotificationHeader
		{
			get
			{
				return this.notificationHeader;
			}
			set
			{
				base.SetValue<string>(() => this.NotificationHeader, ref this.notificationHeader, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004610 File Offset: 0x00002810
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00004628 File Offset: 0x00002828
		public string NotificationText
		{
			get
			{
				return this.notificationText;
			}
			set
			{
				base.SetValue<string>(() => this.NotificationText, ref this.notificationText, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004668 File Offset: 0x00002868
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00004680 File Offset: 0x00002880
		public string BusyMessage
		{
			get
			{
				return this.busyMessage;
			}
			set
			{
				base.SetValue<string>(() => this.BusyMessage, ref this.busyMessage, value);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000046C0 File Offset: 0x000028C0
		public void Handle(SwitchStateMessage message)
		{
			bool flag = string.IsNullOrEmpty(message.State);
			if (flag)
			{
				base.Commands.Run((AppController c) => c.EndCurrentState());
			}
			else
			{
				base.Commands.Run((AppController c) => c.SwitchToState(message.State));
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000047C4 File Offset: 0x000029C4
		public void Handle(IsBusyMessage message)
		{
			this.IsAppBusy = message.IsBusy;
			this.BusyMessage = message.Message;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000047E4 File Offset: 0x000029E4
		public void Handle(NotificationMessage message)
		{
			this.IsNotificationVisible = new bool?(message.ShowNotification);
			bool? flag = this.IsNotificationVisible;
			bool flag2 = true;
			bool flag3 = !((flag.GetValueOrDefault() == flag2) & (flag != null));
			if (!flag3)
			{
				this.NotificationHeader = message.Header;
				this.NotificationText = message.Text;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004843 File Offset: 0x00002A43
		public void Handle(HeaderMessage message)
		{
			this.HeaderText = message.Header;
			this.SubheaderText = message.Subheader;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004860 File Offset: 0x00002A60
		public void Handle(IsBackButtonMessage message)
		{
			this.IsBackButton = message.IsBackButton;
		}

		// Token: 0x04000054 RID: 84
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x04000055 RID: 85
		private string busyMessage;

		// Token: 0x04000056 RID: 86
		private string notificationHeader;

		// Token: 0x04000057 RID: 87
		private string notificationText;

		// Token: 0x04000058 RID: 88
		private string headerText;

		// Token: 0x04000059 RID: 89
		private string subheaderText;

		// Token: 0x0400005A RID: 90
		private bool isAppBusy;

		// Token: 0x0400005B RID: 91
		private bool? isNotificationVisible;

		// Token: 0x0400005C RID: 92
		private bool showDetailedInfo;

		// Token: 0x0400005D RID: 93
		private bool isBackButton;
	}
}
