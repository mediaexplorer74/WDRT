using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006F9 RID: 1785
	[StructLayout(LayoutKind.Sequential)]
	internal class EntryPointEntry
	{
		// Token: 0x04002388 RID: 9096
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x04002389 RID: 9097
		[MarshalAs(UnmanagedType.LPWStr)]
		public string CommandLine_File;

		// Token: 0x0400238A RID: 9098
		[MarshalAs(UnmanagedType.LPWStr)]
		public string CommandLine_Parameters;

		// Token: 0x0400238B RID: 9099
		public IReferenceIdentity Identity;

		// Token: 0x0400238C RID: 9100
		public uint Flags;
	}
}
