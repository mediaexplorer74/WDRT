using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A3 RID: 1699
	internal struct StoreOperationInstallDeployment
	{
		// Token: 0x06004FE8 RID: 20456 RVA: 0x0011E0FF File Offset: 0x0011C2FF
		public StoreOperationInstallDeployment(IDefinitionAppId App, StoreApplicationReference reference)
		{
			this = new StoreOperationInstallDeployment(App, true, reference);
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x0011E10C File Offset: 0x0011C30C
		[SecuritySafeCritical]
		public StoreOperationInstallDeployment(IDefinitionAppId App, bool UninstallOthers, StoreApplicationReference reference)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationInstallDeployment));
			this.Flags = StoreOperationInstallDeployment.OpFlags.Nothing;
			this.Application = App;
			if (UninstallOthers)
			{
				this.Flags |= StoreOperationInstallDeployment.OpFlags.UninstallOthers;
			}
			this.Reference = reference.ToIntPtr();
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x0011E15A File Offset: 0x0011C35A
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04002244 RID: 8772
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002245 RID: 8773
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationInstallDeployment.OpFlags Flags;

		// Token: 0x04002246 RID: 8774
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04002247 RID: 8775
		public IntPtr Reference;

		// Token: 0x02000C47 RID: 3143
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003776 RID: 14198
			Nothing = 0,
			// Token: 0x04003777 RID: 14199
			UninstallOthers = 1
		}

		// Token: 0x02000C48 RID: 3144
		public enum Disposition
		{
			// Token: 0x04003779 RID: 14201
			Failed,
			// Token: 0x0400377A RID: 14202
			AlreadyInstalled,
			// Token: 0x0400377B RID: 14203
			Installed
		}
	}
}
