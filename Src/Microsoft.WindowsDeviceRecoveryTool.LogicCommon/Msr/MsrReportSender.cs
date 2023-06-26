using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Interfaces;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Msr
{
	// Token: 0x02000029 RID: 41
	public sealed class MsrReportSender
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060002B0 RID: 688 RVA: 0x00009754 File Offset: 0x00007954
		// (remove) Token: 0x060002B1 RID: 689 RVA: 0x0000978C File Offset: 0x0000798C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action SessionReportsSendingCompleted;

		// Token: 0x060002B2 RID: 690 RVA: 0x000097C4 File Offset: 0x000079C4
		public MsrReportSender(MsrReporting msrReporting)
		{
			bool flag = msrReporting == null;
			if (flag)
			{
				throw new ArgumentNullException("msrReporting");
			}
			this.ioHelper = new IOHelper();
			this.workerHelper = new WorkerHelper(new DoWorkEventHandler(this.SendOldReports));
			this.msrReporting = msrReporting;
			this.msrReporting.SendCompleted += this.MsrReportSendCompleted;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00009830 File Offset: 0x00007A30
		public void SendReport(ReportData reportData, bool isInternal)
		{
			Tracer<MsrReportSender>.WriteInformation("Sending report {0} | {1}", new object[] { reportData.Description, reportData.UriData });
			try
			{
				bool flag = false;
				MsrReport msrReport = ReportBuilder.Build(reportData, isInternal);
				bool flag2 = !flag;
				if (flag2)
				{
					this.msrReporting.SendAsync(msrReport);
				}
			}
			catch (Exception ex)
			{
				Tracer<MsrReportSender>.WriteError(ex, "Sending report failed.", new object[0]);
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000098B4 File Offset: 0x00007AB4
		public void SendReport(SurveyReport report, bool isInternal)
		{
			Tracer<MsrReportSender>.WriteInformation("Sending report {0} | {1}", new object[] { report.ManufacturerHardwareModel, report.ManufacturerHardwareVariant });
			try
			{
				bool flag = false;
				bool flag2 = !flag;
				if (flag2)
				{
					this.msrReporting.SendAsync(report);
				}
			}
			catch (Exception ex)
			{
				Tracer<MsrReportSender>.WriteError(ex, "Sending report failed.", new object[0]);
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000992C File Offset: 0x00007B2C
		public void SaveLocalReport(ReportData reportData)
		{
			Tracer<MsrReportSender>.WriteInformation("Saving report locally: {0}", new object[] { reportData.PhoneInfo });
			try
			{
				MsrReport msrReport = ReportBuilder.Build(reportData, ApplicationInfo.IsInternal());
				bool flag = string.IsNullOrWhiteSpace(reportData.LocalPath);
				if (flag)
				{
					reportData.LocalPath = this.BuildReportFilename("Not sent\\", reportData.PhoneInfo.Imei, reportData.Description, ReportFileType.Binary);
				}
				this.ioHelper.SerializeReport(reportData.LocalPath, msrReport);
			}
			catch (Exception ex)
			{
				Tracer<MsrReportSender>.WriteError(ex, "Saving local report failed.", new object[0]);
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000099D4 File Offset: 0x00007BD4
		public void SendOldReports()
		{
			bool flag = !this.workerHelper.IsBusy;
			if (flag)
			{
				this.workerHelper.RunWorkerAsync();
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00009A04 File Offset: 0x00007C04
		public void RemoveLocalReport(string localPath)
		{
			bool flag = !string.IsNullOrWhiteSpace(localPath);
			if (flag)
			{
				this.ioHelper.DeleteFile(localPath);
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00009A30 File Offset: 0x00007C30
		public void SendSessionReports(List<ReportData> reportDataList, bool isInternal)
		{
			this.sendingReportQueue = new List<IReport>();
			List<IReport> list = this.sendingReportQueue;
			lock (list)
			{
				try
				{
					foreach (ReportData reportData in reportDataList)
					{
						reportData.EndDataCollecting();
						bool flag2 = false;
						MsrReport msrReport = ReportBuilder.Build(reportData, isInternal);
						bool flag3 = !flag2;
						if (flag3)
						{
							this.sendingReportQueue.Add(msrReport);
							this.msrReporting.SendAsync(msrReport);
						}
					}
				}
				catch (Exception ex)
				{
					Tracer<MsrReportSender>.WriteError(ex, "Sending report failed.", new object[0]);
				}
			}
			this.TrySendSessionReportsCompletedEvent();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00009B20 File Offset: 0x00007D20
		private void SaveSentReport(ReportSendCompletedEventArgs e)
		{
			string text = this.BuildReportFilename("Sent\\", e.Parameters.Ext1.Replace(":", string.Empty).Replace(" ", string.Empty), e.Parameters.Ext2, ReportFileType.Xml);
			this.ioHelper.SaveReport(text, e.Parameters);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00009B84 File Offset: 0x00007D84
		private void SaveNotSentReport(ReportSendCompletedEventArgs e)
		{
			IReport report = e.Report;
			bool flag = string.IsNullOrWhiteSpace(report.LocalPath);
			if (flag)
			{
				report.LocalPath = this.BuildReportFilename("Not sent\\", report.Imei, report.ActionDescription, ReportFileType.Binary);
			}
			this.ioHelper.SerializeReport(report.LocalPath, report);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00009BDC File Offset: 0x00007DDC
		private void SendOldReports(object sender, DoWorkEventArgs e)
		{
			string text = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppDataPath(SpecialFolder.Reports) + "Not sent\\";
			this.ioHelper.CreateDir(text);
			string[] msrFiles = this.ioHelper.GetMsrFiles(text);
			string[] array = msrFiles;
			int i = 0;
			while (i < array.Length)
			{
				string text2 = array[i];
				IReport report;
				try
				{
					report = this.ioHelper.DeserializeReport(text2);
				}
				catch (Exception)
				{
					try
					{
						Report report2 = this.ioHelper.DeserializeFireReport(text2);
						report = report2.ConvertToMsrReport();
					}
					catch (Exception ex)
					{
						Tracer<MsrReportSender>.WriteError(ex, "Old report deserialization failed", new object[0]);
						this.ioHelper.DeleteFile(text2);
						goto IL_A2;
					}
				}
				goto IL_93;
				IL_A2:
				i++;
				continue;
				IL_93:
				this.msrReporting.SendAsync(report);
				goto IL_A2;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00009CB4 File Offset: 0x00007EB4
		[Conditional("DEBUG")]
		private void SaveInternalReport(IReport report, ref bool handled)
		{
			Tracer<MsrReportSender>.LogEntry("SaveInternalReport");
			try
			{
				string text = this.BuildReportFilename("Internal\\", report.Imei, report.ActionDescription, ReportFileType.Csv);
				this.ioHelper.SaveReportAsCsv(text, report);
				this.RemoveLocalReport(report.LocalPath);
				handled = true;
			}
			catch (IOException)
			{
			}
			Tracer<MsrReportSender>.LogExit("SaveInternalReport");
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00009D28 File Offset: 0x00007F28
		private void MsrReportSendCompleted(ReportSendCompletedEventArgs e)
		{
			bool flag = e.Error != null;
			if (flag)
			{
				try
				{
					this.SaveNotSentReport(e);
				}
				catch (Exception ex)
				{
					Tracer<MsrReportSender>.WriteInformation("SaveNotSentReport failed: {0}", new object[] { ex.Message });
				}
			}
			else
			{
				try
				{
					Tracer<MsrReportSender>.WriteInformation("Msr report was sent successfully.");
					this.SaveSentReport(e);
					this.RemoveLocalReport(e.Report.LocalPath);
				}
				catch (Exception ex2)
				{
					Tracer<MsrReportSender>.WriteInformation("SaveSentReport failed: {0}", new object[] { ex2.Message });
				}
			}
			bool flag2 = this.sendingReportQueue != null && this.sendingReportQueue.Contains(e.Report);
			if (flag2)
			{
				List<IReport> list = this.sendingReportQueue;
				lock (list)
				{
					this.sendingReportQueue.Remove(e.Report);
				}
				this.TrySendSessionReportsCompletedEvent();
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00009E48 File Offset: 0x00008048
		private void TrySendSessionReportsCompletedEvent()
		{
			bool flag = this.sendingReportQueue == null || this.sendingReportQueue.Any<IReport>();
			if (!flag)
			{
				Action sessionReportsSendingCompleted = this.SessionReportsSendingCompleted;
				bool flag2 = sessionReportsSendingCompleted != null;
				if (flag2)
				{
					sessionReportsSendingCompleted();
				}
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00009E8C File Offset: 0x0000808C
		private string BuildReportFilename(string subDirectory, string imei, string reportDescription, ReportFileType reportFileType)
		{
			string text = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppDataPath(SpecialFolder.Reports) + subDirectory;
			this.ioHelper.CreateDir(text);
			DateTime utcNow = DateTime.UtcNow;
			string reportFileExtension = this.ioHelper.GetReportFileExtension(reportFileType);
			string text2 = string.Format("msr_{0:yyyyMMdd}_{0:HHmmss_ff}_{1}_{2}.{3}", new object[] { utcNow, imei, reportDescription, reportFileExtension });
			return text + text2;
		}

		// Token: 0x04000124 RID: 292
		private MsrReporting msrReporting;

		// Token: 0x04000125 RID: 293
		private WorkerHelper workerHelper;

		// Token: 0x04000126 RID: 294
		private IOHelper ioHelper;

		// Token: 0x04000127 RID: 295
		private List<IReport> sendingReportQueue;
	}
}
