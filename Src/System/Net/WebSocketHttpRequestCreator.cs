using System;
using System.Net.WebSockets;

namespace System.Net
{
	// Token: 0x02000139 RID: 313
	internal class WebSocketHttpRequestCreator : IWebRequestCreate
	{
		// Token: 0x06000B48 RID: 2888 RVA: 0x0003DE45 File Offset: 0x0003C045
		public WebSocketHttpRequestCreator(bool usingHttps)
		{
			this.m_httpScheme = (usingHttps ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0003DE64 File Offset: 0x0003C064
		public WebRequest Create(Uri Uri)
		{
			HttpWebRequest httpWebRequest = new HttpWebRequest(new UriBuilder(Uri)
			{
				Scheme = this.m_httpScheme
			}.Uri, null, true, "WebSocket" + Guid.NewGuid().ToString());
			WebSocketHelpers.PrepareWebRequest(ref httpWebRequest);
			return httpWebRequest;
		}

		// Token: 0x0400106D RID: 4205
		private string m_httpScheme;
	}
}
