using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000A9 RID: 169
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyReferenceEntry
	{
		// Token: 0x040002C6 RID: 710
		public IReferenceIdentity ReferenceIdentity;

		// Token: 0x040002C7 RID: 711
		public uint Flags;

		// Token: 0x040002C8 RID: 712
		public AssemblyReferenceDependentAssemblyEntry DependentAssembly;
	}
}
