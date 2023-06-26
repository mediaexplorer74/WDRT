using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the <see langword="TYMED" /> structure.</summary>
	// Token: 0x020003E7 RID: 999
	[Flags]
	[global::__DynamicallyInvokable]
	public enum TYMED
	{
		/// <summary>The storage medium is a global memory handle (HGLOBAL). Allocate the global handle with the GMEM_SHARE flag. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is <see langword="null" />, the destination process should use <see langword="GlobalFree" /> to release the memory.</summary>
		// Token: 0x0400208B RID: 8331
		[global::__DynamicallyInvokable]
		TYMED_HGLOBAL = 1,
		/// <summary>The storage medium is a disk file identified by a path. If the <see langword="STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is <see langword="null" />, the destination process should use <see langword="OpenFile" /> to delete the file.</summary>
		// Token: 0x0400208C RID: 8332
		[global::__DynamicallyInvokable]
		TYMED_FILE = 2,
		/// <summary>The storage medium is a stream object identified by an <see langword="IStream" /> pointer. Use <see langword="ISequentialStream::Read" /> to read the data. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is not <see langword="null" />, the destination process should use <see langword="IStream::Release" /> to release the stream component.</summary>
		// Token: 0x0400208D RID: 8333
		[global::__DynamicallyInvokable]
		TYMED_ISTREAM = 4,
		/// <summary>The storage medium is a storage component identified by an <see langword="IStorage" /> pointer. The data is in the streams and storages contained by this <see langword="IStorage" /> instance. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is not <see langword="null" />, the destination process should use <see langword="IStorage::Release" /> to release the storage component.</summary>
		// Token: 0x0400208E RID: 8334
		[global::__DynamicallyInvokable]
		TYMED_ISTORAGE = 8,
		/// <summary>The storage medium is a Graphics Device Interface (GDI) component (HBITMAP). If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is <see langword="null" />, the destination process should use <see langword="DeleteObject" /> to delete the bitmap.</summary>
		// Token: 0x0400208F RID: 8335
		[global::__DynamicallyInvokable]
		TYMED_GDI = 16,
		/// <summary>The storage medium is a metafile (HMETAFILE). Use the Windows or WIN32 functions to access the metafile's data. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is <see langword="null" />, the destination process should use <see langword="DeleteMetaFile" /> to delete the bitmap.</summary>
		// Token: 0x04002090 RID: 8336
		[global::__DynamicallyInvokable]
		TYMED_MFPICT = 32,
		/// <summary>The storage medium is an enhanced metafile. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is <see langword="null" />, the destination process should use <see langword="DeleteEnhMetaFile" /> to delete the bitmap.</summary>
		// Token: 0x04002091 RID: 8337
		[global::__DynamicallyInvokable]
		TYMED_ENHMF = 64,
		/// <summary>No data is being passed.</summary>
		// Token: 0x04002092 RID: 8338
		[global::__DynamicallyInvokable]
		TYMED_NULL = 0
	}
}
