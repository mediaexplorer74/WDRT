using System;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000882 RID: 2178
	internal class AsyncReplySink : IMessageSink
	{
		// Token: 0x06005CA4 RID: 23716 RVA: 0x0014631A File Offset: 0x0014451A
		internal AsyncReplySink(IMessageSink replySink, Context cliCtx)
		{
			this._replySink = replySink;
			this._cliCtx = cliCtx;
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x00146330 File Offset: 0x00144530
		[SecurityCritical]
		internal static object SyncProcessMessageCallback(object[] args)
		{
			IMessage message = (IMessage)args[0];
			IMessageSink messageSink = (IMessageSink)args[1];
			Thread.CurrentContext.NotifyDynamicSinks(message, true, false, true, true);
			return messageSink.SyncProcessMessage(message);
		}

		// Token: 0x06005CA6 RID: 23718 RVA: 0x00146368 File Offset: 0x00144568
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = null;
			if (this._replySink != null)
			{
				object[] array = new object[] { reqMsg, this._replySink };
				InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(AsyncReplySink.SyncProcessMessageCallback);
				message = (IMessage)Thread.CurrentThread.InternalCrossContextCallback(this._cliCtx, internalCrossContextDelegate, array);
			}
			return message;
		}

		// Token: 0x06005CA7 RID: 23719 RVA: 0x001463B9 File Offset: 0x001445B9
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06005CA8 RID: 23720 RVA: 0x001463C0 File Offset: 0x001445C0
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x040029D3 RID: 10707
		private IMessageSink _replySink;

		// Token: 0x040029D4 RID: 10708
		private Context _cliCtx;
	}
}
