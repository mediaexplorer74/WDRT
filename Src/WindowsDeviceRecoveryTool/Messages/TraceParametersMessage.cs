using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000B3 RID: 179
	public class TraceParametersMessage
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x0001B832 File Offset: 0x00019A32
		public TraceParametersMessage(string logZipFilePath = null, bool collectingLogsCompleted = false)
		{
			this.LogZipFilePath = logZipFilePath;
			this.CollectingLogsCompleted = collectingLogsCompleted;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001B84C File Offset: 0x00019A4C
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0001B854 File Offset: 0x00019A54
		public string LogZipFilePath { get; private set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001B85D File Offset: 0x00019A5D
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0001B865 File Offset: 0x00019A65
		public bool CollectingLogsCompleted { get; private set; }
	}
}
