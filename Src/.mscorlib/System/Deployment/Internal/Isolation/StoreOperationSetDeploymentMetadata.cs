using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A6 RID: 1702
	internal struct StoreOperationSetDeploymentMetadata
	{
		// Token: 0x06004FEF RID: 20463 RVA: 0x0011E1E7 File Offset: 0x0011C3E7
		public StoreOperationSetDeploymentMetadata(IDefinitionAppId Deployment, StoreApplicationReference Reference, StoreOperationMetadataProperty[] SetProperties)
		{
			this = new StoreOperationSetDeploymentMetadata(Deployment, Reference, SetProperties, null);
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x0011E1F4 File Offset: 0x0011C3F4
		[SecuritySafeCritical]
		public StoreOperationSetDeploymentMetadata(IDefinitionAppId Deployment, StoreApplicationReference Reference, StoreOperationMetadataProperty[] SetProperties, StoreOperationMetadataProperty[] TestProperties)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationSetDeploymentMetadata));
			this.Flags = StoreOperationSetDeploymentMetadata.OpFlags.Nothing;
			this.Deployment = Deployment;
			if (SetProperties != null)
			{
				this.PropertiesToSet = StoreOperationSetDeploymentMetadata.MarshalProperties(SetProperties);
				this.cPropertiesToSet = new IntPtr(SetProperties.Length);
			}
			else
			{
				this.PropertiesToSet = IntPtr.Zero;
				this.cPropertiesToSet = IntPtr.Zero;
			}
			if (TestProperties != null)
			{
				this.PropertiesToTest = StoreOperationSetDeploymentMetadata.MarshalProperties(TestProperties);
				this.cPropertiesToTest = new IntPtr(TestProperties.Length);
			}
			else
			{
				this.PropertiesToTest = IntPtr.Zero;
				this.cPropertiesToTest = IntPtr.Zero;
			}
			this.InstallerReference = Reference.ToIntPtr();
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x0011E2A0 File Offset: 0x0011C4A0
		[SecurityCritical]
		public void Destroy()
		{
			if (this.PropertiesToSet != IntPtr.Zero)
			{
				StoreOperationSetDeploymentMetadata.DestroyProperties(this.PropertiesToSet, (ulong)this.cPropertiesToSet.ToInt64());
				this.PropertiesToSet = IntPtr.Zero;
				this.cPropertiesToSet = IntPtr.Zero;
			}
			if (this.PropertiesToTest != IntPtr.Zero)
			{
				StoreOperationSetDeploymentMetadata.DestroyProperties(this.PropertiesToTest, (ulong)this.cPropertiesToTest.ToInt64());
				this.PropertiesToTest = IntPtr.Zero;
				this.cPropertiesToTest = IntPtr.Zero;
			}
			if (this.InstallerReference != IntPtr.Zero)
			{
				StoreApplicationReference.Destroy(this.InstallerReference);
				this.InstallerReference = IntPtr.Zero;
			}
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0011E354 File Offset: 0x0011C554
		[SecurityCritical]
		private static void DestroyProperties(IntPtr rgItems, ulong iItems)
		{
			if (rgItems != IntPtr.Zero)
			{
				ulong num = (ulong)((long)Marshal.SizeOf(typeof(StoreOperationMetadataProperty)));
				for (ulong num2 = 0UL; num2 < iItems; num2 += 1UL)
				{
					Marshal.DestroyStructure(new IntPtr((long)(num2 * num + (ulong)rgItems.ToInt64())), typeof(StoreOperationMetadataProperty));
				}
				Marshal.FreeCoTaskMem(rgItems);
			}
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x0011E3B4 File Offset: 0x0011C5B4
		[SecurityCritical]
		private static IntPtr MarshalProperties(StoreOperationMetadataProperty[] Props)
		{
			if (Props == null || Props.Length == 0)
			{
				return IntPtr.Zero;
			}
			int num = Marshal.SizeOf(typeof(StoreOperationMetadataProperty));
			IntPtr intPtr = Marshal.AllocCoTaskMem(num * Props.Length);
			for (int num2 = 0; num2 != Props.Length; num2++)
			{
				Marshal.StructureToPtr<StoreOperationMetadataProperty>(Props[num2], new IntPtr((long)(num2 * num) + intPtr.ToInt64()), false);
			}
			return intPtr;
		}

		// Token: 0x04002250 RID: 8784
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002251 RID: 8785
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationSetDeploymentMetadata.OpFlags Flags;

		// Token: 0x04002252 RID: 8786
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Deployment;

		// Token: 0x04002253 RID: 8787
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr InstallerReference;

		// Token: 0x04002254 RID: 8788
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr cPropertiesToTest;

		// Token: 0x04002255 RID: 8789
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr PropertiesToTest;

		// Token: 0x04002256 RID: 8790
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr cPropertiesToSet;

		// Token: 0x04002257 RID: 8791
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr PropertiesToSet;

		// Token: 0x02000C4B RID: 3147
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003783 RID: 14211
			Nothing = 0
		}

		// Token: 0x02000C4C RID: 3148
		public enum Disposition
		{
			// Token: 0x04003785 RID: 14213
			Failed,
			// Token: 0x04003786 RID: 14214
			Set = 2
		}
	}
}
