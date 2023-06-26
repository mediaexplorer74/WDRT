using System;

namespace System.Net
{
	// Token: 0x020001A1 RID: 417
	internal struct TunnelStateObject
	{
		// Token: 0x06000FEC RID: 4076 RVA: 0x000536DF File Offset: 0x000518DF
		internal TunnelStateObject(HttpWebRequest r, Connection c)
		{
			this.Connection = c;
			this.OriginalRequest = r;
		}

		// Token: 0x0400131E RID: 4894
		internal Connection Connection;

		// Token: 0x0400131F RID: 4895
		internal HttpWebRequest OriginalRequest;
	}
}
