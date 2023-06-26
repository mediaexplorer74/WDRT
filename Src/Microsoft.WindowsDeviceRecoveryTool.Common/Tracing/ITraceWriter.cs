using System;
using System.Diagnostics;

namespace Microsoft.WindowsDeviceRecoveryTool.Common.Tracing
{
	// Token: 0x0200000C RID: 12
	public interface ITraceWriter
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000049 RID: 73
		string LogFilePath { get; }

		// Token: 0x0600004A RID: 74
		void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data);

		// Token: 0x0600004B RID: 75
		void ChangeLogFolder(string newPath);

		// Token: 0x0600004C RID: 76
		void Close();
	}
}
