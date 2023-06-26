using System;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x02000369 RID: 873
	internal struct NetworkEvents
	{
		// Token: 0x04001D9F RID: 7583
		public AsyncEventBits Events;

		// Token: 0x04001DA0 RID: 7584
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public int[] ErrorCodes;
	}
}
