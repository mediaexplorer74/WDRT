using System;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000819 RID: 2073
	internal class WorkItem
	{
		// Token: 0x06005919 RID: 22809 RVA: 0x0013B100 File Offset: 0x00139300
		[SecurityCritical]
		internal WorkItem(IMessage reqMsg, IMessageSink nextSink, IMessageSink replySink)
		{
			this._reqMsg = reqMsg;
			this._replyMsg = null;
			this._nextSink = nextSink;
			this._replySink = replySink;
			this._ctx = Thread.CurrentContext;
			this._callCtx = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
		}

		// Token: 0x0600591A RID: 22810 RVA: 0x0013B14F File Offset: 0x0013934F
		internal virtual void SetWaiting()
		{
			this._flags |= 1;
		}

		// Token: 0x0600591B RID: 22811 RVA: 0x0013B15F File Offset: 0x0013935F
		internal virtual bool IsWaiting()
		{
			return (this._flags & 1) == 1;
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x0013B16C File Offset: 0x0013936C
		internal virtual void SetSignaled()
		{
			this._flags |= 2;
		}

		// Token: 0x0600591D RID: 22813 RVA: 0x0013B17C File Offset: 0x0013937C
		internal virtual bool IsSignaled()
		{
			return (this._flags & 2) == 2;
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x0013B189 File Offset: 0x00139389
		internal virtual void SetAsync()
		{
			this._flags |= 4;
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x0013B199 File Offset: 0x00139399
		internal virtual bool IsAsync()
		{
			return (this._flags & 4) == 4;
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x0013B1A6 File Offset: 0x001393A6
		internal virtual void SetDummy()
		{
			this._flags |= 8;
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x0013B1B6 File Offset: 0x001393B6
		internal virtual bool IsDummy()
		{
			return (this._flags & 8) == 8;
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x0013B1C4 File Offset: 0x001393C4
		[SecurityCritical]
		internal static object ExecuteCallback(object[] args)
		{
			WorkItem workItem = (WorkItem)args[0];
			if (workItem.IsAsync())
			{
				workItem._nextSink.AsyncProcessMessage(workItem._reqMsg, workItem._replySink);
			}
			else if (workItem._nextSink != null)
			{
				workItem._replyMsg = workItem._nextSink.SyncProcessMessage(workItem._reqMsg);
			}
			return null;
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x0013B21C File Offset: 0x0013941C
		[SecurityCritical]
		internal virtual void Execute()
		{
			Thread.CurrentThread.InternalCrossContextCallback(this._ctx, WorkItem._xctxDel, new object[] { this });
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06005924 RID: 22820 RVA: 0x0013B23E File Offset: 0x0013943E
		internal virtual IMessage ReplyMessage
		{
			get
			{
				return this._replyMsg;
			}
		}

		// Token: 0x04002894 RID: 10388
		private const int FLG_WAITING = 1;

		// Token: 0x04002895 RID: 10389
		private const int FLG_SIGNALED = 2;

		// Token: 0x04002896 RID: 10390
		private const int FLG_ASYNC = 4;

		// Token: 0x04002897 RID: 10391
		private const int FLG_DUMMY = 8;

		// Token: 0x04002898 RID: 10392
		internal int _flags;

		// Token: 0x04002899 RID: 10393
		internal IMessage _reqMsg;

		// Token: 0x0400289A RID: 10394
		internal IMessageSink _nextSink;

		// Token: 0x0400289B RID: 10395
		internal IMessageSink _replySink;

		// Token: 0x0400289C RID: 10396
		internal IMessage _replyMsg;

		// Token: 0x0400289D RID: 10397
		internal Context _ctx;

		// Token: 0x0400289E RID: 10398
		[SecurityCritical]
		internal LogicalCallContext _callCtx;

		// Token: 0x0400289F RID: 10399
		internal static InternalCrossContextDelegate _xctxDel = new InternalCrossContextDelegate(WorkItem.ExecuteCallback);
	}
}
