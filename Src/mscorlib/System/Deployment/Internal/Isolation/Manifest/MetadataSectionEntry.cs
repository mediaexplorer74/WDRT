using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200070E RID: 1806
	[StructLayout(LayoutKind.Sequential)]
	internal class MetadataSectionEntry : IDisposable
	{
		// Token: 0x06005111 RID: 20753 RVA: 0x0011F59C File Offset: 0x0011D79C
		~MetadataSectionEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x0011F5CC File Offset: 0x0011D7CC
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x0011F5D8 File Offset: 0x0011D7D8
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

		// Token: 0x040023C4 RID: 9156
		public uint SchemaVersion;

		// Token: 0x040023C5 RID: 9157
		public uint ManifestFlags;

		// Token: 0x040023C6 RID: 9158
		public uint UsagePatterns;

		// Token: 0x040023C7 RID: 9159
		public IDefinitionIdentity CdfIdentity;

		// Token: 0x040023C8 RID: 9160
		[MarshalAs(UnmanagedType.LPWStr)]
		public string LocalPath;

		// Token: 0x040023C9 RID: 9161
		public uint HashAlgorithm;

		// Token: 0x040023CA RID: 9162
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ManifestHash;

		// Token: 0x040023CB RID: 9163
		public uint ManifestHashSize;

		// Token: 0x040023CC RID: 9164
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ContentType;

		// Token: 0x040023CD RID: 9165
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeImageVersion;

		// Token: 0x040023CE RID: 9166
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr MvidValue;

		// Token: 0x040023CF RID: 9167
		public uint MvidValueSize;

		// Token: 0x040023D0 RID: 9168
		public DescriptionMetadataEntry DescriptionData;

		// Token: 0x040023D1 RID: 9169
		public DeploymentMetadataEntry DeploymentData;

		// Token: 0x040023D2 RID: 9170
		public DependentOSMetadataEntry DependentOSData;

		// Token: 0x040023D3 RID: 9171
		[MarshalAs(UnmanagedType.LPWStr)]
		public string defaultPermissionSetID;

		// Token: 0x040023D4 RID: 9172
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RequestedExecutionLevel;

		// Token: 0x040023D5 RID: 9173
		public bool RequestedExecutionLevelUIAccess;

		// Token: 0x040023D6 RID: 9174
		public IReferenceIdentity ResourceTypeResourcesDependency;

		// Token: 0x040023D7 RID: 9175
		public IReferenceIdentity ResourceTypeManifestResourcesDependency;

		// Token: 0x040023D8 RID: 9176
		[MarshalAs(UnmanagedType.LPWStr)]
		public string KeyInfoElement;

		// Token: 0x040023D9 RID: 9177
		public CompatibleFrameworksMetadataEntry CompatibleFrameworksData;
	}
}
