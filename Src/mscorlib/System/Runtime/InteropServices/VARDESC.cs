using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.VARDESC" /> instead.</summary>
	// Token: 0x0200099A RID: 2458
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.VARDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		/// <summary>Indicates the member ID of a variable.</summary>
		// Token: 0x04002C5B RID: 11355
		public int memid;

		/// <summary>This field is reserved for future use.</summary>
		// Token: 0x04002C5C RID: 11356
		public string lpstrSchema;

		/// <summary>Contains the variable type.</summary>
		// Token: 0x04002C5D RID: 11357
		public ELEMDESC elemdescVar;

		/// <summary>Defines the properties of a variable.</summary>
		// Token: 0x04002C5E RID: 11358
		public short wVarFlags;

		/// <summary>Defines how a variable should be marshaled.</summary>
		// Token: 0x04002C5F RID: 11359
		public VarEnum varkind;

		/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.VARDESC.DESCUNION" /> instead.</summary>
		// Token: 0x02000C95 RID: 3221
		[ComVisible(false)]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Indicates the offset of this variable within the instance.</summary>
			// Token: 0x04003857 RID: 14423
			[FieldOffset(0)]
			public int oInst;

			/// <summary>Describes a symbolic constant.</summary>
			// Token: 0x04003858 RID: 14424
			[FieldOffset(0)]
			public IntPtr lpvarValue;
		}
	}
}
