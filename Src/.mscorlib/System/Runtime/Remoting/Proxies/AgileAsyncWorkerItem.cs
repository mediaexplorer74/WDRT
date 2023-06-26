using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x02000801 RID: 2049
	internal class AgileAsyncWorkerItem
	{
		// Token: 0x06005882 RID: 22658 RVA: 0x001395CF File Offset: 0x001377CF
		[SecurityCritical]
		public AgileAsyncWorkerItem(IMethodCallMessage message, AsyncResult ar, object target)
		{
			this._message = new MethodCall(message);
			this._ar = ar;
			this._target = target;
		}

		// Token: 0x06005883 RID: 22659 RVA: 0x001395F1 File Offset: 0x001377F1
		[SecurityCritical]
		public static void ThreadPoolCallBack(object o)
		{
			((AgileAsyncWorkerItem)o).DoAsyncCall();
		}

		// Token: 0x06005884 RID: 22660 RVA: 0x001395FE File Offset: 0x001377FE
		[SecurityCritical]
		public void DoAsyncCall()
		{
			new StackBuilderSink(this._target).AsyncProcessMessage(this._message, this._ar);
		}

		// Token: 0x04002856 RID: 10326
		private IMethodCallMessage _message;

		// Token: 0x04002857 RID: 10327
		private AsyncResult _ar;

		// Token: 0x04002858 RID: 10328
		private object _target;
	}
}
