using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains the arguments passed to a method or property by <see langword="IDispatch::Invoke" />.</summary>
	// Token: 0x02000A45 RID: 2629
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		/// <summary>Represents a reference to the array of arguments.</summary>
		// Token: 0x04002DCA RID: 11722
		[__DynamicallyInvokable]
		public IntPtr rgvarg;

		/// <summary>Represents the dispatch IDs of named arguments.</summary>
		// Token: 0x04002DCB RID: 11723
		[__DynamicallyInvokable]
		public IntPtr rgdispidNamedArgs;

		/// <summary>Represents the count of arguments.</summary>
		// Token: 0x04002DCC RID: 11724
		[__DynamicallyInvokable]
		public int cArgs;

		/// <summary>Represents the count of named arguments</summary>
		// Token: 0x04002DCD RID: 11725
		[__DynamicallyInvokable]
		public int cNamedArgs;
	}
}
