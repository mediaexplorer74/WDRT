using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000818 RID: 2072
	internal class SynchronizedServerContextSink : InternalSink, IMessageSink
	{
		// Token: 0x06005913 RID: 22803 RVA: 0x0013B038 File Offset: 0x00139238
		[SecurityCritical]
		internal SynchronizedServerContextSink(SynchronizationAttribute prop, IMessageSink nextSink)
		{
			this._property = prop;
			this._nextSink = nextSink;
		}

		// Token: 0x06005914 RID: 22804 RVA: 0x0013B050 File Offset: 0x00139250
		[SecuritySafeCritical]
		~SynchronizedServerContextSink()
		{
			this._property.Dispose();
		}

		// Token: 0x06005915 RID: 22805 RVA: 0x0013B084 File Offset: 0x00139284
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			WorkItem workItem = new WorkItem(reqMsg, this._nextSink, null);
			this._property.HandleWorkRequest(workItem);
			return workItem.ReplyMessage;
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x0013B0B4 File Offset: 0x001392B4
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			WorkItem workItem = new WorkItem(reqMsg, this._nextSink, replySink);
			workItem.SetAsync();
			this._property.HandleWorkRequest(workItem);
			return null;
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06005917 RID: 22807 RVA: 0x0013B0E2 File Offset: 0x001392E2
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x04002892 RID: 10386
		internal IMessageSink _nextSink;

		// Token: 0x04002893 RID: 10387
		[SecurityCritical]
		internal SynchronizationAttribute _property;
	}
}
