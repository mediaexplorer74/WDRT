using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000DC RID: 220
	[StructLayout(LayoutKind.Sequential)]
	internal class SecurityDescriptorReferenceEntry
	{
		// Token: 0x04000384 RID: 900
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x04000385 RID: 901
		[MarshalAs(UnmanagedType.LPWStr)]
		public string BuildFilter;
	}
}
