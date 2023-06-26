using System;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000837 RID: 2103
	internal class ADAsyncWorkItem
	{
		// Token: 0x06005A09 RID: 23049 RVA: 0x0013EA3B File Offset: 0x0013CC3B
		[SecurityCritical]
		internal ADAsyncWorkItem(IMessage reqMsg, IMessageSink nextSink, IMessageSink replySink)
		{
			this._reqMsg = reqMsg;
			this._nextSink = nextSink;
			this._replySink = replySink;
			this._callCtx = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
		}

		// Token: 0x06005A0A RID: 23050 RVA: 0x0013EA70 File Offset: 0x0013CC70
		[SecurityCritical]
		internal virtual void FinishAsyncWork(object stateIgnored)
		{
			LogicalCallContext logicalCallContext = CallContext.SetLogicalCallContext(this._callCtx);
			IMessage message = this._nextSink.SyncProcessMessage(this._reqMsg);
			if (this._replySink != null)
			{
				this._replySink.SyncProcessMessage(message);
			}
			CallContext.SetLogicalCallContext(logicalCallContext);
		}

		// Token: 0x040028FB RID: 10491
		private IMessageSink _replySink;

		// Token: 0x040028FC RID: 10492
		private IMessageSink _nextSink;

		// Token: 0x040028FD RID: 10493
		[SecurityCritical]
		private LogicalCallContext _callCtx;

		// Token: 0x040028FE RID: 10494
		private IMessage _reqMsg;
	}
}
