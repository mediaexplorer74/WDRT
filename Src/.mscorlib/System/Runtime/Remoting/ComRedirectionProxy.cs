using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007BC RID: 1980
	internal class ComRedirectionProxy : MarshalByRefObject, IMessageSink
	{
		// Token: 0x060055E7 RID: 21991 RVA: 0x00132442 File Offset: 0x00130642
		internal ComRedirectionProxy(MarshalByRefObject comObject, Type serverType)
		{
			this._comObject = comObject;
			this._serverType = serverType;
		}

		// Token: 0x060055E8 RID: 21992 RVA: 0x00132458 File Offset: 0x00130658
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)msg;
			IMethodReturnMessage methodReturnMessage = RemotingServices.ExecuteMessage(this._comObject, methodCallMessage);
			if (methodReturnMessage != null)
			{
				COMException ex = methodReturnMessage.Exception as COMException;
				if (ex != null && (ex._HResult == -2147023174 || ex._HResult == -2147023169))
				{
					this._comObject = (MarshalByRefObject)Activator.CreateInstance(this._serverType, true);
					methodReturnMessage = RemotingServices.ExecuteMessage(this._comObject, methodCallMessage);
				}
			}
			return methodReturnMessage;
		}

		// Token: 0x060055E9 RID: 21993 RVA: 0x001324CC File Offset: 0x001306CC
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

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x060055EA RID: 21994 RVA: 0x001324EF File Offset: 0x001306EF
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x04002782 RID: 10114
		private MarshalByRefObject _comObject;

		// Token: 0x04002783 RID: 10115
		private Type _serverType;
	}
}
