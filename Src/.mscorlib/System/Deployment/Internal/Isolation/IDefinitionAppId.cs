using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000692 RID: 1682
	[Guid("d91e12d8-98ed-47fa-9936-39421283d59b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IDefinitionAppId
	{
		// Token: 0x06004FAD RID: 20397
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string get_SubscriptionId();

		// Token: 0x06004FAE RID: 20398
		void put_SubscriptionId([MarshalAs(UnmanagedType.LPWStr)] [In] string Subscription);

		// Token: 0x06004FAF RID: 20399
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string get_Codebase();

		// Token: 0x06004FB0 RID: 20400
		[SecurityCritical]
		void put_Codebase([MarshalAs(UnmanagedType.LPWStr)] [In] string CodeBase);

		// Token: 0x06004FB1 RID: 20401
		[SecurityCritical]
		IEnumDefinitionIdentity EnumAppPath();

		// Token: 0x06004FB2 RID: 20402
		[SecurityCritical]
		void SetAppPath([In] uint cIDefinitionIdentity, [MarshalAs(UnmanagedType.LPArray)] [In] IDefinitionIdentity[] DefinitionIdentity);
	}
}
