using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000188 RID: 392
	internal class WebProxyData
	{
		// Token: 0x0400126C RID: 4716
		internal bool bypassOnLocal;

		// Token: 0x0400126D RID: 4717
		internal bool automaticallyDetectSettings;

		// Token: 0x0400126E RID: 4718
		internal Uri proxyAddress;

		// Token: 0x0400126F RID: 4719
		internal Hashtable proxyHostAddresses;

		// Token: 0x04001270 RID: 4720
		internal Uri scriptLocation;

		// Token: 0x04001271 RID: 4721
		internal ArrayList bypassList;
	}
}
