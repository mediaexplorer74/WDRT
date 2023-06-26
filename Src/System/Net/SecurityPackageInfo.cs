using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000216 RID: 534
	internal struct SecurityPackageInfo
	{
		// Token: 0x040015AB RID: 5547
		internal int Capabilities;

		// Token: 0x040015AC RID: 5548
		internal short Version;

		// Token: 0x040015AD RID: 5549
		internal short RPCID;

		// Token: 0x040015AE RID: 5550
		internal int MaxToken;

		// Token: 0x040015AF RID: 5551
		internal IntPtr Name;

		// Token: 0x040015B0 RID: 5552
		internal IntPtr Comment;

		// Token: 0x040015B1 RID: 5553
		internal static readonly int Size = Marshal.SizeOf(typeof(SecurityPackageInfo));

		// Token: 0x040015B2 RID: 5554
		internal static readonly int NameOffest = (int)Marshal.OffsetOf(typeof(SecurityPackageInfo), "Name");
	}
}
