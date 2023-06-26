using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Workflow
{
	// Token: 0x0200001E RID: 30
	[Export]
	public class RebootHtcViewModel : BaseViewModel, ICanHandle<FlashResultMessage>, ICanHandle, ICanHandle<FirmwareVersionsCompareMessage>
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000936C File Offset: 0x0000756C
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00009384 File Offset: 0x00007584
		public string Header
		{
			get
			{
				return this.header;
			}
			set
			{
				base.SetValue<string>(() => this.Header, ref this.header, value);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000093C4 File Offset: 0x000075C4
		public override void OnStarted()
		{
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			this.ResetText();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000093E0 File Offset: 0x000075E0
		public void Handle(FlashResultMessage message)
		{
			this.packageNotFound = message.Result;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000093F0 File Offset: 0x000075F0
		public void Handle(FirmwareVersionsCompareMessage message)
		{
			SwVersionComparisonResult status = message.Status;
			SwVersionComparisonResult swVersionComparisonResult = status;
			if (swVersionComparisonResult == SwVersionComparisonResult.PackageNotFound)
			{
				this.packageNotFound = true;
				this.ResetText();
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00009420 File Offset: 0x00007620
		private void ResetText()
		{
			this.Header = LocalizationManager.GetTranslation(this.packageNotFound ? "HtcPackageNotFound" : "OperationFailed");
			string translation = LocalizationManager.GetTranslation(this.packageNotFound ? "SoftwarePackage" : "RestartDeviceHeader");
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(translation, ""));
		}

		// Token: 0x040000BC RID: 188
		private bool packageNotFound = true;

		// Token: 0x040000BD RID: 189
		private string header;
	}
}
