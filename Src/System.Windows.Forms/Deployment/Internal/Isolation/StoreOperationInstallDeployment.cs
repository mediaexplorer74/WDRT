using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000054 RID: 84
	internal struct StoreOperationInstallDeployment
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00007259 File Offset: 0x00005459
		public StoreOperationInstallDeployment(IDefinitionAppId App, StoreApplicationReference reference)
		{
			this = new StoreOperationInstallDeployment(App, true, reference);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007264 File Offset: 0x00005464
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

		// Token: 0x0600018E RID: 398 RVA: 0x000072B2 File Offset: 0x000054B2
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04000164 RID: 356
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04000165 RID: 357
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationInstallDeployment.OpFlags Flags;

		// Token: 0x04000166 RID: 358
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04000167 RID: 359
		public IntPtr Reference;

		// Token: 0x02000528 RID: 1320
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037AC RID: 14252
			Nothing = 0,
			// Token: 0x040037AD RID: 14253
			UninstallOthers = 1
		}

		// Token: 0x02000529 RID: 1321
		public enum Disposition
		{
			// Token: 0x040037AF RID: 14255
			Failed,
			// Token: 0x040037B0 RID: 14256
			AlreadyInstalled,
			// Token: 0x040037B1 RID: 14257
			Installed
		}
	}
}
