using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000512 RID: 1298
	internal class ThreadHelper
	{
		// Token: 0x06003D34 RID: 15668 RVA: 0x000E7911 File Offset: 0x000E5B11
		internal ThreadHelper(Delegate start)
		{
			this._start = start;
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x000E7920 File Offset: 0x000E5B20
		internal void SetExecutionContextHelper(ExecutionContext ec)
		{
			this._executionContext = ec;
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000E792C File Offset: 0x000E5B2C
		[SecurityCritical]
		private static void ThreadStart_Context(object state)
		{
			ThreadHelper threadHelper = (ThreadHelper)state;
			if (threadHelper._start is ThreadStart)
			{
				((ThreadStart)threadHelper._start)();
				return;
			}
			((ParameterizedThreadStart)threadHelper._start)(threadHelper._startArg);
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000E7974 File Offset: 0x000E5B74
		[SecurityCritical]
		internal void ThreadStart(object obj)
		{
			this._startArg = obj;
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ParameterizedThreadStart)this._start)(obj);
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x000E79A8 File Offset: 0x000E5BA8
		[SecurityCritical]
		internal void ThreadStart()
		{
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ThreadStart)this._start)();
		}

		// Token: 0x040019EB RID: 6635
		private Delegate _start;

		// Token: 0x040019EC RID: 6636
		private object _startArg;

		// Token: 0x040019ED RID: 6637
		private ExecutionContext _executionContext;

		// Token: 0x040019EE RID: 6638
		[SecurityCritical]
		internal static ContextCallback _ccb = new ContextCallback(ThreadHelper.ThreadStart_Context);
	}
}
