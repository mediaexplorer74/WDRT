using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies whether applicable <see cref="Overload:System.String.Split" /> method overloads include or omit empty substrings from the return value.</summary>
	// Token: 0x02000074 RID: 116
	[ComVisible(false)]
	[Flags]
	[__DynamicallyInvokable]
	public enum StringSplitOptions
	{
		/// <summary>The return value includes array elements that contain an empty string</summary>
		// Token: 0x0400028C RID: 652
		[__DynamicallyInvokable]
		None = 0,
		/// <summary>The return value does not include array elements that contain an empty string</summary>
		// Token: 0x0400028D RID: 653
		[__DynamicallyInvokable]
		RemoveEmptyEntries = 1
	}
}
