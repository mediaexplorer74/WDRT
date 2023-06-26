using System;

namespace System.Net
{
	// Token: 0x020001E2 RID: 482
	internal class StaticProxy : ProxyChain
	{
		// Token: 0x060012C0 RID: 4800 RVA: 0x000635EA File Offset: 0x000617EA
		internal StaticProxy(Uri destination, Uri proxy)
			: base(destination)
		{
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.m_Proxy = proxy;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0006360E File Offset: 0x0006180E
		protected override bool GetNextProxy(out Uri proxy)
		{
			proxy = this.m_Proxy;
			if (proxy == null)
			{
				return false;
			}
			this.m_Proxy = null;
			return true;
		}

		// Token: 0x0400150D RID: 5389
		private Uri m_Proxy;
	}
}
