using System;
using System.Net.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000159 RID: 345
	internal class SslStreamContext : TransportContext
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x00040FE7 File Offset: 0x0003F1E7
		internal SslStreamContext(SslStream sslStream)
		{
			this.sslStream = sslStream;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00040FF6 File Offset: 0x0003F1F6
		public override ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			return this.sslStream.GetChannelBinding(kind);
		}

		// Token: 0x04001136 RID: 4406
		private SslStream sslStream;
	}
}
