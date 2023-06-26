using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Interfaces;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Msr
{
	// Token: 0x02000028 RID: 40
	public sealed class MsrReporting
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060002A6 RID: 678 RVA: 0x00009484 File Offset: 0x00007684
		// (remove) Token: 0x060002A7 RID: 679 RVA: 0x000094BC File Offset: 0x000076BC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ReportSendCompletedEventArgs> SendCompleted;

		// Token: 0x060002A8 RID: 680 RVA: 0x000094F1 File Offset: 0x000076F1
		public MsrReporting(MsrReportingService msrReportingService)
		{
			this.environmentInfo = new EnvironmentInfo(new ApplicationInfo());
			this.msrReportingService = msrReportingService;
			this.actionQueue = new Queue<ReportStatusAsyncState>();
			this.processingThreadStarted = false;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00009524 File Offset: 0x00007724
		public void SendAsync(IReport report)
		{
			ReportStatusAsyncState reportStatusAsyncState = new ReportStatusAsyncState(ReportingOperation.Send, report, null);
			this.AddToReportQueue(reportStatusAsyncState);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00009544 File Offset: 0x00007744
		private void AddToReportQueue(ReportStatusAsyncState asyncState)
		{
			object syncRoot = ((ICollection)this.actionQueue).SyncRoot;
			lock (syncRoot)
			{
				this.actionQueue.Enqueue(asyncState);
				bool flag2 = !this.processingThreadStarted;
				if (flag2)
				{
					this.processingThreadStarted = true;
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.ProcessReportQueueWork));
				}
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000095C0 File Offset: 0x000077C0
		private void ProcessReportQueueWork(object obj)
		{
			do
			{
				object syncRoot = ((ICollection)this.actionQueue).SyncRoot;
				ReportStatusAsyncState reportStatusAsyncState;
				lock (syncRoot)
				{
					bool flag2 = this.actionQueue.Count > 0;
					if (!flag2)
					{
						this.processingThreadStarted = false;
						break;
					}
					reportStatusAsyncState = this.actionQueue.Dequeue();
				}
				bool flag3 = reportStatusAsyncState.ReportingOperation == ReportingOperation.Send;
				if (flag3)
				{
					this.HandleSendRequest(reportStatusAsyncState);
				}
			}
			while (this.processingThreadStarted);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00009658 File Offset: 0x00007858
		private void HandleSendRequest(ReportStatusAsyncState statusAsyncState)
		{
			Exception ex = null;
			ReportUpdateStatus4Parameters reportUpdateStatus4Parameters = null;
			try
			{
				reportUpdateStatus4Parameters = statusAsyncState.Report.CreateReportStatusParameters();
				this.msrReportingService.SendReportAsync(statusAsyncState.Report).Wait();
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				this.OnSendCompleted(new ReportSendCompletedEventArgs(reportUpdateStatus4Parameters, statusAsyncState.Report, ex, null));
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000096D0 File Offset: 0x000078D0
		private string FormatString(string source, int maxLength)
		{
			bool flag = string.IsNullOrEmpty(source);
			string text;
			if (flag)
			{
				text = "Unknown";
			}
			else
			{
				text = this.Truncate(source, maxLength);
			}
			return text;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00009700 File Offset: 0x00007900
		private string Truncate(string source, int length)
		{
			bool flag = source.Length > length;
			if (flag)
			{
				source = source.Substring(0, length);
			}
			return source;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000972C File Offset: 0x0000792C
		private void OnSendCompleted(ReportSendCompletedEventArgs e)
		{
			Action<ReportSendCompletedEventArgs> sendCompleted = this.SendCompleted;
			bool flag = sendCompleted != null;
			if (flag)
			{
				sendCompleted(e);
			}
		}

		// Token: 0x0400011F RID: 287
		private readonly Queue<ReportStatusAsyncState> actionQueue;

		// Token: 0x04000120 RID: 288
		private MsrReportingService msrReportingService;

		// Token: 0x04000121 RID: 289
		private bool processingThreadStarted;

		// Token: 0x04000122 RID: 290
		private readonly IEnvironmentInfo environmentInfo;
	}
}
