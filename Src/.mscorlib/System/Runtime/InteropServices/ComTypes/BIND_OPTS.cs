using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Stores the parameters that are used during a moniker binding operation.</summary>
	// Token: 0x02000A21 RID: 2593
	[__DynamicallyInvokable]
	public struct BIND_OPTS
	{
		/// <summary>Specifies the size, in bytes, of the <see langword="BIND_OPTS" /> structure.</summary>
		// Token: 0x04002D4D RID: 11597
		[__DynamicallyInvokable]
		public int cbStruct;

		/// <summary>Controls aspects of moniker binding operations.</summary>
		// Token: 0x04002D4E RID: 11598
		[__DynamicallyInvokable]
		public int grfFlags;

		/// <summary>Represents flags that should be used when opening the file that contains the object identified by the moniker.</summary>
		// Token: 0x04002D4F RID: 11599
		[__DynamicallyInvokable]
		public int grfMode;

		/// <summary>Indicates the amount of time (clock time in milliseconds, as returned by the <see langword="GetTickCount" /> function) that the caller specified to complete the binding operation.</summary>
		// Token: 0x04002D50 RID: 11600
		[__DynamicallyInvokable]
		public int dwTickCountDeadline;
	}
}
