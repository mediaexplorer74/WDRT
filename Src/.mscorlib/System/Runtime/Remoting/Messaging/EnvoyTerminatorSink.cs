using System;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000880 RID: 2176
	[Serializable]
	internal class EnvoyTerminatorSink : InternalSink, IMessageSink
	{
		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06005C95 RID: 23701 RVA: 0x00145F84 File Offset: 0x00144184
		internal static IMessageSink MessageSink
		{
			get
			{
				if (EnvoyTerminatorSink.messageSink == null)
				{
					EnvoyTerminatorSink envoyTerminatorSink = new EnvoyTerminatorSink();
					object obj = EnvoyTerminatorSink.staticSyncObject;
					lock (obj)
					{
						if (EnvoyTerminatorSink.messageSink == null)
						{
							EnvoyTerminatorSink.messageSink = envoyTerminatorSink;
						}
					}
				}
				return EnvoyTerminatorSink.messageSink;
			}
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x00145FE4 File Offset: 0x001441E4
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				return message;
			}
			return Thread.CurrentContext.GetClientContextChain().SyncProcessMessage(reqMsg);
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x00146010 File Offset: 0x00144210
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			IMessageCtrl messageCtrl = null;
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(message);
				}
			}
			else
			{
				messageCtrl = Thread.CurrentContext.GetClientContextChain().AsyncProcessMessage(reqMsg, replySink);
			}
			return messageCtrl;
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06005C98 RID: 23704 RVA: 0x00146049 File Offset: 0x00144249
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x040029CF RID: 10703
		private static volatile EnvoyTerminatorSink messageSink;

		// Token: 0x040029D0 RID: 10704
		private static object staticSyncObject = new object();
	}
}
