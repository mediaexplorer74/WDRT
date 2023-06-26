using System;
using System.IO;

namespace System.Net
{
	// Token: 0x0200013A RID: 314
	internal class CoreResponseData
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x0003DEB8 File Offset: 0x0003C0B8
		internal CoreResponseData Clone()
		{
			return new CoreResponseData
			{
				m_StatusCode = this.m_StatusCode,
				m_StatusDescription = this.m_StatusDescription,
				m_IsVersionHttp11 = this.m_IsVersionHttp11,
				m_ContentLength = this.m_ContentLength,
				m_ResponseHeaders = this.m_ResponseHeaders,
				m_ConnectStream = this.m_ConnectStream
			};
		}

		// Token: 0x0400106E RID: 4206
		public HttpStatusCode m_StatusCode;

		// Token: 0x0400106F RID: 4207
		public string m_StatusDescription;

		// Token: 0x04001070 RID: 4208
		public bool m_IsVersionHttp11;

		// Token: 0x04001071 RID: 4209
		public long m_ContentLength;

		// Token: 0x04001072 RID: 4210
		public WebHeaderCollection m_ResponseHeaders;

		// Token: 0x04001073 RID: 4211
		public Stream m_ConnectStream;
	}
}
