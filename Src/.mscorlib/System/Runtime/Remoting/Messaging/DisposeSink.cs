using System;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000884 RID: 2180
	internal class DisposeSink : IMessageSink
	{
		// Token: 0x06005CB0 RID: 23728 RVA: 0x00146553 File Offset: 0x00144753
		internal DisposeSink(IDisposable iDis, IMessageSink replySink)
		{
			this._iDis = iDis;
			this._replySink = replySink;
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x0014656C File Offset: 0x0014476C
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = null;
			try
			{
				if (this._replySink != null)
				{
					message = this._replySink.SyncProcessMessage(reqMsg);
				}
			}
			finally
			{
				this._iDis.Dispose();
			}
			return message;
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x001465B0 File Offset: 0x001447B0
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06005CB3 RID: 23731 RVA: 0x001465B7 File Offset: 0x001447B7
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x040029D7 RID: 10711
		private IDisposable _iDis;

		// Token: 0x040029D8 RID: 10712
		private IMessageSink _replySink;
	}
}
