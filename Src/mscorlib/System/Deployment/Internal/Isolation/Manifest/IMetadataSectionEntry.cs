using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000710 RID: 1808
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("AB1ED79F-943E-407d-A80B-0744E3A95B28")]
	[ComImport]
	internal interface IMetadataSectionEntry
	{
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06005115 RID: 20757
		MetadataSectionEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06005116 RID: 20758
		uint SchemaVersion
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06005117 RID: 20759
		uint ManifestFlags
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06005118 RID: 20760
		uint UsagePatterns
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06005119 RID: 20761
		IDefinitionIdentity CdfIdentity
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x0600511A RID: 20762
		string LocalPath
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x0600511B RID: 20763
		uint HashAlgorithm
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x0600511C RID: 20764
		object ManifestHash
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x0600511D RID: 20765
		string ContentType
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x0600511E RID: 20766
		string RuntimeImageVersion
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600511F RID: 20767
		object MvidValue
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06005120 RID: 20768
		IDescriptionMetadataEntry DescriptionData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06005121 RID: 20769
		IDeploymentMetadataEntry DeploymentData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06005122 RID: 20770
		IDependentOSMetadataEntry DependentOSData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06005123 RID: 20771
		string defaultPermissionSetID
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06005124 RID: 20772
		string RequestedExecutionLevel
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06005125 RID: 20773
		bool RequestedExecutionLevelUIAccess
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06005126 RID: 20774
		IReferenceIdentity ResourceTypeResourcesDependency
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06005127 RID: 20775
		IReferenceIdentity ResourceTypeManifestResourcesDependency
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06005128 RID: 20776
		string KeyInfoElement
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06005129 RID: 20777
		ICompatibleFrameworksMetadataEntry CompatibleFrameworksData
		{
			[SecurityCritical]
			get;
		}
	}
}
