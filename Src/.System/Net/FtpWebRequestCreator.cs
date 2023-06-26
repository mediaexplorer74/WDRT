using System;

namespace System.Net
{
	// Token: 0x020000F0 RID: 240
	internal class FtpWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x0002F1E5 File Offset: 0x0002D3E5
		internal FtpWebRequestCreator()
		{
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002F1ED File Offset: 0x0002D3ED
		public WebRequest Create(Uri uri)
		{
			return new FtpWebRequest(uri);
		}
	}
}
