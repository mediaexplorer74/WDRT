using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000D3 RID: 211
	[StructLayout(LayoutKind.Sequential)]
	internal class RegistryValueEntry
	{
		// Token: 0x0400035B RID: 859
		public uint Flags;

		// Token: 0x0400035C RID: 860
		public uint OperationHint;

		// Token: 0x0400035D RID: 861
		public uint Type;

		// Token: 0x0400035E RID: 862
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Value;

		// Token: 0x0400035F RID: 863
		[MarshalAs(UnmanagedType.LPWStr)]
		public string BuildFilter;
	}
}
