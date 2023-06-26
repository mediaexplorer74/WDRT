using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000AB RID: 171
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("FD47B733-AFBC-45e4-B7C2-BBEB1D9F766C")]
	[ComImport]
	internal interface IAssemblyReferenceEntry
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000292 RID: 658
		AssemblyReferenceEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000293 RID: 659
		IReferenceIdentity ReferenceIdentity
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000294 RID: 660
		uint Flags
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000295 RID: 661
		IAssemblyReferenceDependentAssemblyEntry DependentAssembly
		{
			[SecurityCritical]
			get;
		}
	}
}
