using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068E RID: 1678
	[Guid("587bf538-4d90-4a3c-9ef1-58a200a8a9e7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IDefinitionIdentity
	{
		// Token: 0x06004F9C RID: 20380
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string GetAttribute([MarshalAs(UnmanagedType.LPWStr)] [In] string Namespace, [MarshalAs(UnmanagedType.LPWStr)] [In] string Name);

		// Token: 0x06004F9D RID: 20381
		[SecurityCritical]
		void SetAttribute([MarshalAs(UnmanagedType.LPWStr)] [In] string Namespace, [MarshalAs(UnmanagedType.LPWStr)] [In] string Name, [MarshalAs(UnmanagedType.LPWStr)] [In] string Value);

		// Token: 0x06004F9E RID: 20382
		[SecurityCritical]
		IEnumIDENTITY_ATTRIBUTE EnumAttributes();

		// Token: 0x06004F9F RID: 20383
		[SecurityCritical]
		IDefinitionIdentity Clone([In] IntPtr cDeltas, [MarshalAs(UnmanagedType.LPArray)] [In] IDENTITY_ATTRIBUTE[] Deltas);
	}
}
