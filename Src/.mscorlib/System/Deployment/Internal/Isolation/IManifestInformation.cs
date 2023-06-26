using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006B3 RID: 1715
	[Guid("81c85208-fe61-4c15-b5bb-ff5ea66baad9")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IManifestInformation
	{
		// Token: 0x06005040 RID: 20544
		[SecurityCritical]
		void get_FullPath([MarshalAs(UnmanagedType.LPWStr)] out string FullPath);
	}
}
