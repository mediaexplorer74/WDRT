using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Provides an explicit layout that is visible from unmanaged code and that will have the same layout as the Win32 OVERLAPPED structure with additional reserved fields at the end.</summary>
	// Token: 0x02000501 RID: 1281
	[ComVisible(true)]
	public struct NativeOverlapped
	{
		/// <summary>Specifies a system-dependent status. Reserved for operating system use.</summary>
		// Token: 0x040019AC RID: 6572
		public IntPtr InternalLow;

		/// <summary>Specifies the length of the data transferred. Reserved for operating system use.</summary>
		// Token: 0x040019AD RID: 6573
		public IntPtr InternalHigh;

		/// <summary>Specifies a file position at which to start the transfer.</summary>
		// Token: 0x040019AE RID: 6574
		public int OffsetLow;

		/// <summary>Specifies the high word of the byte offset at which to start the transfer.</summary>
		// Token: 0x040019AF RID: 6575
		public int OffsetHigh;

		/// <summary>Specifies the handle to an event set to the signaled state when the operation is complete. The calling process must set this member either to zero or to a valid event handle before calling any overlapped functions.</summary>
		// Token: 0x040019B0 RID: 6576
		public IntPtr EventHandle;
	}
}
