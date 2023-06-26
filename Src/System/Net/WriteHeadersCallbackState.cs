using System;

namespace System.Net
{
	// Token: 0x020001A5 RID: 421
	internal struct WriteHeadersCallbackState
	{
		// Token: 0x0600104D RID: 4173 RVA: 0x00057494 File Offset: 0x00055694
		internal WriteHeadersCallbackState(HttpWebRequest request, ConnectStream stream)
		{
			this.request = request;
			this.stream = stream;
		}

		// Token: 0x04001369 RID: 4969
		internal HttpWebRequest request;

		// Token: 0x0400136A RID: 4970
		internal ConnectStream stream;
	}
}
