using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x0200015A RID: 346
	internal class HttpListenerRequestContext : TransportContext
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x00041004 File Offset: 0x0003F204
		internal HttpListenerRequestContext(HttpListenerRequest request)
		{
			this.request = request;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00041013 File Offset: 0x0003F213
		public override ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			if (kind != ChannelBindingKind.Endpoint)
			{
				throw new NotSupportedException(SR.GetString("net_listener_invalid_cbt_type", new object[] { kind.ToString() }));
			}
			return this.request.GetChannelBinding();
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0004104B File Offset: 0x0003F24B
		public override IEnumerable<TokenBinding> GetTlsTokenBindings()
		{
			return this.request.GetTlsTokenBindings();
		}

		// Token: 0x04001137 RID: 4407
		private HttpListenerRequest request;
	}
}
