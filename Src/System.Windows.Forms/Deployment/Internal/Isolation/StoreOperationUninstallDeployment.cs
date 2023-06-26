using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000055 RID: 85
	internal struct StoreOperationUninstallDeployment
	{
		// Token: 0x0600018F RID: 399 RVA: 0x000072BF File Offset: 0x000054BF
		[SecuritySafeCritical]
		public StoreOperationUninstallDeployment(IDefinitionAppId appid, StoreApplicationReference AppRef)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationUninstallDeployment));
			this.Flags = StoreOperationUninstallDeployment.OpFlags.Nothing;
			this.Application = appid;
			this.Reference = AppRef.ToIntPtr();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000072F1 File Offset: 0x000054F1
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04000168 RID: 360
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04000169 RID: 361
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationUninstallDeployment.OpFlags Flags;

		// Token: 0x0400016A RID: 362
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400016B RID: 363
		public IntPtr Reference;

		// Token: 0x0200052A RID: 1322
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037B3 RID: 14259
			Nothing = 0
		}

		// Token: 0x0200052B RID: 1323
		public enum Disposition
		{
			// Token: 0x040037B5 RID: 14261
			Failed,
			// Token: 0x040037B6 RID: 14262
			DidNotExist,
			// Token: 0x040037B7 RID: 14263
			Uninstalled
		}
	}
}
