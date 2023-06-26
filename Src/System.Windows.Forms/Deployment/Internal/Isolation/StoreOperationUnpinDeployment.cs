using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000053 RID: 83
	internal struct StoreOperationUnpinDeployment
	{
		// Token: 0x0600018A RID: 394 RVA: 0x0000721A File Offset: 0x0000541A
		[SecuritySafeCritical]
		public StoreOperationUnpinDeployment(IDefinitionAppId app, StoreApplicationReference reference)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationUnpinDeployment));
			this.Flags = StoreOperationUnpinDeployment.OpFlags.Nothing;
			this.Application = app;
			this.Reference = reference.ToIntPtr();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000724C File Offset: 0x0000544C
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04000160 RID: 352
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04000161 RID: 353
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationUnpinDeployment.OpFlags Flags;

		// Token: 0x04000162 RID: 354
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04000163 RID: 355
		public IntPtr Reference;

		// Token: 0x02000526 RID: 1318
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037A7 RID: 14247
			Nothing = 0
		}

		// Token: 0x02000527 RID: 1319
		public enum Disposition
		{
			// Token: 0x040037A9 RID: 14249
			Failed,
			// Token: 0x040037AA RID: 14250
			Unpinned
		}
	}
}
