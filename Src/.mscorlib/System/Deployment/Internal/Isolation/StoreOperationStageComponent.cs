using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200069E RID: 1694
	internal struct StoreOperationStageComponent
	{
		// Token: 0x06004FDA RID: 20442 RVA: 0x0011DF54 File Offset: 0x0011C154
		public void Destroy()
		{
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x0011DF56 File Offset: 0x0011C156
		public StoreOperationStageComponent(IDefinitionAppId app, string Manifest)
		{
			this = new StoreOperationStageComponent(app, null, Manifest);
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x0011DF61 File Offset: 0x0011C161
		public StoreOperationStageComponent(IDefinitionAppId app, IDefinitionIdentity comp, string Manifest)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationStageComponent));
			this.Flags = StoreOperationStageComponent.OpFlags.Nothing;
			this.Application = app;
			this.Component = comp;
			this.ManifestPath = Manifest;
		}

		// Token: 0x0400222B RID: 8747
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400222C RID: 8748
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationStageComponent.OpFlags Flags;

		// Token: 0x0400222D RID: 8749
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400222E RID: 8750
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionIdentity Component;

		// Token: 0x0400222F RID: 8751
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ManifestPath;

		// Token: 0x02000C3E RID: 3134
		[Flags]
		public enum OpFlags
		{
			// Token: 0x0400375B RID: 14171
			Nothing = 0
		}

		// Token: 0x02000C3F RID: 3135
		public enum Disposition
		{
			// Token: 0x0400375D RID: 14173
			Failed,
			// Token: 0x0400375E RID: 14174
			Installed,
			// Token: 0x0400375F RID: 14175
			Refreshed,
			// Token: 0x04003760 RID: 14176
			AlreadyInstalled
		}
	}
}
