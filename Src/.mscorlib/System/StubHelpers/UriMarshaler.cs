using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x02000599 RID: 1433
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class UriMarshaler
	{
		// Token: 0x06004302 RID: 17154
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetRawUriFromNative(IntPtr pUri);

		// Token: 0x06004303 RID: 17155
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr CreateNativeUriInstanceHelper(char* rawUri, int strLen);

		// Token: 0x06004304 RID: 17156 RVA: 0x000FAFBC File Offset: 0x000F91BC
		[SecurityCritical]
		internal unsafe static IntPtr CreateNativeUriInstance(string rawUri)
		{
			char* ptr = rawUri;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UriMarshaler.CreateNativeUriInstanceHelper(ptr, rawUri.Length);
		}
	}
}
