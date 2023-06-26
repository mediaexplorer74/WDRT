using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.Common.Tracing
{
	// Token: 0x0200000D RID: 13
	[CompilerGenerated]
	internal sealed class ThreadSafeTracer : IThreadSafeTracer
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002E50 File Offset: 0x00001050
		public ThreadSafeTracer(string name, SourceLevels tracingLevel)
		{
			this.tracer = new TraceSource(name)
			{
				Switch = new SourceSwitch("Main switch")
				{
					Level = tracingLevel
				}
			};
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E8A File Offset: 0x0000108A
		public ThreadSafeTracer(string name)
			: this(name, SourceLevels.All)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E98 File Offset: 0x00001098
		public void TraceData(TraceEventType eventType, params object[] data)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.tracer.TraceData(eventType, 0, data);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002EE8 File Offset: 0x000010E8
		public void DisableTracing()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.tracer.Switch.Level = SourceLevels.Off;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F3C File Offset: 0x0000113C
		public void EnableTracing()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.tracer.Switch.Level = SourceLevels.All;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F90 File Offset: 0x00001190
		public void AddTraceListener(TraceListener traceListener)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.tracer.Listeners.Add(traceListener);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002FE4 File Offset: 0x000011E4
		public void Close()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.tracer.Close();
			}
		}

		// Token: 0x0400000C RID: 12
		private readonly object syncRoot = new object();

		// Token: 0x0400000D RID: 13
		private readonly TraceSource tracer;
	}
}
