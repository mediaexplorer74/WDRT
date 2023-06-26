using System;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
	// Token: 0x02000591 RID: 1425
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class WSTRBufferMarshaler
	{
		// Token: 0x060042ED RID: 17133 RVA: 0x000FAE70 File Offset: 0x000F9070
		internal static IntPtr ConvertToNative(string strManaged)
		{
			return IntPtr.Zero;
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x000FAE77 File Offset: 0x000F9077
		internal static string ConvertToManaged(IntPtr bstr)
		{
			return null;
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x000FAE7A File Offset: 0x000F907A
		internal static void ClearNative(IntPtr pNative)
		{
		}
	}
}
