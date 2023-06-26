using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001D5 RID: 469
	[StructLayout(LayoutKind.Sequential)]
	internal class TransmitFileBuffers
	{
		// Token: 0x040014C8 RID: 5320
		internal IntPtr preBuffer;

		// Token: 0x040014C9 RID: 5321
		internal int preBufferLength;

		// Token: 0x040014CA RID: 5322
		internal IntPtr postBuffer;

		// Token: 0x040014CB RID: 5323
		internal int postBufferLength;
	}
}
