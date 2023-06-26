using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200069A RID: 1690
	[Guid("8c87810c-2541-4f75-b2d0-9af515488e23")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IAppIdAuthority
	{
		// Token: 0x06004FCA RID: 20426
		[SecurityCritical]
		IDefinitionAppId TextToDefinition([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string Identity);

		// Token: 0x06004FCB RID: 20427
		[SecurityCritical]
		IReferenceAppId TextToReference([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string Identity);

		// Token: 0x06004FCC RID: 20428
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string DefinitionToText([In] uint Flags, [In] IDefinitionAppId DefinitionAppId);

		// Token: 0x06004FCD RID: 20429
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string ReferenceToText([In] uint Flags, [In] IReferenceAppId ReferenceAppId);

		// Token: 0x06004FCE RID: 20430
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreDefinitionsEqual([In] uint Flags, [In] IDefinitionAppId Definition1, [In] IDefinitionAppId Definition2);

		// Token: 0x06004FCF RID: 20431
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreReferencesEqual([In] uint Flags, [In] IReferenceAppId Reference1, [In] IReferenceAppId Reference2);

		// Token: 0x06004FD0 RID: 20432
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreTextualDefinitionsEqual([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string AppIdLeft, [MarshalAs(UnmanagedType.LPWStr)] [In] string AppIdRight);

		// Token: 0x06004FD1 RID: 20433
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreTextualReferencesEqual([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string AppIdLeft, [MarshalAs(UnmanagedType.LPWStr)] [In] string AppIdRight);

		// Token: 0x06004FD2 RID: 20434
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool DoesDefinitionMatchReference([In] uint Flags, [In] IDefinitionAppId DefinitionIdentity, [In] IReferenceAppId ReferenceIdentity);

		// Token: 0x06004FD3 RID: 20435
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool DoesTextualDefinitionMatchTextualReference([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string Definition, [MarshalAs(UnmanagedType.LPWStr)] [In] string Reference);

		// Token: 0x06004FD4 RID: 20436
		[SecurityCritical]
		ulong HashReference([In] uint Flags, [In] IReferenceAppId ReferenceIdentity);

		// Token: 0x06004FD5 RID: 20437
		[SecurityCritical]
		ulong HashDefinition([In] uint Flags, [In] IDefinitionAppId DefinitionIdentity);

		// Token: 0x06004FD6 RID: 20438
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string GenerateDefinitionKey([In] uint Flags, [In] IDefinitionAppId DefinitionIdentity);

		// Token: 0x06004FD7 RID: 20439
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string GenerateReferenceKey([In] uint Flags, [In] IReferenceAppId ReferenceIdentity);

		// Token: 0x06004FD8 RID: 20440
		[SecurityCritical]
		IDefinitionAppId CreateDefinition();

		// Token: 0x06004FD9 RID: 20441
		[SecurityCritical]
		IReferenceAppId CreateReference();
	}
}
