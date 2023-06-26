using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000CD RID: 205
	[StructLayout(LayoutKind.Sequential)]
	internal class EventMapEntry
	{
		// Token: 0x0400034F RID: 847
		[MarshalAs(UnmanagedType.LPWStr)]
		public string MapName;

		// Token: 0x04000350 RID: 848
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x04000351 RID: 849
		public uint Value;

		// Token: 0x04000352 RID: 850
		public bool IsValueMap;
	}
}
