using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage
{
	// Token: 0x0200004D RID: 77
	public class FfuPackageFileInfo : PackageFileInfo
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000696C File Offset: 0x00004B6C
		public FfuPackageFileInfo(string path, PlatformId platformId, string softwareVersion)
			: base(path)
		{
			this.softwareVersion = softwareVersion;
			this.PlatformId = platformId;
			this.akVersion = null;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000698D File Offset: 0x00004B8D
		public FfuPackageFileInfo(string path, PlatformId platformId, string softwareVersion, string akVersion)
			: base(path)
		{
			this.softwareVersion = softwareVersion;
			this.PlatformId = platformId;
			this.akVersion = akVersion;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000069AF File Offset: 0x00004BAF
		// (set) Token: 0x0600026C RID: 620 RVA: 0x000069B7 File Offset: 0x00004BB7
		public PlatformId PlatformId { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000069C0 File Offset: 0x00004BC0
		public override string PackageId
		{
			get
			{
				return this.PlatformId.ToString();
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000069E0 File Offset: 0x00004BE0
		public override string Name
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000069F4 File Offset: 0x00004BF4
		public override string SoftwareVersion
		{
			get
			{
				return this.softwareVersion;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00006A0C File Offset: 0x00004C0C
		public override string AkVersion
		{
			get
			{
				return this.akVersion;
			}
		}

		// Token: 0x04000220 RID: 544
		private readonly string softwareVersion;

		// Token: 0x04000221 RID: 545
		private readonly string akVersion;
	}
}
