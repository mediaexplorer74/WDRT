using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage
{
	// Token: 0x0200004E RID: 78
	public abstract class PackageFileInfo
	{
		// Token: 0x06000271 RID: 625 RVA: 0x00006A24 File Offset: 0x00004C24
		protected PackageFileInfo(string path)
		{
			this.Path = path;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00006A36 File Offset: 0x00004C36
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00006A3E File Offset: 0x00004C3E
		public string Path { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00006A47 File Offset: 0x00004C47
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00006A4F File Offset: 0x00004C4F
		public string SalesName { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00006A58 File Offset: 0x00004C58
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00006A60 File Offset: 0x00004C60
		public string ManufacturerModelName { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00006A69 File Offset: 0x00004C69
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00006A71 File Offset: 0x00004C71
		public bool OfflinePackage { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600027A RID: 634
		public abstract string PackageId { get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600027B RID: 635
		public abstract string Name { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600027C RID: 636
		public abstract string SoftwareVersion { get; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600027D RID: 637
		public abstract string AkVersion { get; }
	}
}
