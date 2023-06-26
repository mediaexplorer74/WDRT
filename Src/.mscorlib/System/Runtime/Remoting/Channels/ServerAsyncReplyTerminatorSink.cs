using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200082A RID: 2090
	internal class ServerAsyncReplyTerminatorSink : IMessageSink
	{
		// Token: 0x060059B3 RID: 22963 RVA: 0x0013D7B4 File Offset: 0x0013B9B4
		internal ServerAsyncReplyTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x0013D7C4 File Offset: 0x0013B9C4
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage replyMsg)
		{
			Guid guid;
			RemotingServices.CORProfilerRemotingServerSendingReply(out guid, true);
			if (RemotingServices.CORProfilerTrackRemotingCookie())
			{
				replyMsg.Properties["CORProfilerCookie"] = guid;
			}
			return this._nextSink.SyncProcessMessage(replyMsg);
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x0013D802 File Offset: 0x0013BA02
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage replyMsg, IMessageSink replySink)
		{
			return null;
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x060059B6 RID: 22966 RVA: 0x0013D805 File Offset: 0x0013BA05
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040028DA RID: 10458
		internal IMessageSink _nextSink;
	}
}
