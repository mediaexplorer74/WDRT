using System;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Interfaces;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting
{
	// Token: 0x02000018 RID: 24
	public sealed class ReportStatusAsyncState
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x000076F9 File Offset: 0x000058F9
		public ReportStatusAsyncState(ReportingOperation operation, IReport report = null, object userState = null)
		{
			this.ReportingOperation = operation;
			this.Report = report;
			this.UserState = userState;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000771B File Offset: 0x0000591B
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00007723 File Offset: 0x00005923
		public ReportingOperation ReportingOperation { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000772C File Offset: 0x0000592C
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00007734 File Offset: 0x00005934
		public IReport Report { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000773D File Offset: 0x0000593D
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00007745 File Offset: 0x00005945
		public object UserState { get; private set; }
	}
}
