using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000158 RID: 344
	internal class ConnectStreamContext : TransportContext
	{
		// Token: 0x06000C0B RID: 3083 RVA: 0x00040FCA File Offset: 0x0003F1CA
		internal ConnectStreamContext(ConnectStream connectStream)
		{
			this.connectStream = connectStream;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00040FD9 File Offset: 0x0003F1D9
		public override ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			return this.connectStream.GetChannelBinding(kind);
		}

		// Token: 0x04001135 RID: 4405
		private ConnectStream connectStream;
	}
}
