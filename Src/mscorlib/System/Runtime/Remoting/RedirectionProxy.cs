using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007BB RID: 1979
	internal class RedirectionProxy : MarshalByRefObject, IMessageSink
	{
		// Token: 0x060055E2 RID: 21986 RVA: 0x00132358 File Offset: 0x00130558
		[SecurityCritical]
		internal RedirectionProxy(MarshalByRefObject proxy, Type serverType)
		{
			this._proxy = proxy;
			this._realProxy = RemotingServices.GetRealProxy(this._proxy);
			this._serverType = serverType;
			this._objectMode = WellKnownObjectMode.Singleton;
		}

		// Token: 0x17000E22 RID: 3618
		// (set) Token: 0x060055E3 RID: 21987 RVA: 0x00132386 File Offset: 0x00130586
		public WellKnownObjectMode ObjectMode
		{
			set
			{
				this._objectMode = value;
			}
		}

		// Token: 0x060055E4 RID: 21988 RVA: 0x00132390 File Offset: 0x00130590
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage message = null;
			try
			{
				msg.Properties["__Uri"] = this._realProxy.IdentityObject.URI;
				if (this._objectMode == WellKnownObjectMode.Singleton)
				{
					message = this._realProxy.Invoke(msg);
				}
				else
				{
					MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._serverType, true);
					RealProxy realProxy = RemotingServices.GetRealProxy(marshalByRefObject);
					message = realProxy.Invoke(msg);
				}
			}
			catch (Exception ex)
			{
				message = new ReturnMessage(ex, msg as IMethodCallMessage);
			}
			return message;
		}

		// Token: 0x060055E5 RID: 21989 RVA: 0x0013241C File Offset: 0x0013061C
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			IMessage message = this.SyncProcessMessage(msg);
			if (replySink != null)
			{
				replySink.SyncProcessMessage(message);
			}
			return null;
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x060055E6 RID: 21990 RVA: 0x0013243F File Offset: 0x0013063F
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x0400277E RID: 10110
		private MarshalByRefObject _proxy;

		// Token: 0x0400277F RID: 10111
		[SecurityCritical]
		private RealProxy _realProxy;

		// Token: 0x04002780 RID: 10112
		private Type _serverType;

		// Token: 0x04002781 RID: 10113
		private WellKnownObjectMode _objectMode;
	}
}
