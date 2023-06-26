using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Prepares performance data for the performance DLL the system loads when working with performance counters.</summary>
	// Token: 0x020004D7 RID: 1239
	[Guid("73386977-D6FD-11D2-BED5-00C04F79E3AE")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ICollectData
	{
		/// <summary>Collects the performance data for the performance DLL.</summary>
		/// <param name="id">The call index.</param>
		/// <param name="valueName">A pointer to a Unicode string list with the requested object identifiers.</param>
		/// <param name="data">A pointer to the data buffer.</param>
		/// <param name="totalBytes">A pointer to a number of bytes.</param>
		/// <param name="res">When this method returns, contains a <see cref="T:System.IntPtr" /> to the first byte after the data, -1 for an error, or -2 if a larger buffer is required. This parameter is passed uninitialized.</param>
		// Token: 0x06002ECE RID: 11982
		[return: MarshalAs(UnmanagedType.I4)]
		void CollectData([MarshalAs(UnmanagedType.I4)] [In] int id, [MarshalAs(UnmanagedType.SysInt)] [In] IntPtr valueName, [MarshalAs(UnmanagedType.SysInt)] [In] IntPtr data, [MarshalAs(UnmanagedType.I4)] [In] int totalBytes, [MarshalAs(UnmanagedType.SysInt)] out IntPtr res);

		/// <summary>Called by the performance DLL's close performance data function.</summary>
		// Token: 0x06002ECF RID: 11983
		void CloseData();
	}
}
