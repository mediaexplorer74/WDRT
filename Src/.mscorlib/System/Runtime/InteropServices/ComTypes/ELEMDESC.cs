using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains the type description and process transfer information for a variable, function, or a function parameter.</summary>
	// Token: 0x02000A42 RID: 2626
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ELEMDESC
	{
		/// <summary>Identifies the type of the element.</summary>
		// Token: 0x04002DBD RID: 11709
		[__DynamicallyInvokable]
		public TYPEDESC tdesc;

		/// <summary>Contains information about an element.</summary>
		// Token: 0x04002DBE RID: 11710
		[__DynamicallyInvokable]
		public ELEMDESC.DESCUNION desc;

		/// <summary>Contains information about an element.</summary>
		// Token: 0x02000CA4 RID: 3236
		[__DynamicallyInvokable]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Contains information for remoting the element.</summary>
			// Token: 0x04003888 RID: 14472
			[__DynamicallyInvokable]
			[FieldOffset(0)]
			public IDLDESC idldesc;

			/// <summary>Contains information about the parameter.</summary>
			// Token: 0x04003889 RID: 14473
			[__DynamicallyInvokable]
			[FieldOffset(0)]
			public PARAMDESC paramdesc;
		}
	}
}
