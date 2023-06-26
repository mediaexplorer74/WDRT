using System;

namespace System.Net
{
	// Token: 0x020000E7 RID: 231
	internal class FileWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x0002BF4D File Offset: 0x0002A14D
		internal FileWebRequestCreator()
		{
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002BF55 File Offset: 0x0002A155
		public WebRequest Create(Uri uri)
		{
			return new FileWebRequest(uri);
		}
	}
}
