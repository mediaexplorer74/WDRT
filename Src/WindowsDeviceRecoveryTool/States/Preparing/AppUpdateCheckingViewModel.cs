using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x02000041 RID: 65
	[Export]
	public class AppUpdateCheckingViewModel : BaseViewModel, ICanHandle<ApplicationUpdateMessage>, ICanHandle
	{
		// Token: 0x0600029F RID: 671 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
		[ImportingConstructor]
		public AppUpdateCheckingViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000F1D8 File Offset: 0x0000D3D8
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000F230 File Offset: 0x0000D430
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000F248 File Offset: 0x0000D448
		public bool IsChecking
		{
			get
			{
				return this.isChecking;
			}
			set
			{
				base.SetValue<bool>(() => this.IsChecking, ref this.isChecking, value);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000F288 File Offset: 0x0000D488
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				base.SetValue<string>(() => this.Version, ref this.version, value);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				base.SetValue<string>(() => this.Description, ref this.description, value);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000F338 File Offset: 0x0000D538
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("ApplicationUpdate"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			this.IsChecking = true;
			base.Commands.Run((AppController c) => c.CheckForAppUpdate(null, CancellationToken.None));
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000F40A File Offset: 0x0000D60A
		public void Handle(ApplicationUpdateMessage message)
		{
			this.Description = this.ReplaceTagByNewLine(message.Update.Description);
			this.Version = message.Update.Version;
			this.IsChecking = false;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000F440 File Offset: 0x0000D640
		private string ReplaceTagByNewLine(string text)
		{
			return (text != null) ? Regex.Replace(text, "<br>", "\n", RegexOptions.IgnoreCase) : string.Empty;
		}

		// Token: 0x04000137 RID: 311
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x04000138 RID: 312
		private string version;

		// Token: 0x04000139 RID: 313
		private string description;

		// Token: 0x0400013A RID: 314
		private bool isChecking;
	}
}
