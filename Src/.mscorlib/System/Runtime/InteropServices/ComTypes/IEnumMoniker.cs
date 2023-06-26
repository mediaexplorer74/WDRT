using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Manages the definition of the <see langword="IEnumMoniker" /> interface.</summary>
	// Token: 0x02000A27 RID: 2599
	[Guid("00000102-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IEnumMoniker
	{
		/// <summary>Retrieves a specified number of items in the enumeration sequence.</summary>
		/// <param name="celt">The number of monikers to return in <paramref name="rgelt" />.</param>
		/// <param name="rgelt">When this method returns, contains a reference to the enumerated monikers. This parameter is passed uninitialized.</param>
		/// <param name="pceltFetched">When this method returns, contains a reference to the actual number of monikers enumerated in <paramref name="rgelt" />.</param>
		/// <returns>
		///   <see langword="S_OK" /> if the <paramref name="pceltFetched" /> parameter equals the <paramref name="celt" /> parameter; otherwise, <see langword="S_FALSE" />.</returns>
		// Token: 0x06006636 RID: 26166
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] IMoniker[] rgelt, IntPtr pceltFetched);

		/// <summary>Skips a specified number of items in the enumeration sequence.</summary>
		/// <param name="celt">The number of elements to skip in the enumeration.</param>
		/// <returns>
		///   <see langword="S_OK" /> if the number of elements skipped equals the <paramref name="celt" /> parameter; otherwise, <see langword="S_FALSE" />.</returns>
		// Token: 0x06006637 RID: 26167
		[__DynamicallyInvokable]
		[PreserveSig]
		int Skip(int celt);

		/// <summary>Resets the enumeration sequence to the beginning.</summary>
		// Token: 0x06006638 RID: 26168
		[__DynamicallyInvokable]
		void Reset();

		/// <summary>Creates a new enumerator that contains the same enumeration state as the current one.</summary>
		/// <param name="ppenum">When this method returns, contains a reference to the newly created enumerator. This parameter is passed uninitialized.</param>
		// Token: 0x06006639 RID: 26169
		[__DynamicallyInvokable]
		void Clone(out IEnumMoniker ppenum);
	}
}
