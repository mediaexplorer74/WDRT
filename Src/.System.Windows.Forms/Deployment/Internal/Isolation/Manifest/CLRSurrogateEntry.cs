using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000A3 RID: 163
	[StructLayout(LayoutKind.Sequential)]
	internal class CLRSurrogateEntry
	{
		// Token: 0x040002A9 RID: 681
		public Guid Clsid;

		// Token: 0x040002AA RID: 682
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeVersion;

		// Token: 0x040002AB RID: 683
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ClassName;
	}
}
