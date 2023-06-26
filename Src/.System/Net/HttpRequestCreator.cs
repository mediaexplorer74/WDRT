using System;

namespace System.Net
{
	// Token: 0x02000138 RID: 312
	internal class HttpRequestCreator : IWebRequestCreate
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x0003DE34 File Offset: 0x0003C034
		public WebRequest Create(Uri Uri)
		{
			return new HttpWebRequest(Uri, null);
		}
	}
}
