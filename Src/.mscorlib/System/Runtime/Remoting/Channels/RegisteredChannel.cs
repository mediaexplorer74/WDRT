using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000827 RID: 2087
	internal class RegisteredChannel
	{
		// Token: 0x060059A4 RID: 22948 RVA: 0x0013D628 File Offset: 0x0013B828
		internal RegisteredChannel(IChannel chnl)
		{
			this.channel = chnl;
			this.flags = 0;
			if (chnl is IChannelSender)
			{
				this.flags |= 1;
			}
			if (chnl is IChannelReceiver)
			{
				this.flags |= 2;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x060059A5 RID: 22949 RVA: 0x0013D677 File Offset: 0x0013B877
		internal virtual IChannel Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x060059A6 RID: 22950 RVA: 0x0013D67F File Offset: 0x0013B87F
		internal virtual bool IsSender()
		{
			return (this.flags & 1) > 0;
		}

		// Token: 0x060059A7 RID: 22951 RVA: 0x0013D68C File Offset: 0x0013B88C
		internal virtual bool IsReceiver()
		{
			return (this.flags & 2) > 0;
		}

		// Token: 0x040028D1 RID: 10449
		private IChannel channel;

		// Token: 0x040028D2 RID: 10450
		private byte flags;

		// Token: 0x040028D3 RID: 10451
		private const byte SENDER = 1;

		// Token: 0x040028D4 RID: 10452
		private const byte RECEIVER = 2;
	}
}
