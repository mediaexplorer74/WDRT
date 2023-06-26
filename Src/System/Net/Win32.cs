using System;

namespace System.Net
{
	// Token: 0x0200021E RID: 542
	internal static class Win32
	{
		// Token: 0x040015EC RID: 5612
		internal const int OverlappedInternalOffset = 0;

		// Token: 0x040015ED RID: 5613
		internal static int OverlappedInternalHighOffset = IntPtr.Size;

		// Token: 0x040015EE RID: 5614
		internal static int OverlappedOffsetOffset = IntPtr.Size * 2;

		// Token: 0x040015EF RID: 5615
		internal static int OverlappedOffsetHighOffset = IntPtr.Size * 2 + 4;

		// Token: 0x040015F0 RID: 5616
		internal static int OverlappedhEventOffset = IntPtr.Size * 2 + 8;

		// Token: 0x040015F1 RID: 5617
		internal static int OverlappedSize = IntPtr.Size * 3 + 8;
	}
}
