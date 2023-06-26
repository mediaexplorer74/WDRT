using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEATTR" /> instead.</summary>
	// Token: 0x02000992 RID: 2450
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEATTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEATTR
	{
		/// <summary>A constant used with the <see cref="F:System.Runtime.InteropServices.TYPEATTR.memidConstructor" /> and <see cref="F:System.Runtime.InteropServices.TYPEATTR.memidDestructor" /> fields.</summary>
		// Token: 0x04002C25 RID: 11301
		public const int MEMBER_ID_NIL = -1;

		/// <summary>The GUID of the type information.</summary>
		// Token: 0x04002C26 RID: 11302
		public Guid guid;

		/// <summary>Locale of member names and documentation strings.</summary>
		// Token: 0x04002C27 RID: 11303
		public int lcid;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002C28 RID: 11304
		public int dwReserved;

		/// <summary>ID of constructor, or <see cref="F:System.Runtime.InteropServices.TYPEATTR.MEMBER_ID_NIL" /> if none.</summary>
		// Token: 0x04002C29 RID: 11305
		public int memidConstructor;

		/// <summary>ID of destructor, or <see cref="F:System.Runtime.InteropServices.TYPEATTR.MEMBER_ID_NIL" /> if none.</summary>
		// Token: 0x04002C2A RID: 11306
		public int memidDestructor;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002C2B RID: 11307
		public IntPtr lpstrSchema;

		/// <summary>The size of an instance of this type.</summary>
		// Token: 0x04002C2C RID: 11308
		public int cbSizeInstance;

		/// <summary>A <see cref="T:System.Runtime.InteropServices.TYPEKIND" /> value describing the type this information describes.</summary>
		// Token: 0x04002C2D RID: 11309
		public TYPEKIND typekind;

		/// <summary>Indicates the number of functions on the interface this structure describes.</summary>
		// Token: 0x04002C2E RID: 11310
		public short cFuncs;

		/// <summary>Indicates the number of variables and data fields on the interface described by this structure.</summary>
		// Token: 0x04002C2F RID: 11311
		public short cVars;

		/// <summary>Indicates the number of implemented interfaces on the interface this structure describes.</summary>
		// Token: 0x04002C30 RID: 11312
		public short cImplTypes;

		/// <summary>The size of this type's virtual method table (VTBL).</summary>
		// Token: 0x04002C31 RID: 11313
		public short cbSizeVft;

		/// <summary>Specifies the byte alignment for an instance of this type.</summary>
		// Token: 0x04002C32 RID: 11314
		public short cbAlignment;

		/// <summary>A <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> value describing this information.</summary>
		// Token: 0x04002C33 RID: 11315
		public TYPEFLAGS wTypeFlags;

		/// <summary>Major version number.</summary>
		// Token: 0x04002C34 RID: 11316
		public short wMajorVerNum;

		/// <summary>Minor version number.</summary>
		// Token: 0x04002C35 RID: 11317
		public short wMinorVerNum;

		/// <summary>If <see cref="F:System.Runtime.InteropServices.TYPEATTR.typekind" /> == <see cref="F:System.Runtime.InteropServices.TYPEKIND.TKIND_ALIAS" />, specifies the type for which this type is an alias.</summary>
		// Token: 0x04002C36 RID: 11318
		public TYPEDESC tdescAlias;

		/// <summary>IDL attributes of the described type.</summary>
		// Token: 0x04002C37 RID: 11319
		public IDLDESC idldescType;
	}
}
