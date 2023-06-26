using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains statistical information about an open storage, stream, or byte-array object.</summary>
	// Token: 0x02000A33 RID: 2611
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		/// <summary>Represents a pointer to a null-terminated string containing the name of the object described by this structure.</summary>
		// Token: 0x04002D55 RID: 11605
		[__DynamicallyInvokable]
		public string pwcsName;

		/// <summary>Indicates the type of storage object, which is one of the values from the <see langword="STGTY" /> enumeration.</summary>
		// Token: 0x04002D56 RID: 11606
		[__DynamicallyInvokable]
		public int type;

		/// <summary>Specifies the size, in bytes, of the stream or byte array.</summary>
		// Token: 0x04002D57 RID: 11607
		[__DynamicallyInvokable]
		public long cbSize;

		/// <summary>Indicates the last modification time for this storage, stream, or byte array.</summary>
		// Token: 0x04002D58 RID: 11608
		[__DynamicallyInvokable]
		public FILETIME mtime;

		/// <summary>Indicates the creation time for this storage, stream, or byte array.</summary>
		// Token: 0x04002D59 RID: 11609
		[__DynamicallyInvokable]
		public FILETIME ctime;

		/// <summary>Specifies the last access time for this storage, stream, or byte array.</summary>
		// Token: 0x04002D5A RID: 11610
		[__DynamicallyInvokable]
		public FILETIME atime;

		/// <summary>Indicates the access mode that was specified when the object was opened.</summary>
		// Token: 0x04002D5B RID: 11611
		[__DynamicallyInvokable]
		public int grfMode;

		/// <summary>Indicates the types of region locking supported by the stream or byte array.</summary>
		// Token: 0x04002D5C RID: 11612
		[__DynamicallyInvokable]
		public int grfLocksSupported;

		/// <summary>Indicates the class identifier for the storage object.</summary>
		// Token: 0x04002D5D RID: 11613
		[__DynamicallyInvokable]
		public Guid clsid;

		/// <summary>Indicates the current state bits of the storage object (the value most recently set by the <see langword="IStorage::SetStateBits" /> method).</summary>
		// Token: 0x04002D5E RID: 11614
		[__DynamicallyInvokable]
		public int grfStateBits;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002D5F RID: 11615
		[__DynamicallyInvokable]
		public int reserved;
	}
}
