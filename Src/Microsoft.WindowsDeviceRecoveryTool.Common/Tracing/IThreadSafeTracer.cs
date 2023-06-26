using System;
using System.Diagnostics;

namespace Microsoft.WindowsDeviceRecoveryTool.Common.Tracing
{
	// Token: 0x0200000B RID: 11
	public interface IThreadSafeTracer
	{
		// Token: 0x06000044 RID: 68
		void TraceData(TraceEventType eventType, params object[] data);

		// Token: 0x06000045 RID: 69
		void DisableTracing();

		// Token: 0x06000046 RID: 70
		void EnableTracing();

		// Token: 0x06000047 RID: 71
		void AddTraceListener(TraceListener traceListener);

		// Token: 0x06000048 RID: 72
		void Close();
	}
}
