using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Contains constants that specify infinite time-out intervals. This class cannot be inherited.</summary>
	// Token: 0x0200052A RID: 1322
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public static class Timeout
	{
		/// <summary>A constant used to specify an infinite waiting period, for methods that accept a <see cref="T:System.TimeSpan" /> parameter.</summary>
		// Token: 0x04001A36 RID: 6710
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);

		/// <summary>A constant used to specify an infinite waiting period, for threading methods that accept an <see cref="T:System.Int32" /> parameter.</summary>
		// Token: 0x04001A37 RID: 6711
		[__DynamicallyInvokable]
		public const int Infinite = -1;

		// Token: 0x04001A38 RID: 6712
		internal const uint UnsignedInfinite = 4294967295U;
	}
}
