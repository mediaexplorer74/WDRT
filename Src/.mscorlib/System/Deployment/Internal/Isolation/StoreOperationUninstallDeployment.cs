using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A4 RID: 1700
	internal struct StoreOperationUninstallDeployment
	{
		// Token: 0x06004FEB RID: 20459 RVA: 0x0011E167 File Offset: 0x0011C367
		[SecuritySafeCritical]
		public StoreOperationUninstallDeployment(IDefinitionAppId appid, StoreApplicationReference AppRef)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationUninstallDeployment));
			this.Flags = StoreOperationUninstallDeployment.OpFlags.Nothing;
			this.Application = appid;
			this.Reference = AppRef.ToIntPtr();
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x0011E199 File Offset: 0x0011C399
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04002248 RID: 8776
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002249 RID: 8777
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationUninstallDeployment.OpFlags Flags;

		// Token: 0x0400224A RID: 8778
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400224B RID: 8779
		public IntPtr Reference;

		// Token: 0x02000C49 RID: 3145
		[Flags]
		public enum OpFlags
		{
			// Token: 0x0400377D RID: 14205
			Nothing = 0
		}

		// Token: 0x02000C4A RID: 3146
		public enum Disposition
		{
			// Token: 0x0400377F RID: 14207
			Failed,
			// Token: 0x04003780 RID: 14208
			DidNotExist,
			// Token: 0x04003781 RID: 14209
			Uninstalled
		}
	}
}
