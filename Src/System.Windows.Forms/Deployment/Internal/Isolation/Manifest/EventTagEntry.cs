using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000D0 RID: 208
	[StructLayout(LayoutKind.Sequential)]
	internal class EventTagEntry
	{
		// Token: 0x04000357 RID: 855
		[MarshalAs(UnmanagedType.LPWStr)]
		public string TagData;

		// Token: 0x04000358 RID: 856
		public uint EventID;
	}
}
