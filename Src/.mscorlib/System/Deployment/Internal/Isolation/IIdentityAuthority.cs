using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000697 RID: 1687
	[Guid("261a6983-c35d-4d0d-aa5b-7867259e77bc")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IIdentityAuthority
	{
		// Token: 0x06004FB8 RID: 20408
		[SecurityCritical]
		IDefinitionIdentity TextToDefinition([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string Identity);

		// Token: 0x06004FB9 RID: 20409
		[SecurityCritical]
		IReferenceIdentity TextToReference([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string Identity);

		// Token: 0x06004FBA RID: 20410
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string DefinitionToText([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity);

		// Token: 0x06004FBB RID: 20411
		[SecurityCritical]
		uint DefinitionToTextBuffer([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity, [In] uint BufferSize, [MarshalAs(UnmanagedType.LPArray)] [Out] char[] Buffer);

		// Token: 0x06004FBC RID: 20412
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string ReferenceToText([In] uint Flags, [In] IReferenceIdentity ReferenceIdentity);

		// Token: 0x06004FBD RID: 20413
		[SecurityCritical]
		uint ReferenceToTextBuffer([In] uint Flags, [In] IReferenceIdentity ReferenceIdentity, [In] uint BufferSize, [MarshalAs(UnmanagedType.LPArray)] [Out] char[] Buffer);

		// Token: 0x06004FBE RID: 20414
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreDefinitionsEqual([In] uint Flags, [In] IDefinitionIdentity Definition1, [In] IDefinitionIdentity Definition2);

		// Token: 0x06004FBF RID: 20415
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreReferencesEqual([In] uint Flags, [In] IReferenceIdentity Reference1, [In] IReferenceIdentity Reference2);

		// Token: 0x06004FC0 RID: 20416
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreTextualDefinitionsEqual([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string IdentityLeft, [MarshalAs(UnmanagedType.LPWStr)] [In] string IdentityRight);

		// Token: 0x06004FC1 RID: 20417
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreTextualReferencesEqual([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string IdentityLeft, [MarshalAs(UnmanagedType.LPWStr)] [In] string IdentityRight);

		// Token: 0x06004FC2 RID: 20418
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool DoesDefinitionMatchReference([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity, [In] IReferenceIdentity ReferenceIdentity);

		// Token: 0x06004FC3 RID: 20419
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool DoesTextualDefinitionMatchTextualReference([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string Definition, [MarshalAs(UnmanagedType.LPWStr)] [In] string Reference);

		// Token: 0x06004FC4 RID: 20420
		[SecurityCritical]
		ulong HashReference([In] uint Flags, [In] IReferenceIdentity ReferenceIdentity);

		// Token: 0x06004FC5 RID: 20421
		[SecurityCritical]
		ulong HashDefinition([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity);

		// Token: 0x06004FC6 RID: 20422
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string GenerateDefinitionKey([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity);

		// Token: 0x06004FC7 RID: 20423
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string GenerateReferenceKey([In] uint Flags, [In] IReferenceIdentity ReferenceIdentity);

		// Token: 0x06004FC8 RID: 20424
		[SecurityCritical]
		IDefinitionIdentity CreateDefinition();

		// Token: 0x06004FC9 RID: 20425
		[SecurityCritical]
		IReferenceIdentity CreateReference();
	}
}
