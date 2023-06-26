using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains attributes of a <see langword="UCOMITypeInfo" />.</summary>
	// Token: 0x02000A3B RID: 2619
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEATTR
	{
		/// <summary>A constant used with the <see cref="F:System.Runtime.InteropServices.TYPEATTR.memidConstructor" /> and <see cref="F:System.Runtime.InteropServices.TYPEATTR.memidDestructor" /> fields.</summary>
		// Token: 0x04002D89 RID: 11657
		[__DynamicallyInvokable]
		public const int MEMBER_ID_NIL = -1;

		/// <summary>The GUID of the type information.</summary>
		// Token: 0x04002D8A RID: 11658
		[__DynamicallyInvokable]
		public Guid guid;

		/// <summary>Locale of member names and documentation strings.</summary>
		// Token: 0x04002D8B RID: 11659
		[__DynamicallyInvokable]
		public int lcid;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002D8C RID: 11660
		[__DynamicallyInvokable]
		public int dwReserved;

		/// <summary>ID of constructor, or <see cref="F:System.Runtime.InteropServices.TYPEATTR.MEMBER_ID_NIL" /> if none.</summary>
		// Token: 0x04002D8D RID: 11661
		[__DynamicallyInvokable]
		public int memidConstructor;

		/// <summary>ID of destructor, or <see cref="F:System.Runtime.InteropServices.TYPEATTR.MEMBER_ID_NIL" /> if none.</summary>
		// Token: 0x04002D8E RID: 11662
		[__DynamicallyInvokable]
		public int memidDestructor;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002D8F RID: 11663
		public IntPtr lpstrSchema;

		/// <summary>The size of an instance of this type.</summary>
		// Token: 0x04002D90 RID: 11664
		[__DynamicallyInvokable]
		public int cbSizeInstance;

		/// <summary>A <see cref="T:System.Runtime.InteropServices.TYPEKIND" /> value describing the type this information describes.</summary>
		// Token: 0x04002D91 RID: 11665
		[__DynamicallyInvokable]
		public TYPEKIND typekind;

		/// <summary>Indicates the number of functions on the interface this structure describes.</summary>
		// Token: 0x04002D92 RID: 11666
		[__DynamicallyInvokable]
		public short cFuncs;

		/// <summary>Indicates the number of variables and data fields on the interface described by this structure.</summary>
		// Token: 0x04002D93 RID: 11667
		[__DynamicallyInvokable]
		public short cVars;

		/// <summary>Indicates the number of implemented interfaces on the interface this structure describes.</summary>
		// Token: 0x04002D94 RID: 11668
		[__DynamicallyInvokable]
		public short cImplTypes;

		/// <summary>The size of this type's virtual method table (VTBL).</summary>
		// Token: 0x04002D95 RID: 11669
		[__DynamicallyInvokable]
		public short cbSizeVft;

		/// <summary>Specifies the byte alignment for an instance of this type.</summary>
		// Token: 0x04002D96 RID: 11670
		[__DynamicallyInvokable]
		public short cbAlignment;

		/// <summary>A <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> value describing this information.</summary>
		// Token: 0x04002D97 RID: 11671
		[__DynamicallyInvokable]
		public TYPEFLAGS wTypeFlags;

		/// <summary>Major version number.</summary>
		// Token: 0x04002D98 RID: 11672
		[__DynamicallyInvokable]
		public short wMajorVerNum;

		/// <summary>Minor version number.</summary>
		// Token: 0x04002D99 RID: 11673
		[__DynamicallyInvokable]
		public short wMinorVerNum;

		/// <summary>If <see cref="F:System.Runtime.InteropServices.TYPEATTR.typekind" /> == <see cref="F:System.Runtime.InteropServices.TYPEKIND.TKIND_ALIAS" />, specifies the type for which this type is an alias.</summary>
		// Token: 0x04002D9A RID: 11674
		[__DynamicallyInvokable]
		public TYPEDESC tdescAlias;

		/// <summary>IDL attributes of the described type.</summary>
		// Token: 0x04002D9B RID: 11675
		[__DynamicallyInvokable]
		public IDLDESC idldescType;
	}
}
