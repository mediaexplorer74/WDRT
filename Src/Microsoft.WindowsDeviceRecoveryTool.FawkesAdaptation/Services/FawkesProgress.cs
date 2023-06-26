using System;
using ClickerUtilityLibrary;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.FawkesAdaptation.Services
{
	// Token: 0x02000009 RID: 9
	internal class FawkesProgress : Progress<FawkesProgressData>
	{
		// Token: 0x06000040 RID: 64 RVA: 0x0000323E File Offset: 0x0000143E
		public FawkesProgress(Action<FawkesProgressData> progressHandler)
			: base(progressHandler)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003247 File Offset: 0x00001447
		internal void SetupUpdaterEvents(ClickerFwUpdater updaterInstance)
		{
			if (this.updater != null)
			{
				throw new InvalidOperationException("This instance was setup already");
			}
			this.updater = updaterInstance;
			this.updater.UpdaterEvent += this.UpdaterOnUpdaterEvent;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000327A File Offset: 0x0000147A
		internal void CleanUpdaterEvents()
		{
			if (this.updater != null)
			{
				this.updater.UpdaterEvent -= this.UpdaterOnUpdaterEvent;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000329C File Offset: 0x0000149C
		private void UpdaterOnUpdaterEvent(object sender, FwUpdaterEventArgs fwUpdaterEventArgs)
		{
			Tracer<FawkesProgress>.WriteInformation("Fawkes firmware update event received: {0}, params: {1}", new object[] { fwUpdaterEventArgs.Type, fwUpdaterEventArgs.Parameters });
			switch (fwUpdaterEventArgs.Type)
			{
			case FwUpdaterEventArgs.EventType.UpdateCompleted:
				this.ProcessUpdateFinishedEvent(fwUpdaterEventArgs.Parameters);
				return;
			case FwUpdaterEventArgs.EventType.UpdateProgress:
				this.ProcessUpdateProgressEvent(fwUpdaterEventArgs.Parameters);
				return;
			case FwUpdaterEventArgs.EventType.DeviceDisconnected:
				this.ProcessDeviceDisconnectedEvent(fwUpdaterEventArgs.Parameters);
				return;
			case FwUpdaterEventArgs.EventType.ConnectedToApplication:
				this.ProcessDeviceConnectedToApplicationEvent(fwUpdaterEventArgs.Parameters);
				return;
			case FwUpdaterEventArgs.EventType.ConnectedToBootLoader:
				this.ProcessDeviceConnectedToBootLoaderEvent(fwUpdaterEventArgs.Parameters);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003332 File Offset: 0x00001532
		private void ProcessDeviceDisconnectedEvent(object parameters)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003332 File Offset: 0x00001532
		private void ProcessDeviceConnectedToApplicationEvent(object parameters)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003332 File Offset: 0x00001532
		private void ProcessDeviceConnectedToBootLoaderEvent(object parameters)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003334 File Offset: 0x00001534
		private void ProcessUpdateFinishedEvent(object parameters)
		{
			this.OnReport(new FawkesProgressData(new double?((double)100), "WaitUntilPhoneTurnsOn"));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003350 File Offset: 0x00001550
		private void ProcessUpdateProgressEvent(object parameters)
		{
			double num = -1.0;
			if (parameters is double)
			{
				num = (double)parameters * 100.0;
			}
			this.OnReport(new FawkesProgressData(new double?(num), "FlashingMessageInstallingSoftware"));
		}

		// Token: 0x0400000F RID: 15
		private ClickerFwUpdater updater;
	}
}
