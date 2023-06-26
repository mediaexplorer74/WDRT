using System;
using System.Collections.Generic;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage
{
	// Token: 0x0200004B RID: 75
	public class MsrPackageInfo : PackageFileInfo
	{
		// Token: 0x0600024C RID: 588 RVA: 0x00006734 File Offset: 0x00004934
		public MsrPackageInfo(string path)
			: base(path)
		{
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000673F File Offset: 0x0000493F
		public MsrPackageInfo(string packageId, string name, string softwareVersion)
			: base(string.Empty)
		{
			this.packageId = packageId;
			this.name = name;
			this.softwareVersion = softwareVersion;
			this.akVersion = null;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000676A File Offset: 0x0000496A
		public MsrPackageInfo(string packageId, string name, string softwareVersion, string akVersion)
			: base(string.Empty)
		{
			this.packageId = packageId;
			this.name = name;
			this.softwareVersion = softwareVersion;
			this.akVersion = akVersion;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00006796 File Offset: 0x00004996
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000679E File Offset: 0x0000499E
		public string Id { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000067A8 File Offset: 0x000049A8
		public override string PackageId
		{
			get
			{
				return this.packageId;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000252 RID: 594 RVA: 0x000067C0 File Offset: 0x000049C0
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000067D8 File Offset: 0x000049D8
		public override string SoftwareVersion
		{
			get
			{
				return this.softwareVersion;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000067F0 File Offset: 0x000049F0
		public override string AkVersion
		{
			get
			{
				return this.akVersion;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00006808 File Offset: 0x00004A08
		// (set) Token: 0x06000256 RID: 598 RVA: 0x00006810 File Offset: 0x00004A10
		public IEnumerable<MsrPackageInfo.MsrFileInfo> PackageFileData { get; set; }

		// Token: 0x04000212 RID: 530
		private readonly string packageId;

		// Token: 0x04000213 RID: 531
		private readonly string name;

		// Token: 0x04000214 RID: 532
		private readonly string softwareVersion;

		// Token: 0x04000215 RID: 533
		private readonly string akVersion;

		// Token: 0x02000058 RID: 88
		public class MsrFileInfo
		{
			// Token: 0x17000108 RID: 264
			// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000729A File Offset: 0x0000549A
			// (set) Token: 0x060002B8 RID: 696 RVA: 0x000072A2 File Offset: 0x000054A2
			public string FileName { get; set; }

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x060002B9 RID: 697 RVA: 0x000072AB File Offset: 0x000054AB
			// (set) Token: 0x060002BA RID: 698 RVA: 0x000072B3 File Offset: 0x000054B3
			public string FileType { get; set; }

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x060002BB RID: 699 RVA: 0x000072BC File Offset: 0x000054BC
			// (set) Token: 0x060002BC RID: 700 RVA: 0x000072C4 File Offset: 0x000054C4
			public string FileNameWithRevision { get; set; }

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x060002BD RID: 701 RVA: 0x000072CD File Offset: 0x000054CD
			// (set) Token: 0x060002BE RID: 702 RVA: 0x000072D5 File Offset: 0x000054D5
			public string FileVersion { get; set; }
		}
	}
}
