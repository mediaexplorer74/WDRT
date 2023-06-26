using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x02000044 RID: 68
	[Export]
	public class AbsoluteConfirmationViewModel : BaseViewModel, ICanHandle<DeviceConnectionStatusReadMessage>, ICanHandle
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0000F83C File Offset: 0x0000DA3C
		[ImportingConstructor]
		public AbsoluteConfirmationViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.appContext = appContext;
			this.ContinueCommand = new DelegateCommand<object>(new Action<object>(this.ContinueClicked));
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000F865 File Offset: 0x0000DA65
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000F86D File Offset: 0x0000DA6D
		public ICommand ContinueCommand { get; private set; }

		// Token: 0x060002BD RID: 701 RVA: 0x0000F878 File Offset: 0x0000DA78
		private void ContinueClicked(object obj)
		{
			base.Commands.Run((FlowController c) => c.CheckIfDeviceStillConnected(this.appContext.CurrentPhone, CancellationToken.None));
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000F920 File Offset: 0x0000DB20
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("WarningHeader"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000F97C File Offset: 0x0000DB7C
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
				base.Commands.Run((AppController c) => c.SwitchToState("FlashingState"));
			}
		}

		// Token: 0x0400013F RID: 319
		private readonly Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;
	}
}
