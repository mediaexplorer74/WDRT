using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000670 RID: 1648
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000100-0000-0000-C000-000000000046")]
	[ComImport]
	internal interface IEnumUnknown
	{
		// Token: 0x06004F3D RID: 20285
		[PreserveSig]
		int Next(uint celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown)] [Out] object[] rgelt, ref uint celtFetched);

		// Token: 0x06004F3E RID: 20286
		[PreserveSig]
		int Skip(uint celt);

		// Token: 0x06004F3F RID: 20287
		[PreserveSig]
		int Reset();

		// Token: 0x06004F40 RID: 20288
		[PreserveSig]
		int Clone(out IEnumUnknown enumUnknown);
	}
}
