using System;
using System.ComponentModel.Composition;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x02000055 RID: 85
	[Export]
	public class ReadingDeviceInfoWithThorViewModel : BaseViewModel
	{
		// Token: 0x0600035A RID: 858 RVA: 0x00012DA1 File Offset: 0x00010FA1
		[ImportingConstructor]
		public ReadingDeviceInfoWithThorViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00012DB4 File Offset: 0x00010FB4
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00012DCC File Offset: 0x00010FCC
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00012E0C File Offset: 0x0001100C
		public override string PreviousStateName
		{
			get
			{
				return "AutomaticManufacturerSelectionState";
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00012E24 File Offset: 0x00011024
		public override void OnStarted()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("ReadingDeviceInfo"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(true, null, null));
			base.Commands.Run((LumiaController c) => c.TryReadMissingInfoWithThor(null, CancellationToken.None));
		}

		// Token: 0x0400017C RID: 380
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;
	}
}
