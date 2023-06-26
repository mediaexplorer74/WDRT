using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000521 RID: 1313
	internal sealed class QueueUserWorkItemCallback : IThreadPoolWorkItem
	{
		// Token: 0x06003DFE RID: 15870 RVA: 0x000E8CAB File Offset: 0x000E6EAB
		[SecurityCritical]
		internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, bool compressStack, ref StackCrawlMark stackMark)
		{
			this.callback = waitCallback;
			this.state = stateObj;
			if (compressStack && !ExecutionContext.IsFlowSuppressed())
			{
				this.context = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x000E8CD9 File Offset: 0x000E6ED9
		internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, ExecutionContext ec)
		{
			this.callback = waitCallback;
			this.state = stateObj;
			this.context = ec;
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x000E8CF8 File Offset: 0x000E6EF8
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			if (this.context == null)
			{
				WaitCallback waitCallback = this.callback;
				this.callback = null;
				waitCallback(this.state);
				return;
			}
			ExecutionContext.Run(this.context, QueueUserWorkItemCallback.ccb, this, true);
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x000E8D3A File Offset: 0x000E6F3A
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x000E8D3C File Offset: 0x000E6F3C
		[SecurityCritical]
		private static void WaitCallback_Context(object state)
		{
			QueueUserWorkItemCallback queueUserWorkItemCallback = (QueueUserWorkItemCallback)state;
			WaitCallback waitCallback = queueUserWorkItemCallback.callback;
			waitCallback(queueUserWorkItemCallback.state);
		}

		// Token: 0x04001A1C RID: 6684
		private WaitCallback callback;

		// Token: 0x04001A1D RID: 6685
		private ExecutionContext context;

		// Token: 0x04001A1E RID: 6686
		private object state;

		// Token: 0x04001A1F RID: 6687
		[SecurityCritical]
		internal static ContextCallback ccb = new ContextCallback(QueueUserWorkItemCallback.WaitCallback_Context);
	}
}
