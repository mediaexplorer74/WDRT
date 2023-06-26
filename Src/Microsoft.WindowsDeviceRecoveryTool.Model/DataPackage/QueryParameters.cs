using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage
{
	// Token: 0x0200004C RID: 76
	public class QueryParameters
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00006819 File Offset: 0x00004A19
		public QueryParameters()
		{
			this.ManufacturerProductLine = "WindowsPhone";
			this.PackageType = "VariantSoftware";
			this.PackageClass = "Public";
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00006847 File Offset: 0x00004A47
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000684F File Offset: 0x00004A4F
		public string ManufacturerName { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00006858 File Offset: 0x00004A58
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00006860 File Offset: 0x00004A60
		public string ManufacturerModelName { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00006869 File Offset: 0x00004A69
		// (set) Token: 0x0600025D RID: 605 RVA: 0x00006871 File Offset: 0x00004A71
		public string ManufacturerProductLine { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000687A File Offset: 0x00004A7A
		// (set) Token: 0x0600025F RID: 607 RVA: 0x00006882 File Offset: 0x00004A82
		public string PackageType { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000688B File Offset: 0x00004A8B
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00006893 File Offset: 0x00004A93
		public string PackageClass { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000689C File Offset: 0x00004A9C
		// (set) Token: 0x06000263 RID: 611 RVA: 0x000068A4 File Offset: 0x00004AA4
		public string ManufacturerHardwareModel { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000068AD File Offset: 0x00004AAD
		// (set) Token: 0x06000265 RID: 613 RVA: 0x000068B5 File Offset: 0x00004AB5
		public string ManufacturerHardwareVariant { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000068BE File Offset: 0x00004ABE
		// (set) Token: 0x06000267 RID: 615 RVA: 0x000068C6 File Offset: 0x00004AC6
		public Dictionary<string, string> ExtendedAttributes { get; set; }

		// Token: 0x06000268 RID: 616 RVA: 0x000068D0 File Offset: 0x00004AD0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("ManufacturerName: {0}, ", this.ManufacturerName);
			stringBuilder.AppendFormat("ManufacturerModelName: {0}, ", this.ManufacturerModelName);
			stringBuilder.AppendFormat("ManufacturerProductLine: {0}, ", this.ManufacturerProductLine);
			stringBuilder.AppendFormat("PackageType: {0}, ", this.PackageType);
			stringBuilder.AppendFormat("PackageClass: {0}, ", this.PackageClass);
			stringBuilder.AppendFormat("ManufacturerHardwareModel: {0}, ", this.ManufacturerHardwareModel);
			stringBuilder.AppendFormat("ManufacturerHardwareVariant: {0}", this.ManufacturerHardwareVariant);
			return stringBuilder.ToString();
		}
	}
}
