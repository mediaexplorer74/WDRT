using System;
using System.Diagnostics;

namespace Microsoft.WindowsDeviceRecoveryTool.Common.Tracing
{
	// Token: 0x0200000E RID: 14
	internal class TraceListenerAdapter : TraceListener
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00003030 File Offset: 0x00001230
		public TraceListenerAdapter(ITraceWriter writer)
		{
			bool flag = writer == null;
			if (flag)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003060 File Offset: 0x00001260
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			this.writer.TraceData(eventCache, source, eventType, id, data);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003076 File Offset: 0x00001276
		public override void Write(string message)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003076 File Offset: 0x00001276
		public override void WriteLine(string message)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400000E RID: 14
		private readonly ITraceWriter writer;
	}
}
