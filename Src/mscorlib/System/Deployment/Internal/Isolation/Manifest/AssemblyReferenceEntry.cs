using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006F0 RID: 1776
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyReferenceEntry
	{
		// Token: 0x04002378 RID: 9080
		public IReferenceIdentity ReferenceIdentity;

		// Token: 0x04002379 RID: 9081
		public uint Flags;

		// Token: 0x0400237A RID: 9082
		public AssemblyReferenceDependentAssemblyEntry DependentAssembly;
	}
}
