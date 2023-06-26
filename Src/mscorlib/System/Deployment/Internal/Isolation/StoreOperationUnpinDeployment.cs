using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A2 RID: 1698
	internal struct StoreOperationUnpinDeployment
	{
		// Token: 0x06004FE6 RID: 20454 RVA: 0x0011E0C0 File Offset: 0x0011C2C0
		[SecuritySafeCritical]
		public StoreOperationUnpinDeployment(IDefinitionAppId app, StoreApplicationReference reference)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationUnpinDeployment));
			this.Flags = StoreOperationUnpinDeployment.OpFlags.Nothing;
			this.Application = app;
			this.Reference = reference.ToIntPtr();
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x0011E0F2 File Offset: 0x0011C2F2
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04002240 RID: 8768
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002241 RID: 8769
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationUnpinDeployment.OpFlags Flags;

		// Token: 0x04002242 RID: 8770
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04002243 RID: 8771
		public IntPtr Reference;

		// Token: 0x02000C45 RID: 3141
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003771 RID: 14193
			Nothing = 0
		}

		// Token: 0x02000C46 RID: 3142
		public enum Disposition
		{
			// Token: 0x04003773 RID: 14195
			Failed,
			// Token: 0x04003774 RID: 14196
			Unpinned
		}
	}
}
