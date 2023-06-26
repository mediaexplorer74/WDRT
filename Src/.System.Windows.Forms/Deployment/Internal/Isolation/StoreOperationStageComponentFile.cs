using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000050 RID: 80
	internal struct StoreOperationStageComponentFile
	{
		// Token: 0x06000181 RID: 385 RVA: 0x000070E6 File Offset: 0x000052E6
		public StoreOperationStageComponentFile(IDefinitionAppId App, string CompRelPath, string SrcFile)
		{
			this = new StoreOperationStageComponentFile(App, null, CompRelPath, SrcFile);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000070F2 File Offset: 0x000052F2
		[SecuritySafeCritical]
		public StoreOperationStageComponentFile(IDefinitionAppId App, IDefinitionIdentity Component, string CompRelPath, string SrcFile)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationStageComponentFile));
			this.Flags = StoreOperationStageComponentFile.OpFlags.Nothing;
			this.Application = App;
			this.Component = Component;
			this.ComponentRelativePath = CompRelPath;
			this.SourceFilePath = SrcFile;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000070A6 File Offset: 0x000052A6
		public void Destroy()
		{
		}

		// Token: 0x04000150 RID: 336
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04000151 RID: 337
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationStageComponentFile.OpFlags Flags;

		// Token: 0x04000152 RID: 338
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04000153 RID: 339
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionIdentity Component;

		// Token: 0x04000154 RID: 340
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ComponentRelativePath;

		// Token: 0x04000155 RID: 341
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SourceFilePath;

		// Token: 0x02000521 RID: 1313
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003798 RID: 14232
			Nothing = 0
		}

		// Token: 0x02000522 RID: 1314
		public enum Disposition
		{
			// Token: 0x0400379A RID: 14234
			Failed,
			// Token: 0x0400379B RID: 14235
			Installed,
			// Token: 0x0400379C RID: 14236
			Refreshed,
			// Token: 0x0400379D RID: 14237
			AlreadyInstalled
		}
	}
}
