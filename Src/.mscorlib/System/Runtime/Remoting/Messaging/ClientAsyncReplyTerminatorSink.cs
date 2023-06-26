using System;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000886 RID: 2182
	internal class ClientAsyncReplyTerminatorSink : IMessageSink
	{
		// Token: 0x06005CB8 RID: 23736 RVA: 0x0014669B File Offset: 0x0014489B
		internal ClientAsyncReplyTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06005CB9 RID: 23737 RVA: 0x001466AC File Offset: 0x001448AC
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage replyMsg)
		{
			Guid guid = Guid.Empty;
			if (RemotingServices.CORProfilerTrackRemotingCookie())
			{
				object obj = replyMsg.Properties["CORProfilerCookie"];
				if (obj != null)
				{
					guid = (Guid)obj;
				}
			}
			RemotingServices.CORProfilerRemotingClientReceivingReply(guid, true);
			return this._nextSink.SyncProcessMessage(replyMsg);
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x001466F4 File Offset: 0x001448F4
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage replyMsg, IMessageSink replySink)
		{
			return null;
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06005CBB RID: 23739 RVA: 0x001466F7 File Offset: 0x001448F7
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040029DA RID: 10714
		internal IMessageSink _nextSink;
	}
}
