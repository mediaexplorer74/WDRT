using System;
using System.ComponentModel;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Interfaces;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting
{
	// Token: 0x0200001B RID: 27
	public sealed class ReportSendCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00008134 File Offset: 0x00006334
		public ReportSendCompletedEventArgs(IReport report, Exception error, object userState)
			: base(error, false, userState)
		{
			bool flag = report == null;
			if (flag)
			{
				throw new ArgumentNullException("report");
			}
			this.Report = report;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008168 File Offset: 0x00006368
		public ReportSendCompletedEventArgs(ReportUpdateStatus4Parameters parameters, IReport report, Exception error, object userState = null)
			: base(error, false, userState)
		{
			bool flag = report == null;
			if (flag)
			{
				throw new ArgumentNullException("report");
			}
			this.Parameters = parameters;
			this.Report = report;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000081A5 File Offset: 0x000063A5
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000081AD File Offset: 0x000063AD
		public IReport Report { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000081B6 File Offset: 0x000063B6
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000081BE File Offset: 0x000063BE
		public ReportUpdateStatus4Parameters Parameters { get; private set; }
	}
}
