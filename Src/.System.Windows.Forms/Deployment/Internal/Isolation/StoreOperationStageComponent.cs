using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200004F RID: 79
	internal struct StoreOperationStageComponent
	{
		// Token: 0x0600017E RID: 382 RVA: 0x000070A6 File Offset: 0x000052A6
		public void Destroy()
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000070A8 File Offset: 0x000052A8
		public StoreOperationStageComponent(IDefinitionAppId app, string Manifest)
		{
			this = new StoreOperationStageComponent(app, null, Manifest);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000070B3 File Offset: 0x000052B3
		[SecuritySafeCritical]
		public StoreOperationStageComponent(IDefinitionAppId app, IDefinitionIdentity comp, string Manifest)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationStageComponent));
			this.Flags = StoreOperationStageComponent.OpFlags.Nothing;
			this.Application = app;
			this.Component = comp;
			this.ManifestPath = Manifest;
		}

		// Token: 0x0400014B RID: 331
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400014C RID: 332
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationStageComponent.OpFlags Flags;

		// Token: 0x0400014D RID: 333
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400014E RID: 334
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionIdentity Component;

		// Token: 0x0400014F RID: 335
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ManifestPath;

		// Token: 0x0200051F RID: 1311
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003791 RID: 14225
			Nothing = 0
		}

		// Token: 0x02000520 RID: 1312
		public enum Disposition
		{
			// Token: 0x04003793 RID: 14227
			Failed,
			// Token: 0x04003794 RID: 14228
			Installed,
			// Token: 0x04003795 RID: 14229
			Refreshed,
			// Token: 0x04003796 RID: 14230
			AlreadyInstalled
		}
	}
}
