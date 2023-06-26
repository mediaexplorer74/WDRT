using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x0200015B RID: 347
	internal class CachedTransportContext : TransportContext
	{
		// Token: 0x06000C12 RID: 3090 RVA: 0x00041058 File Offset: 0x0003F258
		internal CachedTransportContext(ChannelBinding binding)
		{
			this.binding = binding;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00041067 File Offset: 0x0003F267
		public override ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			if (kind != ChannelBindingKind.Endpoint)
			{
				return null;
			}
			return this.binding;
		}

		// Token: 0x04001138 RID: 4408
		private ChannelBinding binding;
	}
}
