using System;

namespace System.Net
{
	// Token: 0x020000DC RID: 220
	internal class SystemNetworkCredential : NetworkCredential
	{
		// Token: 0x0600077E RID: 1918 RVA: 0x00029B96 File Offset: 0x00027D96
		private SystemNetworkCredential()
			: base(string.Empty, string.Empty, string.Empty)
		{
		}

		// Token: 0x04000D18 RID: 3352
		internal static readonly SystemNetworkCredential defaultCredential = new SystemNetworkCredential();
	}
}
