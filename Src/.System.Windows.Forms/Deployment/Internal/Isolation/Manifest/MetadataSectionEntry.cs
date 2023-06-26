using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000C7 RID: 199
	[StructLayout(LayoutKind.Sequential)]
	internal class MetadataSectionEntry : IDisposable
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x00008A70 File Offset: 0x00006C70
		~MetadataSectionEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00008AA0 File Offset: 0x00006CA0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00008AAC File Offset: 0x00006CAC
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.ManifestHash != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.ManifestHash);
				this.ManifestHash = IntPtr.Zero;
			}
			if (this.MvidValue != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.MvidValue);
				this.MvidValue = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000312 RID: 786
		public uint SchemaVersion;

		// Token: 0x04000313 RID: 787
		public uint ManifestFlags;

		// Token: 0x04000314 RID: 788
		public uint UsagePatterns;

		// Token: 0x04000315 RID: 789
		public IDefinitionIdentity CdfIdentity;

		// Token: 0x04000316 RID: 790
		[MarshalAs(UnmanagedType.LPWStr)]
		public string LocalPath;

		// Token: 0x04000317 RID: 791
		public uint HashAlgorithm;

		// Token: 0x04000318 RID: 792
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ManifestHash;

		// Token: 0x04000319 RID: 793
		public uint ManifestHashSize;

		// Token: 0x0400031A RID: 794
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ContentType;

		// Token: 0x0400031B RID: 795
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeImageVersion;

		// Token: 0x0400031C RID: 796
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr MvidValue;

		// Token: 0x0400031D RID: 797
		public uint MvidValueSize;

		// Token: 0x0400031E RID: 798
		public DescriptionMetadataEntry DescriptionData;

		// Token: 0x0400031F RID: 799
		public DeploymentMetadataEntry DeploymentData;

		// Token: 0x04000320 RID: 800
		public DependentOSMetadataEntry DependentOSData;

		// Token: 0x04000321 RID: 801
		[MarshalAs(UnmanagedType.LPWStr)]
		public string defaultPermissionSetID;

		// Token: 0x04000322 RID: 802
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RequestedExecutionLevel;

		// Token: 0x04000323 RID: 803
		public bool RequestedExecutionLevelUIAccess;

		// Token: 0x04000324 RID: 804
		public IReferenceIdentity ResourceTypeResourcesDependency;

		// Token: 0x04000325 RID: 805
		public IReferenceIdentity ResourceTypeManifestResourcesDependency;

		// Token: 0x04000326 RID: 806
		[MarshalAs(UnmanagedType.LPWStr)]
		public string KeyInfoElement;

		// Token: 0x04000327 RID: 807
		public CompatibleFrameworksMetadataEntry CompatibleFrameworksData;
	}
}
