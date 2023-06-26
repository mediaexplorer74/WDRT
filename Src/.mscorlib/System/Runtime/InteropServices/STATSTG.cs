using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.STATSTG" /> instead.</summary>
	// Token: 0x0200098A RID: 2442
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.STATSTG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		/// <summary>Pointer to a null-terminated string containing the name of the object described by this structure.</summary>
		// Token: 0x04002BF1 RID: 11249
		public string pwcsName;

		/// <summary>Indicates the type of storage object which is one of the values from the <see langword="STGTY" /> enumeration.</summary>
		// Token: 0x04002BF2 RID: 11250
		public int type;

		/// <summary>Specifies the size in bytes of the stream or byte array.</summary>
		// Token: 0x04002BF3 RID: 11251
		public long cbSize;

		/// <summary>Indicates the last modification time for this storage, stream, or byte array.</summary>
		// Token: 0x04002BF4 RID: 11252
		public FILETIME mtime;

		/// <summary>Indicates the creation time for this storage, stream, or byte array.</summary>
		// Token: 0x04002BF5 RID: 11253
		public FILETIME ctime;

		/// <summary>Indicates the last access time for this storage, stream or byte array.</summary>
		// Token: 0x04002BF6 RID: 11254
		public FILETIME atime;

		/// <summary>Indicates the access mode that was specified when the object was opened.</summary>
		// Token: 0x04002BF7 RID: 11255
		public int grfMode;

		/// <summary>Indicates the types of region locking supported by the stream or byte array.</summary>
		// Token: 0x04002BF8 RID: 11256
		public int grfLocksSupported;

		/// <summary>Indicates the class identifier for the storage object.</summary>
		// Token: 0x04002BF9 RID: 11257
		public Guid clsid;

		/// <summary>Indicates the current state bits of the storage object (the value most recently set by the <see langword="IStorage::SetStateBits" /> method).</summary>
		// Token: 0x04002BFA RID: 11258
		public int grfStateBits;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002BFB RID: 11259
		public int reserved;
	}
}
