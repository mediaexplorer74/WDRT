using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006EA RID: 1770
	[StructLayout(LayoutKind.Sequential)]
	internal class CLRSurrogateEntry
	{
		// Token: 0x0400235B RID: 9051
		public Guid Clsid;

		// Token: 0x0400235C RID: 9052
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeVersion;

		// Token: 0x0400235D RID: 9053
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ClassName;
	}
}
