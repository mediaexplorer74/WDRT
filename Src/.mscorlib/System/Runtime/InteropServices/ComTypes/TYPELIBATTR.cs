using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies a particular type library and provides localization support for member names.</summary>
	// Token: 0x02000A4F RID: 2639
	[__DynamicallyInvokable]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		/// <summary>Represents a globally unique library ID of a type library.</summary>
		// Token: 0x04002E13 RID: 11795
		[__DynamicallyInvokable]
		public Guid guid;

		/// <summary>Represents a locale ID of a type library.</summary>
		// Token: 0x04002E14 RID: 11796
		[__DynamicallyInvokable]
		public int lcid;

		/// <summary>Represents the target hardware platform of a type library.</summary>
		// Token: 0x04002E15 RID: 11797
		[__DynamicallyInvokable]
		public SYSKIND syskind;

		/// <summary>Represents the major version number of a type library.</summary>
		// Token: 0x04002E16 RID: 11798
		[__DynamicallyInvokable]
		public short wMajorVerNum;

		/// <summary>Represents the minor version number of a type library.</summary>
		// Token: 0x04002E17 RID: 11799
		[__DynamicallyInvokable]
		public short wMinorVerNum;

		/// <summary>Represents library flags.</summary>
		// Token: 0x04002E18 RID: 11800
		[__DynamicallyInvokable]
		public LIBFLAGS wLibFlags;
	}
}
