using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200081A RID: 2074
	internal class SynchronizedClientContextSink : InternalSink, IMessageSink
	{
		// Token: 0x06005925 RID: 22821 RVA: 0x0013B246 File Offset: 0x00139446
		[SecurityCritical]
		internal SynchronizedClientContextSink(SynchronizationAttribute prop, IMessageSink nextSink)
		{
			this._property = prop;
			this._nextSink = nextSink;
		}

		// Token: 0x06005926 RID: 22822 RVA: 0x0013B25C File Offset: 0x0013945C
		[SecuritySafeCritical]
		~SynchronizedClientContextSink()
		{
			this._property.Dispose();
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x0013B290 File Offset: 0x00139490
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message;
			if (this._property.IsReEntrant)
			{
				this._property.HandleThreadExit();
				message = this._nextSink.SyncProcessMessage(reqMsg);
				this._property.HandleThreadReEntry();
			}
			else
			{
				LogicalCallContext logicalCallContext = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
				string text = logicalCallContext.RemotingData.LogicalCallID;
				bool flag = false;
				if (text == null)
				{
					text = Identity.GetNewLogicalCallID();
					logicalCallContext.RemotingData.LogicalCallID = text;
					flag = true;
				}
				bool flag2 = false;
				if (this._property.SyncCallOutLCID == null)
				{
					this._property.SyncCallOutLCID = text;
					flag2 = true;
				}
				message = this._nextSink.SyncProcessMessage(reqMsg);
				if (flag2)
				{
					this._property.SyncCallOutLCID = null;
					if (flag)
					{
						LogicalCallContext logicalCallContext2 = (LogicalCallContext)message.Properties[Message.CallContextKey];
						logicalCallContext2.RemotingData.LogicalCallID = null;
					}
				}
			}
			return message;
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x0013B374 File Offset: 0x00139574
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			if (!this._property.IsReEntrant)
			{
				LogicalCallContext logicalCallContext = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
				string newLogicalCallID = Identity.GetNewLogicalCallID();
				logicalCallContext.RemotingData.LogicalCallID = newLogicalCallID;
				this._property.AsyncCallOutLCIDList.Add(newLogicalCallID);
			}
			SynchronizedClientContextSink.AsyncReplySink asyncReplySink = new SynchronizedClientContextSink.AsyncReplySink(replySink, this._property);
			return this._nextSink.AsyncProcessMessage(reqMsg, asyncReplySink);
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06005929 RID: 22825 RVA: 0x0013B3E6 File Offset: 0x001395E6
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040028A0 RID: 10400
		internal IMessageSink _nextSink;

		// Token: 0x040028A1 RID: 10401
		[SecurityCritical]
		internal SynchronizationAttribute _property;

		// Token: 0x02000C6C RID: 3180
		internal class AsyncReplySink : IMessageSink
		{
			// Token: 0x060070C3 RID: 28867 RVA: 0x00186045 File Offset: 0x00184245
			[SecurityCritical]
			internal AsyncReplySink(IMessageSink nextSink, SynchronizationAttribute prop)
			{
				this._nextSink = nextSink;
				this._property = prop;
			}

			// Token: 0x060070C4 RID: 28868 RVA: 0x0018605C File Offset: 0x0018425C
			[SecurityCritical]
			public virtual IMessage SyncProcessMessage(IMessage reqMsg)
			{
				WorkItem workItem = new WorkItem(reqMsg, this._nextSink, null);
				this._property.HandleWorkRequest(workItem);
				if (!this._property.IsReEntrant)
				{
					this._property.AsyncCallOutLCIDList.Remove(((LogicalCallContext)reqMsg.Properties[Message.CallContextKey]).RemotingData.LogicalCallID);
				}
				return workItem.ReplyMessage;
			}

			// Token: 0x060070C5 RID: 28869 RVA: 0x001860C5 File Offset: 0x001842C5
			[SecurityCritical]
			public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001352 RID: 4946
			// (get) Token: 0x060070C6 RID: 28870 RVA: 0x001860CC File Offset: 0x001842CC
			public IMessageSink NextSink
			{
				[SecurityCritical]
				get
				{
					return this._nextSink;
				}
			}

			// Token: 0x040037F3 RID: 14323
			internal IMessageSink _nextSink;

			// Token: 0x040037F4 RID: 14324
			[SecurityCritical]
			internal SynchronizationAttribute _property;
		}
	}
}
