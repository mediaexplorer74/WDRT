using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines a function description.</summary>
	// Token: 0x02000A3C RID: 2620
	[__DynamicallyInvokable]
	public struct FUNCDESC
	{
		/// <summary>Identifies the function member ID.</summary>
		// Token: 0x04002D9C RID: 11676
		[__DynamicallyInvokable]
		public int memid;

		/// <summary>Stores the count of errors a function can return on a 16-bit system.</summary>
		// Token: 0x04002D9D RID: 11677
		public IntPtr lprgscode;

		/// <summary>Indicates the size of <see cref="F:System.Runtime.InteropServices.FUNCDESC.cParams" />.</summary>
		// Token: 0x04002D9E RID: 11678
		public IntPtr lprgelemdescParam;

		/// <summary>Specifies whether the function is virtual, static, or dispatch-only.</summary>
		// Token: 0x04002D9F RID: 11679
		[__DynamicallyInvokable]
		public FUNCKIND funckind;

		/// <summary>Specifies the type of a property function.</summary>
		// Token: 0x04002DA0 RID: 11680
		[__DynamicallyInvokable]
		public INVOKEKIND invkind;

		/// <summary>Specifies the calling convention of a function.</summary>
		// Token: 0x04002DA1 RID: 11681
		[__DynamicallyInvokable]
		public CALLCONV callconv;

		/// <summary>Counts the total number of parameters.</summary>
		// Token: 0x04002DA2 RID: 11682
		[__DynamicallyInvokable]
		public short cParams;

		/// <summary>Counts the optional parameters.</summary>
		// Token: 0x04002DA3 RID: 11683
		[__DynamicallyInvokable]
		public short cParamsOpt;

		/// <summary>Specifies the offset in the VTBL for <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_VIRTUAL" />.</summary>
		// Token: 0x04002DA4 RID: 11684
		[__DynamicallyInvokable]
		public short oVft;

		/// <summary>Counts the permitted return values.</summary>
		// Token: 0x04002DA5 RID: 11685
		[__DynamicallyInvokable]
		public short cScodes;

		/// <summary>Contains the return type of the function.</summary>
		// Token: 0x04002DA6 RID: 11686
		[__DynamicallyInvokable]
		public ELEMDESC elemdescFunc;

		/// <summary>Indicates the <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> of a function.</summary>
		// Token: 0x04002DA7 RID: 11687
		[__DynamicallyInvokable]
		public short wFuncFlags;
	}
}
