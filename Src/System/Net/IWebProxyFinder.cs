using System;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x0200014F RID: 335
	internal interface IWebProxyFinder : IDisposable
	{
		// Token: 0x06000BA6 RID: 2982
		bool GetProxies(Uri destination, out IList<string> proxyList);

		// Token: 0x06000BA7 RID: 2983
		void Abort();

		// Token: 0x06000BA8 RID: 2984
		void Reset();

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000BA9 RID: 2985
		bool IsValid { get; }
	}
}
