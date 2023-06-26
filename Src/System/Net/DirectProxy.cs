using System;

namespace System.Net
{
	// Token: 0x020001E1 RID: 481
	internal class DirectProxy : ProxyChain
	{
		// Token: 0x060012BE RID: 4798 RVA: 0x000635CA File Offset: 0x000617CA
		internal DirectProxy(Uri destination)
			: base(destination)
		{
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000635D3 File Offset: 0x000617D3
		protected override bool GetNextProxy(out Uri proxy)
		{
			proxy = null;
			if (this.m_ProxyRetrieved)
			{
				return false;
			}
			this.m_ProxyRetrieved = true;
			return true;
		}

		// Token: 0x0400150C RID: 5388
		private bool m_ProxyRetrieved;
	}
}
