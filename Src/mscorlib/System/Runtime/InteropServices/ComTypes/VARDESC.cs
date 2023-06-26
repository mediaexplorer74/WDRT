using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes a variable, constant, or data member.</summary>
	// Token: 0x02000A44 RID: 2628
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		/// <summary>Indicates the member ID of a variable.</summary>
		// Token: 0x04002DC4 RID: 11716
		[__DynamicallyInvokable]
		public int memid;

		/// <summary>This field is reserved for future use.</summary>
		// Token: 0x04002DC5 RID: 11717
		[__DynamicallyInvokable]
		public string lpstrSchema;

		/// <summary>Contains information about a variable.</summary>
		// Token: 0x04002DC6 RID: 11718
		[__DynamicallyInvokable]
		public VARDESC.DESCUNION desc;

		/// <summary>Contains the variable type.</summary>
		// Token: 0x04002DC7 RID: 11719
		[__DynamicallyInvokable]
		public ELEMDESC elemdescVar;

		/// <summary>Defines the properties of a variable.</summary>
		// Token: 0x04002DC8 RID: 11720
		[__DynamicallyInvokable]
		public short wVarFlags;

		/// <summary>Defines how to marshal a variable.</summary>
		// Token: 0x04002DC9 RID: 11721
		[__DynamicallyInvokable]
		public VARKIND varkind;

		/// <summary>Contains information about a variable.</summary>
		// Token: 0x02000CA5 RID: 3237
		[__DynamicallyInvokable]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Indicates the offset of this variable within the instance.</summary>
			// Token: 0x0400388A RID: 14474
			[__DynamicallyInvokable]
			[FieldOffset(0)]
			public int oInst;

			/// <summary>Describes a symbolic constant.</summary>
			// Token: 0x0400388B RID: 14475
			[FieldOffset(0)]
			public IntPtr lpvarValue;
		}
	}
}
