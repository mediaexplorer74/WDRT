using System;

namespace System.Net
{
	// Token: 0x020001DE RID: 478
	internal interface IAutoWebProxy : IWebProxy
	{
		// Token: 0x060012B0 RID: 4784
		ProxyChain GetProxies(Uri destination);
	}
}
