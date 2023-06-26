using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x0200081F RID: 2079
	internal class LeaseSink : IMessageSink
	{
		// Token: 0x0600595D RID: 22877 RVA: 0x0013BECB File Offset: 0x0013A0CB
		public LeaseSink(Lease lease, IMessageSink nextSink)
		{
			this.lease = lease;
			this.nextSink = nextSink;
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x0013BEE1 File Offset: 0x0013A0E1
		[SecurityCritical]
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.lease.RenewOnCall();
			return this.nextSink.SyncProcessMessage(msg);
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x0013BEFA File Offset: 0x0013A0FA
		[SecurityCritical]
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this.lease.RenewOnCall();
			return this.nextSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06005960 RID: 22880 RVA: 0x0013BF14 File Offset: 0x0013A114
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this.nextSink;
			}
		}

		// Token: 0x040028AF RID: 10415
		private Lease lease;

		// Token: 0x040028B0 RID: 10416
		private IMessageSink nextSink;
	}
}
