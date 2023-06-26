using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200069F RID: 1695
	internal struct StoreOperationStageComponentFile
	{
		// Token: 0x06004FDD RID: 20445 RVA: 0x0011DF94 File Offset: 0x0011C194
		public StoreOperationStageComponentFile(IDefinitionAppId App, string CompRelPath, string SrcFile)
		{
			this = new StoreOperationStageComponentFile(App, null, CompRelPath, SrcFile);
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x0011DFA0 File Offset: 0x0011C1A0
		public StoreOperationStageComponentFile(IDefinitionAppId App, IDefinitionIdentity Component, string CompRelPath, string SrcFile)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationStageComponentFile));
			this.Flags = StoreOperationStageComponentFile.OpFlags.Nothing;
			this.Application = App;
			this.Component = Component;
			this.ComponentRelativePath = CompRelPath;
			this.SourceFilePath = SrcFile;
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x0011DFDB File Offset: 0x0011C1DB
		public void Destroy()
		{
		}

		// Token: 0x04002230 RID: 8752
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002231 RID: 8753
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationStageComponentFile.OpFlags Flags;

		// Token: 0x04002232 RID: 8754
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04002233 RID: 8755
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionIdentity Component;

		// Token: 0x04002234 RID: 8756
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ComponentRelativePath;

		// Token: 0x04002235 RID: 8757
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SourceFilePath;

		// Token: 0x02000C40 RID: 3136
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003762 RID: 14178
			Nothing = 0
		}

		// Token: 0x02000C41 RID: 3137
		public enum Disposition
		{
			// Token: 0x04003764 RID: 14180
			Failed,
			// Token: 0x04003765 RID: 14181
			Installed,
			// Token: 0x04003766 RID: 14182
			Refreshed,
			// Token: 0x04003767 RID: 14183
			AlreadyInstalled
		}
	}
}
