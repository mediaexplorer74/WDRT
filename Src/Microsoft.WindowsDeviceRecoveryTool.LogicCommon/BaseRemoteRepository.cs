using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon
{
	// Token: 0x02000003 RID: 3
	public abstract class BaseRemoteRepository
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000024E3 File Offset: 0x000006E3
		protected BaseRemoteRepository()
		{
			this.SpeedCalculator = new SpeedCalculator();
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000030 RID: 48 RVA: 0x000024FC File Offset: 0x000006FC
		// (remove) Token: 0x06000031 RID: 49 RVA: 0x00002534 File Offset: 0x00000734
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ProgressChangedEventArgs> ProgressChanged;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002569 File Offset: 0x00000769
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002571 File Offset: 0x00000771
		private protected SpeedCalculator SpeedCalculator { protected get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000257A File Offset: 0x0000077A
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002582 File Offset: 0x00000782
		protected long TotalFilesSize { get; set; }

		// Token: 0x06000036 RID: 54 RVA: 0x0000258B File Offset: 0x0000078B
		public void SetProxy(IWebProxy settings)
		{
			this.proxySettings = settings;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002598 File Offset: 0x00000798
		protected void RaiseProgressChangedEvent(int percentage, string message = null)
		{
			bool flag = !string.IsNullOrEmpty(message);
			if (flag)
			{
				this.lastProgressMessage = message;
			}
			this.lastProgressPercentage = percentage;
			this.RaiseProgressChangedEvent();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000025CC File Offset: 0x000007CC
		protected void RaiseProgressChangedEvent()
		{
			this.RaiseProgressChangedEvent(new ProgressChangedEventArgs(this.lastProgressPercentage, this.lastProgressMessage, this.SpeedCalculator.TotalDownloadedSize, this.TotalFilesSize, this.SpeedCalculator.BytesPerSecond, this.SpeedCalculator.RemaingSeconds));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000261C File Offset: 0x0000081C
		protected void RaiseProgressChangedEvent(ProgressChangedEventArgs progressChangedEventArgs)
		{
			Action<ProgressChangedEventArgs> progressChanged = this.ProgressChanged;
			bool flag = progressChanged != null;
			if (flag)
			{
				progressChanged(progressChangedEventArgs);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002644 File Offset: 0x00000844
		protected IWebProxy Proxy()
		{
			return this.proxySettings ?? WebRequest.GetSystemWebProxy();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002665 File Offset: 0x00000865
		public virtual Task<PackageFileInfo> CheckLatestPackage(QueryParameters queryParameters, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002665 File Offset: 0x00000865
		public virtual List<string> DownloadLatestPackage(QueryParameters queryParameters, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002665 File Offset: 0x00000865
		public virtual List<string> DownloadLatestPackage(DownloadParameters downloadParameters, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000008 RID: 8
		protected const long ErrorHandleDiskFull = 39L;

		// Token: 0x04000009 RID: 9
		protected const long ErrorDiskFull = 112L;

		// Token: 0x0400000A RID: 10
		private int lastProgressPercentage;

		// Token: 0x0400000B RID: 11
		private string lastProgressMessage;

		// Token: 0x0400000C RID: 12
		private IWebProxy proxySettings;
	}
}
