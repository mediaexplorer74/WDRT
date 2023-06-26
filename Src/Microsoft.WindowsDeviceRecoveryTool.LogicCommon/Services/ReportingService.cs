using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services
{
	// Token: 0x02000011 RID: 17
	[Export(typeof(ReportingService))]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class ReportingService
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x0000508C File Offset: 0x0000328C
		[ImportingConstructor]
		public ReportingService(MsrReportingService msrReportingService)
		{
			this.msrReportingService = msrReportingService;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000050A0 File Offset: 0x000032A0
		public MsrReportingService MsrReportingService
		{
			get
			{
				return this.msrReportingService;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000050B8 File Offset: 0x000032B8
		public void SetDownloadByteInformation(Phone phone, ReportOperationType reportOperationType, long currentDownloadedSize, long packageSize, bool isResumed)
		{
			this.msrReportingService.SetDownloadByteInformation(phone, reportOperationType, currentDownloadedSize, packageSize, isResumed);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000050CE File Offset: 0x000032CE
		public void OperationStarted(Phone phone, ReportOperationType reportOperationType)
		{
			this.msrReportingService.OperationStarted(phone, reportOperationType);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000050DF File Offset: 0x000032DF
		public void OperationSucceded(Phone phone, ReportOperationType reportOperationType)
		{
			this.msrReportingService.OperationSucceded(phone, reportOperationType);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000050F0 File Offset: 0x000032F0
		public void PartialOperationSucceded(Phone phone, ReportOperationType reportOperationType, UriData uriData)
		{
			this.msrReportingService.PartialOperationSucceded(phone, reportOperationType, uriData);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005102 File Offset: 0x00003302
		public void OperationFailed(Phone phone, ReportOperationType reportOperationType, UriData resultUriData, Exception ex)
		{
			this.msrReportingService.OperationFailed(phone, reportOperationType, resultUriData, ex);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005116 File Offset: 0x00003316
		public void SendSessionReports()
		{
			this.msrReportingService.SendSessionReports();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005125 File Offset: 0x00003325
		public void SurveySucceded(SurveyReport survey)
		{
			this.msrReportingService.SurveySucceded(survey);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005135 File Offset: 0x00003335
		public void StartFlowSession()
		{
			this.msrReportingService.StartFlowSession();
		}

		// Token: 0x04000045 RID: 69
		private readonly MsrReportingService msrReportingService;
	}
}
