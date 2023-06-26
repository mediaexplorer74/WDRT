using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage
{
	// Token: 0x02000051 RID: 81
	public class VplPackageFileInfo : PackageFileInfo
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000710C File Offset: 0x0000530C
		public VplPackageFileInfo(string path, VariantInfo variantInfo)
			: base(path)
		{
			base.Path = path;
			this.packageId = variantInfo.ProductCode;
			this.name = variantInfo.Name;
			this.softwareVersion = variantInfo.SoftwareVersion;
			this.akVersion = variantInfo.AkVersion;
			this.FfuFilePath = variantInfo.FfuFilePath;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00007168 File Offset: 0x00005368
		public override string PackageId
		{
			get
			{
				return this.packageId;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00007180 File Offset: 0x00005380
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00007198 File Offset: 0x00005398
		public override string SoftwareVersion
		{
			get
			{
				return this.softwareVersion;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000071B0 File Offset: 0x000053B0
		public override string AkVersion
		{
			get
			{
				return this.akVersion;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x000071C8 File Offset: 0x000053C8
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x000071D0 File Offset: 0x000053D0
		public string FfuFilePath { get; private set; }

		// Token: 0x04000235 RID: 565
		private readonly string packageId;

		// Token: 0x04000236 RID: 566
		private readonly string name;

		// Token: 0x04000237 RID: 567
		private readonly string softwareVersion;

		// Token: 0x04000238 RID: 568
		private readonly string akVersion;
	}
}
